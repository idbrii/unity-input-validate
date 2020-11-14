// The MIT License
//
// Copyright (c) 2015 Cristian Alexandru Geambasu
// Copyright (c) 2020 David Briscoe
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.


using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace idbrii.InputValidation
{
    public static class GamepadMappings
    {
        public class Axis
        {
            public string name;
            public int id;
        }
        public class Button
        {
            public string name;
            public KeyCode key;
        }
        public class Mapping
        {
            public int m_GamepadId = 0;
            public List<Axis> m_Axes;
            public List<Button> m_Buttons;

            public float GetAxis(string id)
            {
                var axis = m_Axes.FirstOrDefault(a => a.name == id);
                if (axis == null || axis.id <= 0)
                {
                    return 0f;
                }
                return Input.GetAxis($"joy_{m_GamepadId}_axis_{axis.id}");
            }

            // Pressed
            public bool GetButtonDown(string id)
            {
                var btn = m_Buttons.FirstOrDefault(a => a.name == id);
                if (btn == null)
                {
                    return false;
                }
                return Input.GetKeyDown(btn.key);
            }

            // Held
            public bool GetButton(string id)
            {
                var btn = m_Buttons.FirstOrDefault(a => a.name == id);
                if (btn == null)
                {
                    return false;
                }
                return Input.GetKey(btn.key);
            }

            // Released
            public bool GetButtonUp(string id)
            {
                var btn = m_Buttons.FirstOrDefault(a => a.name == id);
                if (btn == null)
                {
                    return false;
                }
                return Input.GetKeyUp(btn.key);
            }

        }

#if false
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        public static Mapping Create_Xinput() { // XBox 360 OSX
            return new Mapping{

                m_Buttons = new List<Button>
                {
                    new Button{ name="Button A",           key=KeyCode.JoystickButton1 },
                    new Button{ name="Button B",           key=KeyCode.JoystickButton2 },
                    new Button{ name="Button X",           key=KeyCode.JoystickButton4 },
                    new Button{ name="Button Y",           key=KeyCode.JoystickButton5 },
                    new Button{ name="Left Bumper",        key=KeyCode.JoystickButton7 },
                    new Button{ name="Right Bumper",       key=KeyCode.JoystickButton8 },
                    new Button{ name="Back",               key=KeyCode.JoystickButton16 },
                    new Button{ name="Start",              key=KeyCode.JoystickButton12 },
                    new Button{ name="Left Stick Button",  key=KeyCode.JoystickButton14 },
                    new Button{ name="Right Stick Button", key=KeyCode.JoystickButton15 },
                 },

                m_Axes = new List<Axis>
                {
                    new Axis{ name="Left Stick Vertical",    id=1 },
                    new Axis{ name="Left Stick Horizontal",  id=0 },
                    new Axis{ name="Right Stick Vertical",   id=3 },
                    new Axis{ name="Right Stick Horizontal", id=2 },
                    new Axis{ name="Left Trigger",           id=4 },
                    new Axis{ name="Right Trigger",          id=5 },
                    new Axis{ name="DPAD Vertical",          id=-1  }, // "dpad is 5, but 5 often gets stuck at -1."
                    new Axis{ name="DPAD Horizontal",        id=-1  }, // "dpad is 4, but we can't use DPAD Vertical."
                }
            };
        }

        public static Mapping Create_DualShock() { // DualShock 4 OSX
            return new Mapping{

                m_Buttons = new List<Button>
                {
                    new Button{ name="Button A",           key=KeyCode.JoystickButton1 },
                    new Button{ name="Button B",           key=KeyCode.JoystickButton2 },
                    new Button{ name="Button X",           key=KeyCode.JoystickButton0 },
                    new Button{ name="Button Y",           key=KeyCode.JoystickButton3 },
                    new Button{ name="Left Bumper",        key=KeyCode.JoystickButton4 },
                    new Button{ name="Right Bumper",       key=KeyCode.JoystickButton5 },
                    new Button{ name="Back",               key=KeyCode.JoystickButton8 },
                    new Button{ name="Start",              key=KeyCode.JoystickButton9 },
                    new Button{ name="Left Stick Button",  key=KeyCode.JoystickButton10 },
                    new Button{ name="Right Stick Button", key=KeyCode.JoystickButton11 },
                 },

                m_Axes = new List<Axis>
                {
                    new Axis{ name="Left Stick Vertical",    id=1 },
                    new Axis{ name="Left Stick Horizontal",  id=0 },
                    new Axis{ name="Right Stick Vertical",   id=3 },
                    new Axis{ name="Right Stick Horizontal", id=2 },
                    new Axis{ name="Left Trigger",           id=4 },
                    new Axis{ name="Right Trigger",          id=5 },
                    new Axis{ name="DPAD Vertical",          id=11  }, // "Bluetooth uses 11, but wired uses 7."
                    new Axis{ name="DPAD Horizontal",        id=10  }, // "Bluetooth uses 10, but wired uses 6."
                }
            };
        }


#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        public static Mapping Create_Xinput() { // XBox 360 Windows
            return new Mapping{

                m_Buttons = new List<Button>
                {
                    new Button{ name="Button A",           key=KeyCode.JoystickButton0 },
                    new Button{ name="Button B",           key=KeyCode.JoystickButton1 },
                    new Button{ name="Button X",           key=KeyCode.JoystickButton2 },
                    new Button{ name="Button Y",           key=KeyCode.JoystickButton3 },
                    new Button{ name="Left Bumper",        key=KeyCode.JoystickButton4 },
                    new Button{ name="Right Bumper",       key=KeyCode.JoystickButton5 },
                    new Button{ name="Back",               key=KeyCode.JoystickButton6 },
                    new Button{ name="Start",              key=KeyCode.JoystickButton7 },
                    new Button{ name="Left Stick Button",  key=KeyCode.JoystickButton8 },
                    new Button{ name="Right Stick Button", key=KeyCode.JoystickButton9 },
                 },

                m_Axes = new List<Axis>
                {
                    new Axis{ name="Left Stick Vertical",    id=1 },
                    new Axis{ name="Left Stick Horizontal",  id=0 },
                    new Axis{ name="Right Stick Vertical",   id=4 },
                    new Axis{ name="Right Stick Horizontal", id=3 },
                    new Axis{ name="Left Trigger",           id=8 },
                    new Axis{ name="Right Trigger",          id=9 },
                    new Axis{ name="DPAD Vertical",          id=6 },
                    new Axis{ name="DPAD Horizontal",        id=5 },
                }
            };
        }

#elif UNITY_IOS || UNITY_TVOS
        public static Mapping Create_Xinput() {
            return Create_mfi();
        }

        public static Mapping Create_DualShock() {
            return Create_mfi();
        }

        static Mapping Create_mfi() { // Apple MFi Gamepad
            return new Mapping{

                m_Buttons = new List<Button>
                {
                    new Button{ name="Button A",           key=KeyCode.JoystickButton14 },
                    new Button{ name="Button B",           key=KeyCode.JoystickButton13 },
                    new Button{ name="Button X",           key=KeyCode.JoystickButton15 },
                    new Button{ name="Button Y",           key=KeyCode.JoystickButton12 },
                    new Button{ name="Left Bumper",        key=KeyCode.JoystickButton8 },
                    new Button{ name="Right Bumper",       key=KeyCode.JoystickButton9 },
                    new Button{ name="Back",               key=KeyCode.None   }, // "XboxOne starts screen recording, but game gets nothing."
                    new Button{ name="Start",              key=KeyCode.JoystickButton0 },
                    new Button{ name="Left Stick Button",  key=KeyCode.JoystickButton17 },
                    new Button{ name="Right Stick Button", key=KeyCode.JoystickButton18 },
                    new Button{ name="DPAD Up",            key=KeyCode.JoystickButton4 },
                    new Button{ name="DPAD Down",          key=KeyCode.JoystickButton6 },
                    new Button{ name="DPAD Left",          key=KeyCode.JoystickButton7 },
                    new Button{ name="DPAD Right",         key=KeyCode.JoystickButton5 },
                    new Button{ name="Left Trigger",       key=KeyCode.JoystickButton10 },
                    new Button{ name="Right Trigger",      key=KeyCode.JoystickButton11 },
                 },

                m_Axes = new List<Axis>
                {
                    new Axis{ name="Left Stick Vertical",    id=1 },
                    new Axis{ name="Left Stick Horizontal",  id=0 },
                    new Axis{ name="Right Stick Vertical",   id=3 },
                    new Axis{ name="Right Stick Horizontal", id=2 },
                }
            };
        }

#elif UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        public static Mapping Create_Xinput() { // XBox 360 Linux
            return new Mapping{

                m_Buttons = new List<Button>
                {
                    new Button{ name="Button A",           key=KeyCode.JoystickButton0 },
                    new Button{ name="Button B",           key=KeyCode.JoystickButton1 },
                    new Button{ name="Button X",           key=KeyCode.JoystickButton2 },
                    new Button{ name="Button Y",           key=KeyCode.JoystickButton3 },
                    new Button{ name="Left Bumper",        key=KeyCode.JoystickButton4 },
                    new Button{ name="Right Bumper",       key=KeyCode.JoystickButton5 },
                    new Button{ name="Back",               key=KeyCode.JoystickButton6 },
                    new Button{ name="Start",              key=KeyCode.JoystickButton7 },
                    new Button{ name="Left Stick Button",  key=KeyCode.JoystickButton9 },
                    new Button{ name="Right Stick Button", key=KeyCode.JoystickButton10 },
                 },

                m_Axes = new List<Axis>
                {
                    new Axis{ name="Left Stick Vertical",    id=1 },
                    new Axis{ name="Left Stick Horizontal",  id=0 },
                    new Axis{ name="Right Stick Vertical",   id=4 },
                    new Axis{ name="Right Stick Horizontal", id=3 },
                    new Axis{ name="Left Trigger",           id=2 },
                    new Axis{ name="Right Trigger",          id=5 },
                    new Axis{ name="DPAD Vertical",          id=7 },
                    new Axis{ name="DPAD Horizontal",        id=6 },
                }
            };
        }

        public static Mapping Create_DualShock() {
            // Not really the same, but whatever.
            return Create_XBox_Wireless();
        }

        static Mapping Create_XBox_Wireless() { // XBox 360 Linux Wireless
            return new Mapping{

                m_Buttons = new List<Button>
                {
                    new Button{ name="Button A",           key=KeyCode.JoystickButton0 },
                    new Button{ name="Button B",           key=KeyCode.JoystickButton1 },
                    new Button{ name="Button X",           key=KeyCode.JoystickButton2 },
                    new Button{ name="Button Y",           key=KeyCode.JoystickButton3 },
                    new Button{ name="Left Bumper",        key=KeyCode.JoystickButton4 },
                    new Button{ name="Right Bumper",       key=KeyCode.JoystickButton5 },
                    new Button{ name="Back",               key=KeyCode.JoystickButton6 },
                    new Button{ name="Start",              key=KeyCode.JoystickButton7 },
                    new Button{ name="Left Stick Button",  key=KeyCode.JoystickButton9 },
                    new Button{ name="Right Stick Button", key=KeyCode.JoystickButton10 },
                    new Button{ name="DPAD Up",            key=KeyCode.JoystickButton13 },
                    new Button{ name="DPAD Down",          key=KeyCode.JoystickButton14 },
                    new Button{ name="DPAD Left",          key=KeyCode.JoystickButton11 },
                    new Button{ name="DPAD Right",         key=KeyCode.JoystickButton12 },
                 },

                m_Axes = new List<Axis>
                {
                    new Axis{ name="Left Stick Vertical",    id=1 },
                    new Axis{ name="Left Stick Horizontal",  id=0 },
                    new Axis{ name="Right Stick Vertical",   id=4 },
                    new Axis{ name="Right Stick Horizontal", id=3 },
                    new Axis{ name="Left Trigger",           id=2 },
                    new Axis{ name="Right Trigger",          id=5 },
                }
            };
        }
#endif

    }
}
