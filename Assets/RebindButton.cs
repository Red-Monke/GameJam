using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindButton : MonoBehaviour
{
    // Start is called before the first frame update
    public UI uiScript;
    public InputActionReference action;
    public TextMeshProUGUI text;
    public void UpdateText()
    {
        text.text = InputControlPath.ToHumanReadableString(action.action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        Debug.Log("Updated Text to: " + InputControlPath.ToHumanReadableString(action.action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice));
    }

    public void OnClick(){
        uiScript.StartRebindKey(text, action);
    }
}
