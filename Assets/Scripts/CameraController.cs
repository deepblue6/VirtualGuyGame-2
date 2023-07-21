using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [SerializeField] private float minCameraSize = 5f; // Minimum camera size to show both players
    [SerializeField] private float maxSizeDistance = 10f; // Maximum distance between players for the camera to zoom out

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Calculate the distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);

        // Calculate the desired camera size based on the distance between players
        float targetCameraSize = Mathf.Clamp(distance / maxSizeDistance * minCameraSize, minCameraSize, float.MaxValue);

        // Set the camera's size to smoothly zoom in/out
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetCameraSize, Time.deltaTime * 5f);

        // Calculate the average position between the two players
        Vector3 averagePosition = (player1.position + player2.position) / 2f;

        // Set the camera's position to be at the average position while keeping the camera's original z-coordinate
        transform.position = new Vector3(averagePosition.x, averagePosition.y, transform.position.z);
    }
}
