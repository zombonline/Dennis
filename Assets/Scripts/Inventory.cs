using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    PlayerMovement player;
    GameObject craftCanvas;

    [Header("Parts")]
    public int partACount;
    public int partBCount;
    public int partCCount;
    public int partLimit;
    [Header("Part Counts")]
    [SerializeField] TextMeshProUGUI partAText;
    [SerializeField] Image partAImage;
    [SerializeField] TextMeshProUGUI partBText;
    [SerializeField] Image partBImage;
    [SerializeField] TextMeshProUGUI partCText;
    [SerializeField] Image partCImage;

    [SerializeField] Image craftableAImage;
    [SerializeField] Image craftableBImage;
    [SerializeField] Image craftableCImage;

    [SerializeField] Animator aAnim;
    [SerializeField] Animator bAnim;
    [SerializeField] Animator cAnim;
    [SerializeField] Animator dAnim;

    bool inCombineScreen = false;
    bool loadingCombineScreen = false;


    [SerializeField] TextMeshProUGUI multiplierText;

    float standardTransparency;

    public List<int> clickedParts;


    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        standardTransparency = craftableAImage.color.a;

        craftCanvas = GameObject.Find("Crafting Canvas");
    }

    private void Update()
    {
        HandlePartImagesAndText();
        HandleCraftableImages();
        CraftCanvas();

        if(inCombineScreen)
        {
            Time.timeScale = 0;
        }
    }
    void CraftA()
    {
        Debug.Log("Attempting to craft Shield");
        if(partACount>0 && partBCount >0 && player.hasShield == false)
        {
            partACount--;
            partBCount--;
            player.hasShield = true;
            aAnim.SetTrigger("fade");
        }
    }
    void CraftB()
    {
        Debug.Log("Attempting to craft Dash");
        if (partBCount > 0 && partCCount > 0 && player.dashCount < player.maxDashCount)
        {
            partBCount--;
            partCCount--;
            player.dashCount += player.dashAddAmount;
            bAnim.SetTrigger("fade");
            if (player.dashCount > player.maxDashCount)
            {
                player.dashCount = player.maxDashCount;
            }
        }
    }
    void CraftC()
    {
        Debug.Log("Attempting to craft Stopwatch");
        if (partACount > 0 && partCCount > 0 && player.timeSlowCount < player.maxTimeSlowCount)
        {
            partACount--;
            partCCount--;
            player.timeSlowCount += 1;
            cAnim.SetTrigger("fade");
        }
    }
    void CraftD()
    {
        Debug.Log("Attempting to Craft Multiplier");
        if(partACount > 0 && partBCount > 0 && partCCount > 0)
        {
            partACount--;
            partBCount--;
            partCCount--;
            FindObjectOfType<GameSessionScore>().multiplier++;
            dAnim.SetTrigger("fade");
        }
    }
    public void CraftPowerUp()
    {
        if (clickedParts.Count == 2)
        {
            var craftNumber = clickedParts[0] + clickedParts[1];
            switch (craftNumber)
            {
                case 3:
                    CraftA();
                    break;

                case 4:
                    CraftC();
                    break;

                case 5:
                    CraftB();
                    break;
                default:
                    Debug.Log("Unsure what to craft...");
                    break;

            }
        }
        else if (clickedParts.Count == 3)
        {
            CraftD();
        }
        else
        { return; }

    }
    void CraftCanvas()
    {
        if (Input.GetKeyDown(KeyCode.C) && !loadingCombineScreen)
        {
            if (!inCombineScreen)
            {
                StartCoroutine(CraftCanvasCoRoutine());
            }
            else
            {
                ExitCraftCanvas();
            }
        }
    }
    void ExitCraftCanvas()
    {
        foreach (PartButton partButton in FindObjectsOfType<PartButton>())
        {
            partButton.UnselectPart();
        }
        craftCanvas.GetComponent<Canvas>().enabled = false;
        inCombineScreen = false;
        FindObjectOfType<TimeController>().ResetTime();
    }
    IEnumerator CraftCanvasCoRoutine()
    {
        FindObjectOfType<TimeController>().slowTimeToStop = true;
        loadingCombineScreen = true;
        yield return new WaitForSecondsRealtime(1.5f);
        inCombineScreen = true;
        loadingCombineScreen = false;
        craftCanvas.GetComponent<Canvas>().enabled = true;
    }

    void HandlePartImagesAndText()
    {
        partAText.text = partACount.ToString();
        partBText.text = partBCount.ToString();
        partCText.text = partCCount.ToString();

        if (partACount > 0)
        {
            partAImage.color = new Color
                (partAImage.color.r, partAImage.color.g, partAImage.color.b, 1);
        }
        else
        {
            partAImage.color = new Color
                (partAImage.color.r, partAImage.color.g, partAImage.color.b, standardTransparency);
        }

        if (partBCount > 0)
        {
            partBImage.color = new Color
                (partBImage.color.r, partBImage.color.g, partBImage.color.b, 1);
        }
        else
        {
            partBImage.color = new Color
                (partBImage.color.r, partBImage.color.g, partBImage.color.b, standardTransparency);
        }

        if (partCCount > 0)
        {
            partCImage.color = new Color
                (partCImage.color.r, partCImage.color.g, partCImage.color.b, 1);
        }
        else
        {
            partCImage.color = new Color
                (partCImage.color.r, partCImage.color.g, partCImage.color.b, standardTransparency);
        }

    }
    void HandleCraftableImages()
    {
        //craftable A
        if (player.hasShield)
        {
            craftableAImage.color = new Color
                (craftableAImage.color.r, craftableAImage.color.g, craftableAImage.color.b, 1);
        }
        else
        {
            craftableAImage.color = new Color
                (craftableAImage.color.r, craftableAImage.color.g, craftableAImage.color.b, standardTransparency);
        }

        //craftable B
        if (player.dashCount > 0)
        {
            craftableBImage.color = new Color
                (craftableBImage.color.r, craftableBImage.color.g, craftableBImage.color.b, 1);
        }
        else
        {
            craftableBImage.color = new Color
                (craftableBImage.color.r, craftableBImage.color.g, craftableBImage.color.b, standardTransparency);
        }
        //craftable C
        if (player.timeSlowCount > 0)
        {
            craftableCImage.color = new Color
                (craftableCImage.color.r, craftableCImage.color.g, craftableCImage.color.b, 1);
        }
        else
        {
            craftableCImage.color = new Color
                (craftableCImage.color.r, craftableCImage.color.g, craftableCImage.color.b, standardTransparency);
        }
        //Craftable D Multiplier
        multiplierText.text = FindObjectOfType<GameSessionScore>().multiplier.ToString() + "x";
    }

}
