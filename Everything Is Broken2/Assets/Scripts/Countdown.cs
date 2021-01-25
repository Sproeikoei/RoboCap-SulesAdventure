using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] RoboArmScript roboArmScript;
    public static bool win = false;
    public Text countDownTimer;
    public float targetTime = 60.0f;

    private void Start()
    {
        StartCoroutine(updateTimer());
    }

    IEnumerator updateTimer()
    {
        yield return new WaitUntil(() => RoboArmScript.Started);
        while (targetTime > 0f)
        {
            targetTime -= Time.deltaTime;
            yield return null;
            countDownTimer.text = Mathf.RoundToInt(targetTime).ToString();
            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
        }
    }

    void timerEnded()
    {
        countDownTimer.text = "You Win with " + (12-roboArmScript.childrenRenderers.Count) + " arms remaining!" ;
        targetTime = 0;

        win = true;

        roboArmScript.LostGame(targetTime);


        RoboArmScript.Started = false;
    }


}