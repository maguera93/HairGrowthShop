using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinsAnimation : MonoBehaviour {

    private float finalValueAlpha;
    private float timeDuration;
    private float timeCounter, timePositionCounter;
    private float initAlpha;
    private float fadeOutDuration;
    private Color colorImage;
    private Text image;
    private Vector3 position;
    private float startPostion, finalPosition;
    private RectTransform myTransfor;

    public bool animActive = false;
    private bool fadeIn = true;


    // Use this for initialization
    void Start()
    {
        image = GetComponent<Text>();
        colorImage = image.color;
        initAlpha = 0;
        finalValueAlpha = 1.0f;
        timeDuration = 0.4f;
        fadeOutDuration = 0.4f;
        myTransfor = GetComponent<RectTransform>();
        startPostion = myTransfor.localPosition.y;
        position = myTransfor.localPosition;
        finalPosition = 492;

        timeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animActive)
        {
            if (fadeIn)
            {
                if (timeCounter <= timeDuration)
                {
                    timeCounter += Time.deltaTime;
                    float currentAlpha = (float)Easing.CubicEaseIn(timeCounter, initAlpha, (finalValueAlpha - initAlpha), timeDuration);

                    colorImage.a = currentAlpha;
                    image.color = colorImage;

                }
                else
                {
                    fadeIn = false;
                    timeCounter = 0;
                }
            }
            else
            {
                if (timeCounter <= fadeOutDuration)
                {
                    timeCounter += Time.deltaTime;
                    float currentAlpha = (float)Easing.CubicEaseIn(timeCounter, finalValueAlpha, (initAlpha - finalValueAlpha), fadeOutDuration);

                    colorImage.a = currentAlpha;
                    image.color = colorImage;

                }
                else animActive = false;
            }

            if(timePositionCounter <= fadeOutDuration*2)
            {
                timePositionCounter += Time.deltaTime;
                position.y = (float)Easing.Linear(timePositionCounter, startPostion, (finalPosition - startPostion), timeDuration*2);

                myTransfor.localPosition = position;
            }
        }
        else
        {
            IntialValue();
        }

    }

    public void ResetAnim()
    {
        timeCounter = 0;
        timePositionCounter = 0;
        colorImage.a = finalValueAlpha;
        image.color = colorImage;
        fadeIn = false;
    }

    private void IntialValue()
    {
        timeCounter = 0;
        timePositionCounter = 0;
        colorImage.a = 0;
        image.color = colorImage;
        fadeIn = true;
    }
}
