using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.LowLevel;
using TMPro;
using System;

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
    private List<Gamepad> Connected_Devices_List = new List<Gamepad>();

    [Header("Controllers")]
    public GameObject DualShock_Controller;
    public GameObject XBOX_Controller;

    private void Awake()
    {
        Application.runInBackground = true;
    }

    void Update()
    {
        var nl = Environment.NewLine;
        var gamepad = Gamepad.all[Controller_Dropdown.value];
        if (gamepad != null)
        {
            Information.text = "Controller 1" + nl +
                               "Display Name: " + gamepad.displayName + nl +
                               "Name: " + gamepad.name + nl +
                               "Short Display Name: " + gamepad.shortDisplayName + nl +
                               "Description: " + gamepad.description + nl +
                               "Device: " + gamepad.device + nl +
                               "ID: " + gamepad.id + nl +
                               "Layout: " + gamepad.layout + nl +
                               "Native: " + gamepad.native + nl +
                               "Noisy: " + gamepad.noisy + nl +
                               "Path: " + gamepad.path + nl +
                               "Remote: " + gamepad.remote + nl +
                               "Synthetic: " + gamepad.synthetic + nl +
                               "Variants: " + gamepad.variants;
            Controller_Dpad.text = "UP: " + gamepad[GamepadButton.DpadUp].isPressed + nl +
                                   "Down: " + gamepad[GamepadButton.DpadDown].isPressed + nl +
                                   "Left: " + gamepad[GamepadButton.DpadLeft].isPressed + nl +
                                   "Right: " + gamepad[GamepadButton.DpadRight].isPressed + nl;
            Controller_Buttons.text = "A: " + gamepad[GamepadButton.A].isPressed + nl +
                                      "B: " + gamepad[GamepadButton.B].isPressed + nl +
                                      "X: " + gamepad[GamepadButton.X].isPressed + nl +
                                      "Y: " + gamepad[GamepadButton.Y].isPressed + nl +
                                      "O: " + gamepad[GamepadButton.Circle].isPressed + nl +
                                      "X: " + gamepad[GamepadButton.Cross].isPressed + nl +
                                      "/_\\: " + gamepad[GamepadButton.Triangle].isPressed + nl +
                                      "[ ]: " + gamepad[GamepadButton.Square].isPressed + nl +
                                      "North: " + gamepad[GamepadButton.North].isPressed + nl +
                                      "South: " + gamepad[GamepadButton.South].isPressed + nl +
                                      "East: " + gamepad[GamepadButton.East].isPressed + nl +
                                      "West: " + gamepad[GamepadButton.West].isPressed + nl;
            Controller_LeftStick.text = "Left Stick BTN: " + gamepad[GamepadButton.LeftStick].isPressed + nl +
                                        "Left Stick X: " + gamepad.leftStick.x.ReadValue() + nl +
                                        "Left Stick Y: " + gamepad.leftStick.y.ReadValue() + nl +
                                        "Left Stick UP: " + gamepad.leftStick.up.ReadValue() + nl +
                                        "Left Stick Down: " + gamepad.leftStick.down.ReadValue() + nl +
                                        "Left Stick Left: " + gamepad.leftStick.left.ReadValue() + nl +
                                        "Left Stick Right: " + gamepad.leftStick.right.ReadValue() + nl;
            Controller_RightStick.text = "Right Stick BTN: " + gamepad[GamepadButton.RightStick].isPressed + nl +
                                         "Right Stick X: " + gamepad.rightStick.x.ReadValue() + nl +
                                         "Right Stick Y: " + gamepad.rightStick.y.ReadValue() + nl +
                                         "Right Stick UP: " + gamepad.rightStick.up.ReadValue() + nl +
                                         "Right Stick Down: " + gamepad.rightStick.down.ReadValue() + nl +
                                         "Right Stick Left: " + gamepad.rightStick.left.ReadValue() + nl +
                                         "Right Stick Right: " + gamepad.rightStick.right.ReadValue() + nl;
            Controller_Shoulder_Trigger.text = "Left Shoulder: " + gamepad[GamepadButton.LeftShoulder].isPressed + nl +
                                               "Right Shoulder: " + gamepad[GamepadButton.RightShoulder].isPressed + nl +
                                               "Left Trigger: " + gamepad.leftTrigger.ReadValue() + nl +
                                               "Right Trigger: " + gamepad.rightTrigger.ReadValue() + nl +
                                               "Start: " + gamepad[GamepadButton.Start].isPressed + nl +
                                               "Select: " + gamepad[GamepadButton.Select].isPressed + nl;

            if (gamepad.device.name.Contains("DualShockGamepadHID"))
            {
                Controller_Device_Name.text = "Sony Playstation DualShock";
                DualShock_Controller.SetActive(true);
                XBOX_Controller.SetActive(false);
            }
            else
            if (gamepad.device.name.Contains("XInputControllerWindows"))
            {
                Controller_Device_Name.text = "Xbox(Any Controller with XInput)";
                DualShock_Controller.SetActive(false);
                XBOX_Controller.SetActive(true);
            }
            else
            if (gamepad.device.name.Contains("Nintendo Wireless Gamepad"))
            {
                Controller_Device_Name.text = "Nintendo Switch Joycon/Pro";
                DualShock_Controller.SetActive(false);
                XBOX_Controller.SetActive(false);
            }
        }
        else
        {
            Information.text = "NULL";
            Controller_Dpad.text = "NULL";
            Controller_Buttons.text = "NULL";
            Controller_LeftStick.text = "NULL";
            Controller_RightStick.text = "NULL";
            Controller_Shoulder_Trigger.text = "NULL";
            Controller_Device_Name.text = "NULL";
        }

        InputSystemUpdate();
    }

    void InputSystemUpdate()
    {
        var nl = Environment.NewLine;
        Input_System.text = "Devices.Count: " + InputSystem.devices.Count.ToString() + nl +
                            "DisconnectedDevices.Count: " + InputSystem.disconnectedDevices.Count.ToString();

        Connected_Devices_List.RemoveRange(0, Connected_Devices_List.Count);
        Controller_Dropdown.options.RemoveRange(0, Controller_Dropdown.options.Count);
        Controller_Devices_List.text = "";
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Connected_Devices_List.Add(Gamepad.all[i]);
            Controller_Devices_List.text += Gamepad.all[i].id + ": " + Gamepad.all[i].name + nl;
            TMP_Dropdown.OptionData temp = new TMP_Dropdown.OptionData(Gamepad.all[i].id.ToString());
            Controller_Dropdown.options.Add(temp);
        }
        Controller_Dropdown.RefreshShownValue();

        InputSystem.pollingFrequency = 120;
        InputSystem.Update();
    }
}
