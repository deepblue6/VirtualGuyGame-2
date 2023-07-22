using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    private float startTime;
    private bool isRunning = false; // Flag to control the timer

    private void Start()
    {
        // Start the timer when the scene loads
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            text.text = elapsedTime.ToString("0.00");
        }
    }

    // Call this method to start the timer
    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    // Call this method to stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    // Call this method to reset the timer
    public void ResetTimer()
    {
        startTime = Time.time;
    }
}
