using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;


    private void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime); // the three arguments are the x y and z of the game object, the saw
    }
}
