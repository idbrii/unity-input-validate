using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TMPro;
using System;
using idbrii.InputValidation;

public class ControllerTest : MonoBehaviour
{
    public TextMeshProUGUI Information;
    public TextMeshProUGUI Controller_Dpad;
    public TextMeshProUGUI Controller_Buttons;
    public TextMeshProUGUI Controller_LeftStick;
    public TextMeshProUGUI Controller_RightStick;
    public TextMeshProUGUI Controller_Shoulder_Trigger;
    public TextMeshProUGUI Controller_Device_Name;
    public TextMeshProUGUI Input_System;
    public TMP_Dropdown Controller_Dropdown;

    [Header("Controllers")]
    public GameObject DualShock_Controller;
    public GameObject XBOX_Controller;

    int m_GamepadCount = -1;
    GamepadMappings.Mapping m_CurrentMapping;

    private void Awake()
    {
        Application.runInBackground = true;
        m_CurrentMapping = GamepadMappings.Create_Xinput();
        Controller_Dropdown.onValueChanged.AddListener((i) => RefreshSystemState());
    }

    void Update()
    {
        var nl = Environment.NewLine;

        var names = Input.GetJoystickNames();
        if (names.Length != m_GamepadCount)
        {
            RefreshSystemState();
        }
        if (names.Length == 0)
        {
            Information.text = "No Gamepads connected";
            Controller_Dpad.text = "NULL";
            Controller_Buttons.text = "NULL";
            Controller_LeftStick.text = "NULL";
            Controller_RightStick.text = "NULL";
            Controller_Shoulder_Trigger.text = "NULL";
            Controller_Device_Name.text = "NULL";

            return;
        }

        var index = Controller_Dropdown.value;
        var current_name = names[index];

        {
            Information.text = $"Controller {index}" + nl +
                               "Display Name: " + current_name + nl;
            Controller_Device_Name.text = current_name;
            Controller_Dpad.text = "DPAD: " + nl + GetAxisState2D("DPAD") + nl;
            Controller_Buttons.text = 
                                      "North: " + GetButtonState("Button Y") + nl +
                                      "South: " + GetButtonState("Button A") + nl +
                                      "East: " + GetButtonState("Button B") + nl +
                                      "West: " + GetButtonState("Button X") + nl;
            Controller_LeftStick.text = "Left Stick BTN: " + GetButtonState("Left Stick Button") + nl +
                                        "Left Stick: " + GetAxisState2D("Left Stick") + nl;
            Controller_RightStick.text = "Right Stick BTN: " + GetButtonState("Right Stick Button") + nl +
                                         "Right Stick: " + GetAxisState2D("Right Stick") + nl;
            Controller_Shoulder_Trigger.text = "Left Shoulder: " + GetButtonState("Left Bumper") + nl +
                                               "Right Shoulder: " + GetButtonState("Right Bumper") + nl +
                                               "Left Trigger: " + GetAxisState1D("Left Trigger") + nl +
                                               "Right Trigger: " + GetAxisState1D("Right Trigger") + nl +
                                               "Start: " + GetButtonState("Start") + nl +
                                               "Select: " + GetButtonState("Back") + nl;
        }
    }

    enum GamepadType
    {
        Unknown,
        Xbox,
        DualShock,
    }

    bool HasPartialMatch(string haystack, string[] needles)
    {
        bool has_match = !string.IsNullOrEmpty(needles.FirstOrDefault(n => haystack.Contains(n)));
        return has_match;
    }

