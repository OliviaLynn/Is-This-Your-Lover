using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMeter : MonoBehaviour
{
    public float currentHearts = 0.0f;
    public float totalHearts = 5.0f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite twoThirdHeart;
    public Sprite thirdHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        // Clamp heart to be between 0 and max
        if (currentHearts > totalHearts)
        {
            currentHearts = totalHearts;
        }
        else if (currentHearts < 0.0f)
        {
            currentHearts = 0.0f;
        }

        // Go through the list and change heart sprite based on value
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHearts)
            {
                if (currentHearts % (i) <= 0.33f)
                {
                    hearts[i].sprite = thirdHeart;
                }
                else if (currentHearts % (i) <= 0.66f)
                {
                    hearts[i].sprite = twoThirdHeart;
                }
                else
                {
                    hearts[i].sprite = fullHeart;
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            //Sets total max hearts visible
            if (i < totalHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
