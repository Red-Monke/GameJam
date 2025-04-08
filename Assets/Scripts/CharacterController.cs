using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float speedMultiplier;
    float characterDirection;
    Rigidbody2D pb;

    public GameObject groundRayObject;
    public GameObject obstacleRayObject;
    [SerializeField] float obstacleRayDistance;
    [SerializeField] float groundRayDistance;

    public LayerMask obstacleLayermask;
    bool jumpable;

    // Start is called before the first frame update
    void Start()
    {
        jumpable = false;
        pb = GetComponent<Rigidbody2D>();
        characterDirection = 0f;
    }

    private void Update()
    {
        if(moveInput < 0)
        {
            characterDirection = -1f;
        }
        else if (moveInput > 0)
        {
            characterDirection = 1f;   
        }
        else
        {
            characterDirection = 0f;
        }

        Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        
        DetectObstacle();
    }

    void DetectObstacle()
    {
        RaycastHit2D hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, Vector2.right * new Vector2(characterDirection, 0f), obstacleRayDistance, obstacleLayermask);

        if(hitObstacle.collider != null)
        {
            Debug.Log("Obstacle Detected");
            Debug.DrawRay(obstacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(characterDirection, 0), Color.red);
        }
        else
        {
            Debug.Log("No Obstacle Detected");
            Debug.DrawRay(obstacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2(characterDirection, 0), Color.green);
        }
    }

    private void Movement()
    {
        moveInput = Input.GetAxis("Horizontal");
        pb.velocity = new Vector2(moveInput * moveSpeed, pb.velocity.y);

        RaycastHit2D hitGround = Physics2D.Raycast(groundRayObject.transform.position, -Vector2.up, groundRayDistance);
        Debug.DrawRay(groundRayObject.transform.position, -Vector2.up * hitGround.distance, Color.red);

        if (hitGround.collider != null )
        {
            if(hitGround.distance <= 0.2f)
            {
                jumpable = true;
            }
            else
            {
                jumpable = false;
                
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpable == true)
            {
                pb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                return;
            }
        }
    }
}
