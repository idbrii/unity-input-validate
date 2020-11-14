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
    public TextMeshProUGUI Controller_Devices_List;
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
    }

    void Update()
    {
        var nl = Environment.NewLine;

        var names = Input.GetJoystickNames();
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

        if (names.Length != m_GamepadCount)
        {
            RefreshSystemState();
        }
        var index = Controller_Dropdown.value;
        var current_name = names[index];

        m_CurrentMapping.m_GamepadId = Controller_Dropdown.value;

        {
            Information.text = $"Controller {index}" + nl +
                               "Display Name: " + current_name + nl;
            Controller_Device_Name.text = current_name;
            Controller_Dpad.text = GetAxisState2D("DPAD") + nl;
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

            if (current_name.Contains("Sony")
                    || current_name.Contains("PlayStation")
                    || current_name.Contains("DualShockGamepadHID"))
            {
                DualShock_Controller.SetActive(true);
                XBOX_Controller.SetActive(false);
            }
            else if (current_name.Contains("XInput")
                    || current_name.Contains("Xbox")
                    || current_name.Contains("XInputControllerWindows"))
            {
                DualShock_Controller.SetActive(false);
                XBOX_Controller.SetActive(true);
            }
            //~ else if (current_name.Contains("Nintendo Wireless Gamepad"))
            //~ {
            //~     Controller_Device_Name.text = "Nintendo Switch Joycon/Pro";
            //~     DualShock_Controller.SetActive(false);
            //~     XBOX_Controller.SetActive(false);
            //~ }
            else
            {
                DualShock_Controller.SetActive(false);
                XBOX_Controller.SetActive(true);
            }
        }
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
        var x = m_CurrentMapping.GetAxis(prefix + " Horizontal");
        var y = m_CurrentMapping.GetAxis(prefix + " Vertical");
        return $"{x:F1},{y:F1}";
    }

    public bool IsButtonHeld(string id)
    {
        return m_CurrentMapping.GetButton(id);
    }

    public float GetAxis(string id)
    {
        return m_CurrentMapping.GetAxis(id);
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

        int device_count = names.Length;
        int disconnected = names
            .Where(string.IsNullOrEmpty)
            .Count();
        Input_System.text = $"Devices.Count: {device_count - disconnected}" + nl +
                            $"DisconnectedDevices.Count: {disconnected}";

        int i = 0;
        Controller_Devices_List.text = names
            .Aggregate("Device List", (sum, next) => string.Concat(sum, $"\n{i++} {next}"));
                
    }

}
