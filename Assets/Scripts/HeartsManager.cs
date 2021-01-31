using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    private int brokenHeartHalves = 0;
    private int brokenHeartHalvesMax = 10;

    private int happyHearts = 0;
    private int happyHeartsMax = 5;
    public GameObject HeartContainer;

    public void AddBrokenHeartHalves(int amount)
    {
        brokenHeartHalves += amount;
        Debug.Log("Broken Hearts Halves: " + brokenHeartHalves.ToString());
        // todo update ui

        if (brokenHeartHalves >= brokenHeartHalvesMax)
        {
            Debug.Log("Aww, you lost!");
            // todo handle
        }
    }

    public void AddAHappyHeart()
    {
        happyHearts += 1;
        Debug.Log("Happy Hearts: " + happyHearts.ToString());
        UpdateHeartContainer();

        if (happyHearts >= happyHeartsMax)
        {
            Debug.Log("Congrats, you won!");
            // todo handle
        }
    }
    private void UpdateHeartContainer()
    {
        switch (happyHearts)
        {
            case 1:
                HeartContainer.transform.Find("Heart1").gameObject.SetActive(true);
                break;
            case 2:
                HeartContainer.transform.Find("Heart2").gameObject.SetActive(true);
                break;
            case 3:
                HeartContainer.transform.Find("Heart3").gameObject.SetActive(true);
                break;
            case 4:
                HeartContainer.transform.Find("Heart4").gameObject.SetActive(true);
                break;
            case 5:
                HeartContainer.transform.Find("Heart5").gameObject.SetActive(true);
                break;
        }
    }
}
