using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransaction : MonoBehaviour
{
    private CameraController cam;

    public Vector2 newMinpos;
    public Vector2 newMaxpos;
    public Vector3 movePlayer;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    // Trigger detection when the player enters the area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Adjust camera bounds
            cam.minPosition = newMinpos;
            cam.maxPosition = newMaxpos;

            // Move the player using its Rigidbody2D
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.MovePosition(playerRb.position + (Vector2)movePlayer);
            }
        }
    }
}
