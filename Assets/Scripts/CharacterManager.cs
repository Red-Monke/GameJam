using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IInteractable
{
    #region MOVEMENT
    float moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float speedMultiplier;
    float characterDirection;
    Rigidbody2D pb;
    #endregion
    #region INTERACTION
    public GameObject obstacleRayObject;
    public GameObject switchRayObject;
    GameObject interactedObject;
    [SerializeField] float obstacleRayDistance;
    [SerializeField] float switchRayDistance;
    public LayerMask obstacleLayermask;
    public LayerMask switchLayermask;
    RaycastHit2D hitObstacle;
    RaycastHit2D hitSwitch;
    #endregion

    bool jumpable;
    bool facingRight;
    bool pullingObject;

    // Start is called before the first frame update
    void Start()
    {
        jumpable = true;
        pb = GetComponent<Rigidbody2D>();
        characterDirection = 0f;
    }

    private void Update()
    {
        DetectFacingDirection();
        Jump();

        if (Input.GetButtonDown("Interact"))
        {
            Interact();
            Debug.Log("Interact pressed");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        moveInput = Input.GetAxis("Horizontal");
        pb.velocity = new Vector2(moveInput * moveSpeed, pb.velocity.y);
    }
    void DetectFacingDirection()
    {
        if (moveInput < 0)
        {
            facingRight = false;
        }
        else if (moveInput > 0)
        {
            facingRight = true;
        }

        if (facingRight)
        {
            characterDirection = 1f;
        }
        else
        {
            characterDirection = -1f;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpable)
            {
                pb.velocity = Vector2.up * jumpForce;
                jumpable = false;
            }
            else
            {
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable") || collision.gameObject.CompareTag("Ground"))
        {
            jumpable = true;
        }
    }

    public void Interact()
    {
        DetectInteractableObject();
    }

    void DetectInteractableObject()
    {
        hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, Vector2.right * new Vector2(characterDirection, 0f), obstacleRayDistance, obstacleLayermask);

        if (hitObstacle.collider != null)
        {
            GrabObject();
            
            Debug.Log("Obstacle Detected");
        }
        else
        {
            Debug.Log("No Obstacle Detected");
        }

        hitSwitch = Physics2D.Raycast(switchRayObject.transform.position, Vector2.right * new Vector2(characterDirection, 0f), switchRayDistance, switchLayermask);

        if (hitSwitch.collider != null)
        {
            UseSwitch();
            Debug.Log("Switch Detected");
        }
        else
        {
            Debug.Log("No Switch Detected");
        }
    }

    void GrabObject()
    {
        interactedObject = hitObstacle.collider.gameObject;

        interactedObject.GetComponent<FixedJoint2D>().enabled = true;
        interactedObject.GetComponent<BoxPull>().beingPulled = true;
        interactedObject.GetComponent<FixedJoint2D>().connectedBody = this.pb;
        
        if (Input.GetButtonDown("Interact") && interactedObject.GetComponent<BoxPull>().beingPulled == true)
        {
            interactedObject.GetComponent<FixedJoint2D>().enabled = false;
            interactedObject.GetComponent<BoxPull>().beingPulled = false;
        }
    }

    void UseSwitch()
    {

    }
}
