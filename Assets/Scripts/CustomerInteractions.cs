using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteractions : MonoBehaviour
{
    public GameObject PoolOfPossibleLovers;
    public GameObject RequestBox;

    enum TaskStage
    {
        NeedNewTask,
        Clue1,
        Clue2,
        Clue3
    }
    private TaskStage currentStage = TaskStage.NeedNewTask;
    private GameObject currentNeededItem;

    void Start()
    {

        // start a timer for when the first person starts screaming
    }

    public void ReceiveClick()
    {
        if (currentStage == TaskStage.NeedNewTask)
        {
            PickNewNeededItem();
        }
    }

    private void PickNewNeededItem()
    {
        //currentNeededItem = PoolOfPossibleLovers.transform.GetChild(Random.Range(0, PoolOfPossibleLovers.transform.childCount)).gameObject;
        currentNeededItem = PoolOfPossibleLovers.transform.GetChild(0).gameObject;
        Debug.Log("I need a: " + currentNeededItem.name);
        GetDialogueClue(1);
    }

    private void GetDialogueClue(int whichClue)
    {
        if (whichClue == 1)
        {
            RequestBox.SetActive(true);
            string text = "My lover always liked " + currentNeededItem.GetComponent<LoverData>().TheyAlwaysLiked;
            RequestBox.transform.Find("RequestText").GetComponent<UnityEngine.UI.Text>().text = text;
        }

    }
}
