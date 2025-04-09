using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Object References")]
    public InputActionAsset playerInput;
    public PlayerInput playerInputObject;
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public LayerMask boxesLayer;
    public GameObject boxHolder;
    public Transform boxHeld;

    [Header("Actions Keyboard")]
    public InputAction moveRightActionKb;
    public InputAction moveLeftActionKb;
    public InputAction jumpActionKb;
    public InputAction interactActionKb;
    public InputAction menuActionKb;

    [Header("Gamepad Keyboard")]
    public InputActionReference movementPositiveGp;
    public InputActionReference movementNegativeGp;
    public InputActionReference jumpGp;
    public InputActionReference interactGp;
    public InputActionReference menuGp;

    [Header("Values")]
    public float speed;
    public float jumpForce;
    public int moveState;
    public bool paused = false;
    public bool flipState = false; //false = right; true = left;
    public float flipStateFloat() {if (flipState) {return -1;} else {return 1;}}
    public bool holdingBox = false;

    // Update is called once per frame
    void Update() {
        rb.velocity = new Vector2(moveState * speed, rb.velocity.y);

        //flip logic
        if (holdingBox) { boxHeld.position = boxHolder.transform.position; }
        else if (rb.velocity.x > 0) { flipState = false;
            gameObject.transform.localScale = new Vector2(1f, 1f); }
        else if (rb.velocity.x < 0) { flipState = true;
            gameObject.transform.localScale = new Vector2(-1f, 1f);}
    }

    public void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<BoxCollider2D>();

        string rebinds = PlayerPrefs.GetString("rebinds", string.Empty);
        if (string.IsNullOrEmpty(rebinds)){return;}
        playerInputObject.actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void Awake() {
        jumpActionKb = playerInput.FindActionMap("Gameplay").FindAction("Jump");
        moveRightActionKb = playerInput.FindActionMap("Gameplay").FindAction("Movement Right");
        moveLeftActionKb = playerInput.FindActionMap("Gameplay").FindAction("Movement Left");
        interactActionKb = playerInput.FindActionMap("Gameplay").FindAction("Interaction");
        menuActionKb = playerInput.FindActionMap("Gameplay").FindAction("Menu");
        RegisterInputActions();
    }

    void RegisterInputActions(){
        //keyboard inputs
        jumpActionKb.performed -= ctx => Jump(ctx);
        moveRightActionKb.performed -= ctx => MoveRight(ctx);
        moveLeftActionKb.performed -= ctx => MoveLeft(ctx);
        moveRightActionKb.canceled -= ctx => MoveRightCancel(ctx);
        moveLeftActionKb.canceled -= ctx => MoveLeftCancel(ctx);
        interactActionKb.performed -= ctx => Interact(ctx);
        menuActionKb.performed -= ctx => Menu(ctx);

        jumpActionKb.performed += ctx => Jump(ctx);
        moveRightActionKb.performed += ctx => MoveRight(ctx);
        moveLeftActionKb.performed += ctx => MoveLeft(ctx);
        moveRightActionKb.canceled += ctx => MoveRightCancel(ctx);
        moveLeftActionKb.canceled += ctx => MoveLeftCancel(ctx);
        interactActionKb.performed += ctx => Interact(ctx);
        menuActionKb.performed += ctx => Menu(ctx);
    }

    void MoveRight(InputAction.CallbackContext ctx) {
        moveState += 1;
    }

    void MoveLeft(InputAction.CallbackContext ctx) {
        moveState -= 1;
    }

    void MoveRightCancel(InputAction.CallbackContext ctx) {
        moveState -= 1;
    }

    void MoveLeftCancel(InputAction.CallbackContext ctx) {
        moveState += 1;
    }

    void Jump(InputAction.CallbackContext ctx) {
        if (holdingBox) {return;}
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }

    void Interact(InputAction.CallbackContext ctx) {
        Debug.Log("Interact pressed in this context");
        if (holdingBox) {
            holdingBox = false;
            boxHeld.transform.parent = null;
            return;
        }
        
        Debug.Log("Reached raycast code");
        RaycastHit2D ray = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(flipStateFloat(), 0f), .1f, boxesLayer);
        
        if (ray) {
            holdingBox = true;
            ray.transform.SetParent(boxHolder.transform, true);
            boxHeld = ray.transform;
        }
    }

    void Menu(InputAction.CallbackContext ctx) {
        if (paused == false) {
            Time.timeScale = 0.0f;
            paused = true;
            //open menu
        }
        else if (paused == true) {
            Time.timeScale = 1.0f;
            paused = false;
            //close menu
        }
        Debug.Log("menu pressed in current context");
    }
}
