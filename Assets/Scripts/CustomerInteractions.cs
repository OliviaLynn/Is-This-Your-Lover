using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerInteractions : MonoBehaviour
{
    public GameObject PoolOfPossibleLovers;
    public GameObject HeartsManager;
    public GameObject Portal;

    public GameObject HelloBox;
    public GameObject RequestBox;
    public GameObject ThatsNotThemBox;
    public GameObject GiveUpBox;
    public GameObject ThanksBox;
    public GameObject FailureBox;

    public GameObject LosePanel;
    public GameObject WinPanel;

    public GameObject DoorChimes;
    public GameObject WinAudio;
    public GameObject LoseAudio;

    enum TaskStage
    {
        Hello,
        Clue1,
        Clue1F,
        Clue2,
        Clue2F,
        Clue3,
        Thanks,
        GiveUp,
        Failure,
        GameWin,
        GameLose
    }
    private TaskStage currentStage = TaskStage.Hello;
    private GameObject currentNeededItem;

    void Start()
    {
        // start a timer for when the first person starts screaming
        DoorChimes.GetComponent<AudioSource>().Play(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentStage == TaskStage.Hello)
            {
                HideHelloDialogue();
                PickNewNeededItem();
                Portal.GetComponent<PortalDoYeet>().ResetFailedAttempts();
                GoToNextClue();
            }
            else if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
            {
                HideCurrentClue();
            }
            else if (currentStage == TaskStage.Clue1F || currentStage == TaskStage.Clue2F)
            {
                HideThatsNotThem();
                GoToNextClue();
            }
            else if (currentStage == TaskStage.GiveUp)
            {
                HideGiveUpDialogue();
                currentStage = TaskStage.Hello;
            }
            else if (currentStage == TaskStage.Thanks)
            {
                HideThanksDiaogue();
                currentStage = TaskStage.Hello;
                // TODO timer to trigger door chime/screams
                DoorChimes.GetComponent<AudioSource>().Play(0);
            }
            else if (currentStage == TaskStage.Failure)
            {
                HideFailureDiaogue();
                currentStage = TaskStage.Hello;
                // TODO timer to trigger door chime/screams
                DoorChimes.GetComponent<AudioSource>().Play(0);
            }
            else if (currentStage == TaskStage.GameWin)
            {
                // TODO to main menu after game win
                Debug.Log("I should be reloading now");
                SceneManager.LoadScene(0);

            }
            else if (currentStage == TaskStage.GameLose)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
            {
                HideCurrentClue();
                currentStage = TaskStage.GiveUp;
                HeartsManager.GetComponent<HeartsManager>().AddBrokenHeartHalves(1);
                ShowGiveUpDialogue();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //HeartsManager.GetComponent<HeartsManager>().AddAHappyHeart();
        }
    }

    // ------------- Called by other scripts:

    public void LoseWholeGame()
    {
        HideFailureDiaogue();
        HideThanksDiaogue();
        HideGiveUpDialogue();
        HideThatsNotThem();
        HideCurrentClue();
        HideHelloDialogue();
        GiveUpBox.SetActive(false);

        currentStage = TaskStage.GameLose;

        LoseAudio.GetComponent<AudioSource>().Play(0);
        LosePanel.SetActive(true);
    }

    public void WinWholeGame()
    {
        HideFailureDiaogue();
        HideThanksDiaogue();
        HideGiveUpDialogue();
        HideThatsNotThem();
        HideCurrentClue();
        HideHelloDialogue();

        currentStage = TaskStage.GameWin;

        WinAudio.GetComponent<AudioSource>().Play(0);
        WinPanel.SetActive(true);
    }

    public void ReceiveClick() // when the door gets clicked, called from Player.InteractWithObjects
    {
        DoorChimes.GetComponent<AudioSource>().Stop();
        if (currentStage == TaskStage.Hello)
        {
            ShowHelloDialogue();
        }
        else if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
        {
            ShowCurrentClue();
        }
    }

    public int IsThisYourLover(GameObject possibleLover) // a check performed by Portal.PortalDoYeet
    {
        if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
        {
            if (possibleLover.name == currentNeededItem.name)
            {
                currentStage = TaskStage.Thanks;
                HeartsManager.GetComponent<HeartsManager>().AddAHappyHeart();
                HideCurrentClue();
                ShowThanksDialogue();
                return 1; 
            }
            else
            {
                GoToNextClue();
                return 0;
            }
        }
        else
        {
            // We're not in a stage where checking something at the portal makes sense
            return -1;
        }
    }

    // ------------- Progress task:
    private void PickNewNeededItem()
    {
        currentNeededItem = PoolOfPossibleLovers.transform.GetChild(Random.Range(0, PoolOfPossibleLovers.transform.childCount)).gameObject;
        //currentNeededItem = PoolOfPossibleLovers.transform.GetChild(0).gameObject;
        //Debug.Log("I need a: " + currentNeededItem.name);
    }

    private void GoToNextClue()
    {
        //Debug.Log(currentStage);
        if (currentStage == TaskStage.Hello)
        {
            // We've agreed to help, show first clue
            // todo first time in between one
            currentStage = TaskStage.Clue1;
            SetDialogueClueUI();
            ShowCurrentClue();
        }
        else if (currentStage == TaskStage.Clue1)
        {
            // Fail first clue, show "That's not them!"
            currentStage = TaskStage.Clue1F;
            HideCurrentClue();
            ShowThatsNotThem();
        }
        else if (currentStage == TaskStage.Clue1F)
        {
            // Continued from "That's not them," showing second clue:
            currentStage = TaskStage.Clue2;
            HideThatsNotThem();
            SetDialogueClueUI();
            ShowCurrentClue();
        }
        else if (currentStage == TaskStage.Clue2)
        {
            // Fail second clue, show "That's not them!"
            currentStage = TaskStage.Clue2F;
            HideCurrentClue();
            ShowThatsNotThem();
        }
        else if (currentStage == TaskStage.Clue2F)
        {
            // Continued from "That's not them," showing third clue:
            currentStage = TaskStage.Clue3;
            SetDialogueClueUI();
            ShowCurrentClue();
        }
        else if (currentStage == TaskStage.Clue3)
        {
            // Fail second clue, fail completely
            currentStage = TaskStage.Failure;
            HeartsManager.GetComponent<HeartsManager>().AddBrokenHeartHalves(2);
            HideCurrentClue();
            ShowFailureDiaogue();
        }
    }

    // ------------- UI and Display:

    private void ShowHelloDialogue()
    {
        HelloBox.SetActive(true);
    }
    private void HideHelloDialogue()
    {
        HelloBox.SetActive(false);
    }
    private void ShowCurrentClue()
    {
        RequestBox.SetActive(true);
    }
    private void HideCurrentClue()
    {
        RequestBox.SetActive(false);
    }
    private void ShowThatsNotThem()
    {
        ThatsNotThemBox.SetActive(true);
    }
    private void HideThatsNotThem()
    {
        ThatsNotThemBox.SetActive(false);
    }
    private void ShowGiveUpDialogue()
    {
        GiveUpBox.SetActive(true);
    }
    private void HideGiveUpDialogue()
    {
        GiveUpBox.SetActive(false);
    }
    private void ShowThanksDialogue()
    {
        ThanksBox.SetActive(true);
    }
    private void HideThanksDiaogue()
    {
        ThanksBox.SetActive(false);
    }
    private void ShowFailureDiaogue()
    {
        FailureBox.SetActive(true);
    }
    private void HideFailureDiaogue()
    {
        FailureBox.SetActive(false);
    }

    private void SetDialogueClueUI()
    {
        if (currentStage == TaskStage.Clue1)
        {
            string text = "My lover always liked " + currentNeededItem.GetComponent<LoverData>().TheyAlwaysLiked;
            RequestBox.transform.Find("RequestText").GetComponent<UnityEngine.UI.Text>().text = text;
        }
        else if (currentStage == TaskStage.Clue2)
        {
            string text = "My lover always was " + currentNeededItem.GetComponent<LoverData>().TheyAlwaysWere;
            RequestBox.transform.Find("RequestText").GetComponent<UnityEngine.UI.Text>().text = text;
        }
        else if (currentStage == TaskStage.Clue3)
        {
            string text = "My lover always said " + currentNeededItem.GetComponent<LoverData>().TheyAlwaysSaid;
            RequestBox.transform.Find("RequestText").GetComponent<UnityEngine.UI.Text>().text = text;
        }
    }
}
