using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller: MonoBehaviour

{
    [SerializeField] float standardBackgroundScrollSpeed;
    float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;


    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //move offset by speed every second
        offSet = new Vector2(0f, backgroundScrollSpeed);
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;


        //half speed if player has time slow enabled
        if (FindObjectOfType<PlayerMovement>().timeSlowed)
        {
            backgroundScrollSpeed = standardBackgroundScrollSpeed / 2;
        }
        else
        {
            backgroundScrollSpeed = standardBackgroundScrollSpeed;
        }

    }
}
