using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public Text text;
    private AudioSource finishSound;
    private bool levelCompleted = false;
    private Timer timer; // Reference to the Timer script

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        timer = FindObjectOfType<Timer>(); // Find the Timer component in the scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2") && !levelCompleted)
        {
            levelCompleted = true;
            finishSound.Play();
            timer.StopTimer(); // Call the StopTimer method from the Timer script to stop the timer
            text.text = Time.time.ToString("0.00  ");
            Invoke("CompleteLevel", 2f); // calling the CompleteLevel function after 2 seconds
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
