using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    private int brokenHeartHalves = 0;
    private int brokenHeartHalvesMax = 10;
    public GameObject BrokenHeartContainer;

    private int happyHearts = 0;
    private int happyHeartsMax = 5;
    public GameObject HeartContainer;

    public GameObject CustomerInteractions;

    public void AddBrokenHeartHalves(int amount)
    {
        brokenHeartHalves += amount;
        Debug.Log("Broken Hearts Halves: " + brokenHeartHalves.ToString());
        UpdateBrokenHeartContainer();

        if (brokenHeartHalves >= brokenHeartHalvesMax)
        {
            Debug.Log("Aww, you lost!");
            // todo handle
            CustomerInteractions.GetComponent<CustomerInteractions>().LoseWholeGame();
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
            CustomerInteractions.GetComponent<CustomerInteractions>().WinWholeGame();
        }
    }
    private void UpdateBrokenHeartContainer()
    {
        switch (brokenHeartHalves)
        {
            case 1:
                BrokenHeartContainer.transform.Find("HeartH1").gameObject.SetActive(true);
                break;
            case 2:
                BrokenHeartContainer.transform.Find("Heart1").gameObject.SetActive(true);
                break;
            case 3:
                BrokenHeartContainer.transform.Find("HeartH2").gameObject.SetActive(true);
                break;
            case 4:
                BrokenHeartContainer.transform.Find("Heart2").gameObject.SetActive(true);
                break;
            case 5:
                BrokenHeartContainer.transform.Find("HeartH3").gameObject.SetActive(true);
                break;
            case 6:
                BrokenHeartContainer.transform.Find("Heart3").gameObject.SetActive(true);
                break;
            case 7:
                BrokenHeartContainer.transform.Find("HeartH4").gameObject.SetActive(true);
                break;
            case 8:
                BrokenHeartContainer.transform.Find("Heart4").gameObject.SetActive(true);
                break;
            case 9:
                BrokenHeartContainer.transform.Find("HeartH5").gameObject.SetActive(true);
                break;
            case 10:
                BrokenHeartContainer.transform.Find("Heart5").gameObject.SetActive(true);
                break;
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
