using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private AudioSource audio;
    private Canvas canvas;

    public AudioClip introOne;
    public AudioClip introTwo;
    public AudioClip introThree;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        audio = GetComponent<AudioSource>();
        canvas = FindObjectOfType<Canvas>();
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
        if (sentences.Count == 3) { audio.clip = introOne; }
        else if (sentences.Count == 2) { audio.clip = introTwo; }
        else if (sentences.Count == 1) { audio.clip = introThree; }

        audio.Play();

        if (sentences.Count == 0)
        {
            EndIntro();
            return;
        }
        
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndIntro()
    {
        audio.clip = null;
        canvas.GetComponent<SceneFader>().PlayNewGame();
        Debug.Log("End of intro");
    }
}
