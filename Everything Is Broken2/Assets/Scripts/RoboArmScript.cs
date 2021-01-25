using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoboArmScript : MonoBehaviour
{
    public static bool Started = false;

    public Queue<Transform> childrenRenderers = new Queue<Transform>();
    public Image FadeToBlack;
    public Button restartButton;
    public Text restartText;
    public List<NavMeshAgent> agent;

    public void TurnOffChildren(Transform childThatWasHit)
    {
        childrenRenderers.Enqueue(childThatWasHit);
        childThatWasHit.gameObject.SetActive(false);

        if (childrenRenderers.Count == 12)
        {
            LostGame(childrenRenderers.Count);
        }
    }

    public void TurnOnChildren()
    {
        Transform availableObject = childrenRenderers.Dequeue();
        availableObject.gameObject.SetActive(true);
    }

    public void LostGame(float timeLeft)
    {
        StartCoroutine(LostFader());
    }

    IEnumerator LostFader()
    {
        if (Countdown.win)
            Time.timeScale = 0.4f;
        else
            Time.timeScale = 1f;
        Started = false;
        while (FadeToBlack.color.a < 0.4f)
        {
            float fadeAmount = FadeToBlack.color.a + (0.6f * Time.deltaTime);
            FadeToBlack.color = new Color(FadeToBlack.color.r, FadeToBlack.color.g, FadeToBlack.color.b, fadeAmount);

            yield return null;
        }

        StartCoroutine(RestartFader());
    }

    IEnumerator RestartFader()
    {
        Time.timeScale = 1f;
        while (FadeToBlack.color.a < 1f)
        {
            float fadeAmount = FadeToBlack.color.a + (0.6f * Time.deltaTime);
            FadeToBlack.color = new Color(FadeToBlack.color.r, FadeToBlack.color.g, FadeToBlack.color.b, fadeAmount);

            yield return null;
        }

        SceneManager.LoadScene("SampleScene");
        ToolAnimation.attacking = true;
        Countdown.win = false;
        StartCoroutine (StartFader());
    }
    IEnumerator StartFader()
    {
        while (FadeToBlack.color.a > 0f)
        {
            float fadeAmount = FadeToBlack.color.a - (0.6f * Time.deltaTime);
            FadeToBlack.color = new Color(FadeToBlack.color.r, FadeToBlack.color.g, FadeToBlack.color.b, fadeAmount);

            yield return null;
        }
        yield return null;

        Started = true;
    }

    private void Awake()
    {
        FadeToBlack.color = new Color(FadeToBlack.color.r, FadeToBlack.color.g, FadeToBlack.color.b, 1);

        StartCoroutine(StartFader());
    }

    public void Restart()
    {
        StartCoroutine(RestartFader());
    }
}
