using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//~ using UnityEngine.Experimental.Input;
//~ using UnityEngine.Experimental.Input.LowLevel;
using UnityEngine.UI;

public class GamepadVisualization : MonoBehaviour
{
    public RawImage B_BTN;
    public RawImage A_BTN;
    public RawImage Y_BTN;
    public RawImage X_BTN;
    public RawImage Up;
    public RawImage Down;
    public RawImage Left;
    public RawImage Right;
    public RawImage LeftStickBTN;
    public GameObject LeftStick;
    public RawImage LeftShoulder;
    public RawImage LeftTrigger;
    public TextMeshProUGUI LeftTriggerText;
    public RawImage RightStickBTN;
    public GameObject RightStick;
    public RawImage RightShoulder;
    public RawImage RightTrigger;
    public TextMeshProUGUI RightTriggerText;
    public RawImage StartBTN;
    public RawImage SelectBTN;

    ControllerTest CT = new ControllerTest();

    float LeftStickHalf;
    float RightStickHalf;

    Color Press = new Color(1f, 0, 0, 0.5f);
    Color NotPress = new Color(0, 0, 0, 0.5f);

    private void Start()
    {
        CT = GameObject.FindWithTag("CT").GetComponent<ControllerTest>();
        LeftStickHalf = LeftStickBTN.rectTransform.sizeDelta.x / 2;
        RightStickHalf = RightStickBTN.rectTransform.sizeDelta.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //~ var gamepad = Gamepad.all[CT.Controller_Dropdown.value];
        //~ if (gamepad != null)
        //~ {
        //~     if (gamepad[GamepadButton.Circle].isPressed)        { B_BTN.color         = Press; } else { B_BTN.color         = NotPress; }
        //~     if (gamepad[GamepadButton.Cross].isPressed)         { A_BTN.color         = Press; } else { A_BTN.color         = NotPress; }
        //~     if (gamepad[GamepadButton.Triangle].isPressed)      { Y_BTN.color         = Press; } else { Y_BTN.color         = NotPress; }
        //~     if (gamepad[GamepadButton.Square].isPressed)        { X_BTN.color         = Press; } else { X_BTN.color         = NotPress; }
        //~     if (gamepad[GamepadButton.DpadUp].isPressed)        { Up.color            = Press; } else { Up.color            = NotPress; }
        //~     if (gamepad[GamepadButton.DpadDown].isPressed)      { Down.color          = Press; } else { Down.color          = NotPress; }
        //~     if (gamepad[GamepadButton.DpadLeft].isPressed)      { Left.color          = Press; } else { Left.color          = NotPress; }
        //~     if (gamepad[GamepadButton.DpadRight].isPressed)     { Right.color         = Press; } else { Right.color         = NotPress; }
        //~     if (gamepad[GamepadButton.LeftStick].isPressed)     { LeftStickBTN.color  = Press; } else { LeftStickBTN.color  = NotPress; }
        //~     if (gamepad[GamepadButton.LeftShoulder].isPressed)  { LeftShoulder.color  = Press; } else { LeftShoulder.color  = NotPress; }
        //~     if (gamepad[GamepadButton.RightStick].isPressed)    { RightStickBTN.color = Press; } else { RightStickBTN.color = NotPress; }
        //~     if (gamepad[GamepadButton.RightShoulder].isPressed) { RightShoulder.color = Press; } else { RightShoulder.color = NotPress; }
        //~     if (gamepad[GamepadButton.Start].isPressed)         { StartBTN.color      = Press; } else { StartBTN.color      = NotPress; }
        //~     if (gamepad[GamepadButton.Select].isPressed)        { SelectBTN.color     = Press; } else { SelectBTN.color     = NotPress; }

        //~     LeftStick.transform.localPosition = new Vector3(LeftStickHalf * gamepad.leftStick.x.ReadValue(), LeftStickHalf * gamepad.leftStick.y.ReadValue(), 0);
        //~     LeftTrigger.color = new Color(gamepad.leftTrigger.ReadValue(), 0, 0, 0.5f);
        //~     LeftTriggerText.text = Mathf.Round(gamepad.leftTrigger.ReadValue() * 100) + "%";

        //~     RightStick.transform.localPosition = new Vector3(RightStickHalf * gamepad.rightStick.x.ReadValue(), RightStickHalf * gamepad.rightStick.y.ReadValue(), 0);
        //~     RightTrigger.color = new Color(gamepad.rightTrigger.ReadValue(), 0, 0, 0.5f);
        //~     RightTriggerText.text = Mathf.Round(gamepad.rightTrigger.ReadValue() * 100) + "%";
        //~ }
    }
}