    GamepadType DetectGamepad(string gamepad_name)
    {
#if UNITY_IOS || UNITY_TVOS
        if (gamepad_name.Contains("DUALSHOCK"))
        {
            return GamepadType.DualShock;
        }

        if (HasPartialMatch(gamepad_name, new string[] {
            "Xbox",
            "[extended,wireless] joystick",
        }))
        {
            return GamepadType.Xbox;
        }
        return GamepadType.Unknown;

#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        if (gamepad_name.Contains("Unknown Xbox Wireless Controller"))
        {
            return GamepadType.Xbox;
        }

        if (HasPartialMatch(gamepad_name, new string[] {
            "Sony Computer Entertainment Wireless Controller" ,
                "Unknown DUALSHOCK 4 Wireless Controller"         ,
                // Unknown usually uses dualshock button layout.
                "Unknown Wireless Controller"                     ,
        }))
        {
            return GamepadType.DualShock;
        }
        return GamepadType.Unknown;

#else
        // Not sure what the names are on other platforms. Here are some guesses.
        if (gamepad_name.Contains("Sony")
                || gamepad_name.Contains("PlayStation")
                || gamepad_name.Contains("DualShockGamepadHID"))
        {
            return GamepadType.DualShock;
        }
        else if (gamepad_name.Contains("XInput")
                || gamepad_name.Contains("Xbox")
                || gamepad_name.Contains("XInputControllerWindows"))
        {
            return GamepadType.Xbox;
        }
        return GamepadType.Unknown;
#endif
    }

    string GetButtonState(string id)
    {
        string state = "";
        if (m_CurrentMapping.GetButtonDown(id))
        {
            state += "Down";
        }
        if (m_CurrentMapping.GetButton(id))
        {
            state += "Held";
        }
        if (m_CurrentMapping.GetButtonUp(id))
        {
            state += "Up";
        }
        return state;
    }

    string GetAxisState1D(string id)
    {
        var x = m_CurrentMapping.GetAxis(id);
        return $"{x:F1}";
    }

    string GetAxisState2D(string prefix)
    {
        var v = GetAxis2D(prefix);
        return $"{v.x:F1},{v.y:F1}";
    }

    public bool IsButtonHeld(string id)
    {
        return m_CurrentMapping.GetButton(id);
    }

    public float GetAxis(string id)
    {
        return m_CurrentMapping.GetAxis(id);
    }

    public Vector2 GetAxis2D(string prefix)
    {
        var x = m_CurrentMapping.GetAxis(prefix + " Horizontal") + m_CurrentMapping.GetAxis(prefix + " Right") - m_CurrentMapping.GetAxis(prefix + " Left");

        // Maintain Unity's pattern of inverted vertical axes.
        var y = m_CurrentMapping.GetAxis(prefix + " Vertical")   + m_CurrentMapping.GetAxis(prefix + " Down")  - m_CurrentMapping.GetAxis(prefix + " Up");
        return new Vector2(x,y);
    }



    void RefreshSystemState()
    {
        var nl = Environment.NewLine;

        var names = Input.GetJoystickNames();

        m_GamepadCount = names.Length;
        Controller_Dropdown.ClearOptions();
        Controller_Dropdown.AddOptions(names.ToList());
        Controller_Dropdown.value = Mathf.Clamp(Controller_Dropdown.value, 0, names.Length);
        Controller_Dropdown.RefreshShownValue();

        if (names.Length == 0)
        {
            Controller_Dropdown.value = -1;
            return;
        }

        int device_count = names.Length;
        int disconnected = names
            .Where(string.IsNullOrEmpty)
            .Count();
        Input_System.text = $"Devices.Count: {device_count - disconnected}" + nl +
                            $"DisconnectedDevices.Count: {disconnected}";

        var current_name = names[Controller_Dropdown.value];
        bool is_dualshock = DetectGamepad(current_name) == GamepadType.DualShock;
        DualShock_Controller.SetActive(is_dualshock);
        XBOX_Controller.SetActive(!is_dualshock);

        if (is_dualshock)
        {
            m_CurrentMapping = GamepadMappings.Create_DualShock();
        }
        else
        {
            m_CurrentMapping = GamepadMappings.Create_Xinput();
        }
        m_CurrentMapping.m_GamepadId = Controller_Dropdown.value;

    }

}
