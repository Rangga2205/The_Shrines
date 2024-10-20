using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private Animator myAnim;
    public Transform target;
    private Vector3 homePose;  // Changed to Vector3 to store original position
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxRange;
    [SerializeField]
    private float minRange;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;

        // Set homePose to the enemy's starting position
        homePose = transform.position;  // Now using position instead of Transform
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer <= maxRange && distanceToPlayer >= minRange)
        {
            FollowPlayer();
        }
        else if (distanceToPlayer >= maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        // Enemy returns to its original position (homePose) when out of range
        if (Vector3.Distance(transform.position, homePose) > 0.1f)
        {
            myAnim.SetBool("isMoving", true);
            myAnim.SetFloat("moveX", (homePose.x - transform.position.x));
            myAnim.SetFloat("moveY", (homePose.y - transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, homePose, speed * Time.deltaTime);
        }
        else
        {
            myAnim.SetBool("isMoving", false);  // Stop moving when at home position
        }
    }
    private void OnTriggerEnter2D(Collider2D other) // Corrected capitalization of Collider2D
    {
    if (other.tag == "MyWeapon")
        {
            // Calculate the difference between the two positions
            Vector2 difference = transform.position - other.transform.position;

            // Optionally normalize the difference to avoid excessive movement
            difference = difference.normalized;

            // Move the object in the direction of the difference
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }

}

