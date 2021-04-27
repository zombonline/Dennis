using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButton : MonoBehaviour
{
    Inventory inventory;
    Image image;
    float standardTransparency;
    bool isClicked;

    //Part number is used to identify parts when combine method is ran.
    public int partNumber;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        image = GetComponent<Image>();
        standardTransparency = image.color.a;
    }

    private void OnMouseDown()
    {
        if(partNumber == 1)
        {
            if (!isClicked && inventory.partACount > 0)
            { SelectPart(); }
            else if (isClicked)
            { UnselectPart(); }
        }
        if (partNumber == 2)
        {
            if (!isClicked && inventory.partBCount > 0)
            { SelectPart(); }
            else if (isClicked)
            { UnselectPart(); }
        }
        if (partNumber == 3)
        {
            if (!isClicked && inventory.partCCount > 0)
            { SelectPart(); }
            else if (isClicked)
            { UnselectPart(); }
        }

    }

    private void SelectPart()
    {
        isClicked = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        inventory.clickedParts.Add(partNumber);
    }

    //method used to unselect a part when clicked or when player exits combine screen.
    public void UnselectPart()
    {
        isClicked = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, standardTransparency);
        inventory.clickedParts.Remove(partNumber);
    }

}
