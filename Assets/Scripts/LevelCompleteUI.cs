using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    public Image[] starImages;

    public void DisplayStars(int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = (i < starCount);
        }
    }
}
