using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player 2"))
        {
            collision.gameObject.transform.SetParent(transform); // Set the parent of the player to this object     
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player 2"))
        {
            collision.gameObject.transform.SetParent(null); // Set the parent of the player to null   
        }
    }
}
