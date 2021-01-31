using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteractions : MonoBehaviour
{
    public GameObject PoolOfPossibleLovers;
    public GameObject RequestBox;
    public GameObject ThanksBox;

    enum TaskStage
    {
        NeedNewTask,
        Clue1,
        Clue2,
        Clue3,
        Thanks,
        NoThanks
    }
    private TaskStage currentStage = TaskStage.NeedNewTask;
    private GameObject currentNeededItem;

    void Start()
    {

        // start a timer for when the first person starts screaming
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
            {
                HideCurrentClue();
            }
            else if (currentStage == TaskStage.Thanks)
            {
                HideThanksDiaogue();
                // TODO timer to trigger next customer
            }
        }
    }

    // ------------- Called by other scripts:

    public void ReceiveClick()
    {
        if (currentStage == TaskStage.NeedNewTask)
        {
            PickNewNeededItem();
            DisplayCurrentClue();
        }
        if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
        {
            DisplayCurrentClue();
        }
    }

    public int IsThisYourLover(GameObject possibleLover)
    {
        if (currentStage == TaskStage.Clue1 || currentStage == TaskStage.Clue2 || currentStage == TaskStage.Clue3)
        {
            if (possibleLover.name == currentNeededItem.name)
            {
                currentStage = TaskStage.Thanks;
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
            // Not in a stage where checking something at the portal makes sense
            return -1;
        }
    }

    // ------------- Progress task:
    private void PickNewNeededItem()
    {
        //currentNeededItem = PoolOfPossibleLovers.transform.GetChild(Random.Range(0, PoolOfPossibleLovers.transform.childCount)).gameObject;
        currentNeededItem = PoolOfPossibleLovers.transform.GetChild(0).gameObject;
        Debug.Log("I need a: " + currentNeededItem.name);
        currentStage = TaskStage.Clue1;
        SetDialogueClueUI();
        DisplayCurrentClue();
    }

    private void GoToNextClue()
    {
        if (currentStage == TaskStage.Clue1)
        {
            currentStage = TaskStage.Clue2;
            SetDialogueClueUI();
            DisplayCurrentClue();
        }
        else if (currentStage == TaskStage.Clue2)
        {
            currentStage = TaskStage.Clue3;
            SetDialogueClueUI();
            DisplayCurrentClue();
        }
        else if (currentStage == TaskStage.Clue3)
        {
            Debug.Log("That was your last clue!");
            // TODO handle case!!
        }
    }

    // ------------- UI and Display:

    private void DisplayCurrentClue()
    {
        RequestBox.SetActive(true);
    }
    private void HideCurrentClue()
    {
        RequestBox.SetActive(false);
    }
    private void ShowThanksDialogue()
    {
        ThanksBox.SetActive(true);
    }
    private void HideThanksDiaogue()
    {
        ThanksBox.SetActive(false);
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
