using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartIntro(IntroDialogue intro)
    {
        Debug.Log("Starting intro");

        sentences.Clear();

        foreach (string sentence in intro.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndIntro();
            return;
        }

        string setnence = sentences.Dequeue();
    }

    void EndIntro()
    {
        Debug.Log("End of intro");
    }
}
