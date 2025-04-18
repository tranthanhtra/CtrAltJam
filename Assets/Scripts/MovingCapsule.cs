using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingCapsule : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField, Range(0, 100)] private float maxSpeed = 20f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f;
    private Rigidbody rigidbody;
    private Vector3 velocity;

    private Vector2 changeDirectionAfter = new Vector2(1f, 3f);
    private float changeDirectionTime;
    private float lastChangeDirectionTime;
    private Vector2 randomInput;
    private bool touchingWall;
    private Vector3 initPosition;
    private string directionCollide;
    private float freeDirection;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        Vector2 playerInput;
        // playerInput.x = Input.GetAxis("Horizontal");
        // playerInput.y = Input.GetAxis("Vertical");

        // Clamp the input so that the player can't move faster than 1 unit but keep the value if < 1
        // playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        lastChangeDirectionTime += Time.deltaTime;

        Vector2 XRange;
        Vector2 ZRange;

        switch (directionCollide)
        {
            case "Right":
                XRange = new Vector2(-1, 0);
                ZRange = new Vector2(-1, 1);
                freeDirection = Random.Range(1, 3);
                break;
            case "Left":
                XRange = new Vector2(0, 1);
                ZRange = new Vector2(-1, 1);
                freeDirection = Random.Range(1, 3);
                break;
            case "Top":
                XRange = new Vector2(-1, 1);
                ZRange = new Vector2(-1, 0);
                freeDirection = Random.Range(1, 3);
                break;
            case "Bottom":
                XRange = new Vector2(-1, 1);
                ZRange = new Vector2(0, 1);
                freeDirection = Random.Range(1, 3);
                break;
            default:
                XRange = new Vector2(-1, 1);
                ZRange = new Vector2(-1, 1);
                break;
        }

        if (lastChangeDirectionTime > changeDirectionTime || touchingWall)
        {
            lastChangeDirectionTime = 0;
            changeDirectionTime = Random.Range(changeDirectionAfter.x, changeDirectionAfter.y);
            randomInput = new Vector2(UnityEngine.Random.Range(XRange.x, XRange.y), UnityEngine.Random.Range(ZRange.x, ZRange.y));
        }

        if (freeDirection > 0) freeDirection -= Time.deltaTime;
        if (freeDirection <= 0)
        {
            directionCollide = "";
        }
        playerInput = randomInput;

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

        touchingWall = false;
    }

    public void ResetPosition()
    {
        transform.position = initPosition;
        Debug.Log(transform.position);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag(Utils.wallTag))
        {
            // Debug.Log("wall");
            touchingWall = true;
            directionCollide = collision.gameObject.name;
        }
    }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Key"))
    //     {
    //         Debug.Log("died");
    //     }
    // }
}