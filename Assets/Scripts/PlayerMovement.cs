using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region health and shield configuration
    [Header("Health & Shield Configuration")]
    [SerializeField] int hitPoints = 3;
    public bool hasShield = false;
    [SerializeField] [Range(0, 1f)] private float damageSlowMoRate = 0.5f;
    [SerializeField] int deathDelay = 3;

    [SerializeField] AudioClip[] dmgSounds;
    [SerializeField] Sprite[] heartSprites;
    [SerializeField] Image heartImage;

    [SerializeField] SpriteRenderer damageFace;
    #endregion

    #region movement configuration
    [Header("Movement Confiuration")]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float boundaryPadding = 1f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float newXpos;
    float newYpos;
    #endregion

    #region dash configuration
    [Header("Dash Configuration")]

    [SerializeField] public int dashCount;
    [SerializeField] public int maxDashCount;
    [SerializeField] public int dashAddAmount;

    [SerializeField] private float dashForce=100;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashInvincibleDuration = 0.5f;
    [SerializeField] private float dashCooldown =3f;
    [SerializeField] [Range(0, 1f)] private float dashTransparency = 0.5f;
    private float dashTimer =3f;
    #endregion

    #region time slow configuration
    [Header("Time Slow Configuration")]

    [SerializeField] public int timeSlowCount;
    [SerializeField] public int maxTimeSlowCount;
    [SerializeField] public float timeSlowLength;
    public bool timeSlowed;
    #endregion

    #region cashed references
    CircleCollider2D circleCollider;
    Rigidbody2D rb;
    SpriteRenderer spriteRender;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        SetUpMovementBoundaries();
    }

    void Update()
    {

        BasicMovement();
        ExecuteDash();
        ExecuteTimeSlow();
        HandleHeartSprites();
    }

    #region movement methods
    private void BasicMovement()
    {
        //get Horizontal and Vertical movement via inputs
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        //set the movement by adding positive or negative x/y to current transform pos
        //also keep with in the boundaries
        newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        //execute the movement 
        transform.position = new Vector2(newXpos, newYpos);
    }

    private void SetUpMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + boundaryPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - boundaryPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + boundaryPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - boundaryPadding;

    }
    #endregion

    #region dash methods
    private void ExecuteDash()
    {
        dashTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") > 0.5f
            || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0.5f
            || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") < -0.5f
            || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") < -0.5f)
        {
            if (dashTimer < 0 && dashCount > 0) 
            {
                dashCount--;
                StartCoroutine(Dash());
                dashTimer = dashCooldown;
            }
            else { return; }
        }
    }
    public IEnumerator Dash()
    {
        circleCollider.enabled = false;
        spriteRender.color = new Color(1f, 1f, 1f, dashTransparency); 

        rb.AddForce(new Vector2(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")) * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashInvincibleDuration);

        circleCollider.enabled = true;
        spriteRender.color = new Color(1f, 1f, 1f, 1f);
    }
    #endregion

    #region health & shield methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            collision.gameObject.GetComponent<ProjectileMovement>().DeleteProjectile();
            HandleHit();
        }
    }

    void HandleHit()
    {
        if(hasShield)
        {
            hasShield = false;
        }
        else
        {
            AudioSource.PlayClipAtPoint(dmgSounds[Random.Range(0, dmgSounds.Length)],
                Camera.main.transform.position, PlayerPrefsController.GetMasterSFXVolume());

            hitPoints--;
            if (hitPoints <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(HitFrames());
            }
        }
    }

    IEnumerator HitFrames()
    {
        //collision off
        circleCollider.enabled = false;
        damageFace.enabled = true;

        //slow time
        Time.timeScale = damageSlowMoRate;
        yield return new WaitForSecondsRealtime(1f);

        //disable spawners
        GameObject[] yspawners, xspawners;
        DisableSpawners(out yspawners, out xspawners);

        //destroy active projectiles
        GameObject[] activeProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in activeProjectiles)
            projectile.GetComponent<ProjectileMovement>().DeleteProjectile();

        //collision on
        circleCollider.enabled = true;
        damageFace.enabled = false;

        //make time normal
        Time.timeScale = 1f;

        //reset spawn rate
        foreach (GameObject spawner in yspawners)
            spawner.GetComponent<Spawner>().ResetSpawnRate();
        foreach (GameObject spawner in xspawners)
            spawner.GetComponent<Spawner>().ResetSpawnRate();

        //re-enable spawners after delay
        yield return new WaitForSecondsRealtime(3f);
        foreach (GameObject spawner in yspawners)
            spawner.GetComponent<Spawner>().canSpawn = true;
        foreach (GameObject spawner in xspawners)
            spawner.GetComponent<Spawner>().canSpawn = true;
    }

    private static void DisableSpawners(out GameObject[] yspawners, out GameObject[] xspawners)
    {
        yspawners = GameObject.FindGameObjectsWithTag("Y Spawner");
        foreach (GameObject spawner in yspawners)
            spawner.GetComponent<Spawner>().canSpawn = false;

        xspawners = GameObject.FindGameObjectsWithTag("X Spawner");
        foreach (GameObject spawner in xspawners)
            spawner.GetComponent<Spawner>().canSpawn = false;
    }

    IEnumerator Die()
    {
        //disable spawners
        GameObject[] yspawners, xspawners;
        DisableSpawners(out yspawners, out xspawners);

        //"kill" player
        Destroy(damageFace.gameObject);
        spriteRender.enabled = false;
        circleCollider.enabled = false;

        //load final score scene
        yield return new WaitForSecondsRealtime(deathDelay);
        FindObjectOfType<SceneLoader>().LoadFinalScoreScene();
    }

    void HandleHeartSprites()
    {
        if(hitPoints == 3)
        {
            heartImage.sprite = heartSprites[3];
        }
        else if (hitPoints ==2)
        {
            heartImage.sprite = heartSprites[2];
        }
        else if (hitPoints ==1)
        {
            heartImage.sprite = heartSprites[1];
        }
        else
        {
            heartImage.sprite = heartSprites[0];
        }
    }

    #endregion

    #region time slow methods

    void ExecuteTimeSlow()
    {
        if(!timeSlowed && Input.GetKeyDown(KeyCode.V) && timeSlowCount > 0)
        {
            timeSlowCount--;
            StartCoroutine(TimeSlow());
        }
    }    

    IEnumerator TimeSlow()
    {
        timeSlowed = true;
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = true;
        yield return new WaitForSecondsRealtime(timeSlowLength);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = false;
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = true;
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = false;
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = true;
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = false;
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = true;
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.Find("Time Slow Image").GetComponent<Image>().enabled = false;
        timeSlowed = false;

    }

    #endregion

}
