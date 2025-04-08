using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugInputs : MonoBehaviour
{
    public InputActionAsset playerInput;
    public InputAction jumpAction;
    public InputAction moveRightAction;
    public InputAction moveLeftAction;
    public InputAction interactAction;
    public InputAction menuAction;
    void Awake()
    {
        jumpAction = playerInput.FindActionMap("Gameplay").FindAction("Jump");
        moveRightAction = playerInput.FindActionMap("Gameplay").FindAction("Movement Right");
        moveLeftAction = playerInput.FindActionMap("Gameplay").FindAction("Movement Left");
        interactAction = playerInput.FindActionMap("Gameplay").FindAction("Interaction");
        menuAction = playerInput.FindActionMap("Gameplay").FindAction("Menu");
        RegisterInputActions();
    }

    // Update is called once per frame
    public void OnEnable()
    {
        jumpAction.Enable();
        moveRightAction.Enable();
        moveLeftAction.Enable();
        interactAction.Enable();
        menuAction.Enable();
    }

    public void OnDisable()
    {
        jumpAction.Disable();
        moveRightAction.Disable();
        moveLeftAction.Disable();
        interactAction.Disable();
        menuAction.Disable();
    }

    void RegisterInputActions(){
        jumpAction.performed += ctx => Jump(ctx);
        moveRightAction.performed += ctx => MoveRight(ctx);
        moveLeftAction.performed += ctx => MoveLeft(ctx);
        interactAction.performed += ctx => Interact(ctx);
        menuAction.performed += ctx => Menu(ctx);
    }

    public void Jump(InputAction.CallbackContext ctx){
        if (!ctx.performed) { return ;}
        Debug.Log("Jump key has been pressed!");
    }
    public void MoveRight(InputAction.CallbackContext ctx){
        if (!ctx.performed) { return ;}
        Debug.Log("Move Right key has been pressed!");
    }
    public void MoveLeft(InputAction.CallbackContext ctx){
        if (!ctx.performed) { return ;}
        Debug.Log("Move Left key has been pressed!");
    }
    public void Interact(InputAction.CallbackContext ctx){
        if (!ctx.performed) { return ;}
        Debug.Log("Interact key has been pressed!");
    }
    public void Menu(InputAction.CallbackContext ctx){
        if (!ctx.performed) { return ;}
        Debug.Log("Menu key has been pressed!");
    }
}
