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
            Debug.Log("Player collided with a Trap!");
            StartCoroutine(Die());
            hearts--;
        }
    }

    private void Update()
    {
        heartsText.text = $"{gameObject.name} hearts: {hearts}";
    }

    IEnumerator Die()
    {
        //hearts--;
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        GetComponent<PlayerMovement>().enabled = false;
        rb.velocity = Vector2.zero; // Set the Rigidbody's velocity to zero to stop movement
        rb.bodyType = RigidbodyType2D.Static; // Freeze the Rigidbody to prevent further movement
        yield return new WaitForSeconds(1.29f);

        if (hearts > 0)
        {
            // Respawn the player at the spawnpoint
            RespawnPlayer();
        }
        else
        {
            // Reset the player's hearts to 3 and reload the scene
            
            timer.ResetTimer();
            SceneManager.LoadScene("Level 1.0.2");
            hearts = 3;
        }

        anim.SetTrigger("respawn");
        rb.bodyType = RigidbodyType2D.Dynamic; // Unfreeze the Rigidbody to allow movement again
        GetComponent<PlayerMovement>().enabled = true;
    }

    private void RespawnPlayer()
    {
        transform.position = spawnpoint.transform.position;

        // Reset any necessary player-related variables here if needed
        // For example, reset the player's health, ammo, etc.

        // Reset any other game objects or systems that need to be reset after respawning the player
        // For example, reset enemies, traps, puzzles, etc.
    }
}
