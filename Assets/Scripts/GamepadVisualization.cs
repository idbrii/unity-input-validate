using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamepadVisualization : MonoBehaviour
{
    public ControllerTest m_Input;

    [Header("Images")]
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


    float LeftStickHalf;
    float RightStickHalf;

    Color Press = new Color(1f, 0, 0, 0.5f);
    Color NotPress = new Color(0, 0, 0, 0.5f);

    void Start()
    {
        LeftStickHalf = LeftStickBTN.rectTransform.sizeDelta.x / 2;
        RightStickHalf = RightStickBTN.rectTransform.sizeDelta.x / 2;
    }

    // Map [-1,1] to [0,1]
    static float MapNegativeToAbsolute(float x)
    {
        // First map to [0,2]
        var out_of_2 = 1f + x;
        // Then to [0,1]
        return out_of_2 / 2f;
    }

    void Update()
    {
        if (m_Input.Controller_Dropdown.value < 0)
        {
            return;
        }

        UpdateButton("Button B", B_BTN);
        UpdateButton("Button A", A_BTN);
        UpdateButton("Button Y", Y_BTN);
        UpdateButton("Button X", X_BTN);
        UpdateButton("Left Stick Button", LeftStickBTN);
        UpdateButton("Left Bumper", LeftShoulder);
        UpdateButton("Right Stick Button", RightStickBTN);
        UpdateButton("Right Bumper", RightShoulder);
        UpdateButton("Start", StartBTN);
        UpdateButton("Back", SelectBTN);


        var dpad = new Vector2(m_Input.GetAxis("DPAD Horizontal"), m_Input.GetAxis("DPAD Vertical"));
        UpdateButton(dpad.y < -0.1f, Up);
        UpdateButton(dpad.y >  0.1f, Down);
        UpdateButton(dpad.x < -0.1f, Left);
        UpdateButton(dpad.x >  0.1f, Right);


        // Unity input uses inverted vertical sticks by default.
        var left_stick  = new Vector2(m_Input.GetAxis("Left Stick Horizontal"),  -m_Input.GetAxis("Left Stick Vertical"));
        var right_stick = new Vector2(m_Input.GetAxis("Right Stick Horizontal"), -m_Input.GetAxis("Right Stick Vertical"));

        LeftStick.transform.localPosition = left_stick * LeftStickHalf;
        RightStick.transform.localPosition = right_stick * RightStickHalf;


        var left_trigger = MapNegativeToAbsolute(m_Input.GetAxis("Left Trigger"));
        var right_trigger = MapNegativeToAbsolute(m_Input.GetAxis("Right Trigger"));

        LeftTrigger.color = new Color(left_trigger, 0, 0, 0.5f);
        LeftTriggerText.text = left_trigger.ToString("P0");

        RightTrigger.color = new Color(right_trigger, 0, 0, 0.5f);
        RightTriggerText.text = right_trigger.ToString("P0");
    }

    void UpdateButton(string id, RawImage overlay)
    {
        UpdateButton(m_Input.IsButtonHeld(id), overlay);
    }

    void UpdateButton(bool is_held, RawImage overlay)
    {
        if (is_held)
        {
            overlay.color = Press;
        }
        else
        {
            overlay.color = NotPress;
        }
    }

}
