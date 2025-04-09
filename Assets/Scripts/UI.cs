using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("Actions Keyboard")]
    public InputActionReference movementPositiveKb;
    public InputActionReference movementNegativeKb;
    public InputActionReference jumpKb;
    public InputActionReference interactKb;
    public InputActionReference menuKb;

    [Header("Gamepad Keyboard")]
    public InputActionReference movementPositiveGp;
    public InputActionReference movementNegativeGp;
    public InputActionReference jumpGp;
    public InputActionReference interactGp;
    public InputActionReference menuGp;

    [Header("Rebinding")]
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    [SerializeField] GameObject[] rebindButtons;
    public DebugInputs debugInputs;
    public PlayerInput playerInput;
    public InputActionAsset inputAsset;
    public GameObject eventSystems;
    

    public void Quit(){
        Application.Quit();
        Debug.Log("Tried to quit application!");
    }

    public void LoadLevel(int level){
        Debug.Log("Tried loading level: " + level);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void StartRebindKey(TextMeshProUGUI text, InputActionReference actionToChange){
        Debug.Log("Trying to rebind key!");
        text.text = "...";

        debugInputs.OnDisable();

        rebindingOperation = actionToChange.action.PerformInteractiveRebinding().OnComplete
            (operation => RebindComplete()).Start();
    }

    private void RebindComplete(){
        rebindingOperation.Dispose();
        UpdateRebindText();
        debugInputs.OnEnable();
    }

    public void GetRebindButtonText(InputActionReference action, TextMeshProUGUI text){
        text.text = InputControlPath.ToHumanReadableString(action.action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void UpdateRebindText(){
        for (int i = 0; i < rebindButtons.Length; i++) {
            rebindButtons[i].GetComponent<RebindButton>().UpdateText();
        }
    }

    public void Save(){
        string rebinds = playerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void ResetAll(){
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void Start() {
        string rebinds = PlayerPrefs.GetString("rebinds", string.Empty);

        if (string.IsNullOrEmpty(rebinds)){return;}

        playerInput.actions.LoadBindingOverridesFromJson(rebinds);
    }
}
