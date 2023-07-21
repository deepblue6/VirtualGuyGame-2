using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    private float elapsedTime;
    private bool isRunning = true; // Flag to control the timer

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            text.text = elapsedTime.ToString("0.00  ");
        }
    }

    // Call this method to stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    // Call this method to reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
}
