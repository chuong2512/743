using TMPro;
using UnityEngine;

public class THeTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        timerIsRunning = false;
    }
    
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                CAudioManager.Instance.musicSource.Stop();
                timeText.gameObject.SetActive(false);
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float time)
    {
        time += 1;
        float hours = Mathf.FloorToInt(time / 60 / 60);
        float minutes = Mathf.FloorToInt((time - hours * 60 * 60)  / 60);
        float seconds = Mathf.FloorToInt((time - hours * 60 * 60) % 60);
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}",hours, minutes, seconds);
    }
    public void Stop()
    {
        CAudioManager.Instance.musicSource.Stop();
        timeText.gameObject.SetActive(false);
        timeRemaining = 0;
        timerIsRunning = false;
    }
    public void SetTimer(float timeToDi)
    {
        CAudioManager.Instance.musicSource.Play();
        timeText.gameObject.SetActive(true);
        timerIsRunning = true;
        timeRemaining = timeToDi;

        DisplayTime(timeToDi);
    }

}