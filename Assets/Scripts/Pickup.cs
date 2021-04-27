using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Inventory inventory;
    SpriteRenderer spriteRenderer;
    [SerializeField] AudioClip partSFX;
    [SerializeField] AudioClip pointSFX;
    [SerializeField] Sprite[] cakeSprites;
    [SerializeField] float pickupLifetime = 5f;
    [SerializeField] float spriteFlashDuration = 2f;
    [SerializeField] float spriteFlashRate = 0.1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(tag == "Point")
        {
            spriteRenderer.sprite = cakeSprites[Random.Range(0, cakeSprites.Length)];
        }
        inventory = FindObjectOfType<Inventory>();
        StartCoroutine(Despawn());
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            switch (tag)
            {
                case "Point":
                    FindObjectOfType<GameSessionScore>().pointsPickedup++;
                    AudioSource.PlayClipAtPoint(pointSFX, Camera.main.transform.position, PlayerPrefsController.GetMasterSFXVolume());
                    Destroy(gameObject);
                    Debug.Log("Picked up a Point");
                    break;

                case "Part A":
                    if(inventory.partACount < inventory.partLimit)
                    {
                        AudioSource.PlayClipAtPoint(partSFX, Camera.main.transform.position, PlayerPrefsController.GetMasterSFXVolume());
                        inventory.partACount++;
                        Debug.Log("Picked up an A part");
                    }
                    Destroy(gameObject);
                    break;

                case "Part B":
                    if (inventory.partBCount < inventory.partLimit)
                    {
                        AudioSource.PlayClipAtPoint(partSFX, Camera.main.transform.position, PlayerPrefsController.GetMasterSFXVolume());
                        inventory.partBCount++;
                        Debug.Log("Picked up a B part");
                    }
                    Destroy(gameObject);
                    break;

                case "Part C":
                    if (inventory.partCCount < inventory.partLimit)
                    {
                        AudioSource.PlayClipAtPoint(partSFX, Camera.main.transform.position, PlayerPrefsController.GetMasterSFXVolume());
                        inventory.partCCount++;
                        Debug.Log("Picked up a C part");
                    }
                    Destroy(gameObject);
                    break;

                default:
                    Debug.Log("Pickup unkown, did you tag it?");
                    break;
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSecondsRealtime(pickupLifetime);
        spriteRenderer.enabled = false;
        StartCoroutine(FlashSprite());
        yield return new WaitForSecondsRealtime(spriteFlashDuration);
        Destroy(gameObject);

    }
    IEnumerator FlashSprite()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSecondsRealtime(spriteFlashRate);
        spriteRenderer.enabled = true;
        yield return new WaitForSecondsRealtime(spriteFlashRate);
        StartCoroutine(FlashSprite());
    }

}
