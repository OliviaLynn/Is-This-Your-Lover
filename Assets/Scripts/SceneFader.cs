using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// From: https://gamedevelopertips.com/unity-how-fade-between-scenes/
/// </summary>

public class SceneFader : MonoBehaviour
{
    public Image fadeOutImage;
    public float fadeSpeed = 1.0f;

    public enum FadeDirection
    {
        In, //Alpha = 1
        Out //Alpha = 0
    }

    private void OnEnable()
    {
        StartCoroutine(Fade(FadeDirection.Out));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(FadeDirection.Out));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(FadeDirection.In));
    }

    private IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;

        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutImage.enabled = false;
        }
        else
        {
            fadeOutImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }

    public void PlayNewGame()
    {
        Debug.Log("Start fade");
        StartCoroutine(FadeAndLoadScene(FadeDirection.In, "Demo"));
    }

    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
    {
        Debug.Log("In coroutine");
        yield return Fade(fadeDirection);
        Debug.Log("About to load new scene");
        SceneManager.LoadScene(sceneToLoad);
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
}
