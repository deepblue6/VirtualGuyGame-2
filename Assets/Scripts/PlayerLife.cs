using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public Transform spawnpoint;

    private int hearts = 3;
    [SerializeField] private Text heartsText; // UI Text to display hearts count
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource deathSoundEffect; // Assign the death sound effect AudioClip in the Inspector
    private bool isDead = false;
    public Timer timer; // Reference to the Timer script
    private GameObject timerObject; // Reference to the Timer GameObject

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Find the Timer GameObject in the scene and get the Timer script from it
        timerObject = GameObject.FindWithTag("Timer");
        if (timerObject != null)
        {
            timer = timerObject.GetComponent<Timer>();
        }
        else
        {
            Debug.LogError("Timer GameObject not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            isDead = true;
            Debug.Log("Player collided with a Trap!");
            StartCoroutine(Die());
        }
    }



    private void Update()
    {
        heartsText.text = $"{gameObject.name} hearts: {hearts}";
    }

    IEnumerator Die()
    {
        hearts--;
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        GetComponent<PlayerMovement>().enabled = false;
        rb.velocity = Vector2.zero; // Set the Rigidbody's velocity to zero to stop movement
        rb.bodyType = RigidbodyType2D.Static; // Freeze the Rigidbody to prevent further movement
        yield return new WaitForSeconds(1.29f);

        if (hearts > 0)
        {
            transform.position = spawnpoint.transform.position;
        }
        else
        {
            // Reset the timer and reload the scene
            timer.ResetTimer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        anim.SetTrigger("respawn");
        rb.bodyType = RigidbodyType2D.Dynamic; // Unfreeze the Rigidbody to allow movement again
        GetComponent<PlayerMovement>().enabled = true;
    }
}
//