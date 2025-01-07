using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCapsule : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField, Range(0, 100)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    private Rigidbody rigidbody;
    private Vector3 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        // Clamp the input so that the player can't move faster than 1 unit but keep the value if < 1
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * maxSpeed;
        // multiply with delta time (time since last frame) so that frame rate doesn't affect the speed
        
        //with physic
        velocity = rigidbody.velocity;
        
        var maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        //without physic
        /*Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;*/
        
        //with physic
        rigidbody.velocity = velocity;

        if (playerInput != Vector2.zero)
        {
            var desiredQuaternion = Quaternion.LookRotation(new Vector3(playerInput.x, 0, playerInput.y));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredQuaternion, 360 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Key"))
        {
            Debug.Log("died");
        }
    }
}