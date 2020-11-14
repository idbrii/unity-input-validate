// The MIT License
//
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
using System.Text;
using TMPro;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace idbrii.InputValidation
{
    public class ShowAllInputs : MonoBehaviour
    {
        public int m_GamepadIndex = 0;
        public int m_MaxGamepadAxes = 6;
        public TextMeshProUGUI m_Axis;
        public TextMeshProUGUI m_Button;

        List<string> m_InputManagerAxes = new List<string>();

        StringBuilder m_sb = new StringBuilder();

        void Awake()
        {
            for (int i = 0; i < m_MaxGamepadAxes; ++i)
            {
                m_InputManagerAxes.Add(string.Concat("joy_", m_GamepadIndex, "_axis_", i));
            }
        }

        void Update()
        {
            m_sb.Clear();

            foreach (var axis in m_InputManagerAxes)
            {
                m_sb.Append(axis);
                m_sb.Append(": ");
                m_sb.AppendFormat("{0:F2}", Input.GetAxis(axis));
                m_sb.Append("\n");
            }

            m_Axis.text = m_sb.ToString();

            m_sb.Clear();

            for (int i = (int)KeyCode.JoystickButton0; i <= (int)KeyCode.JoystickButton19; ++i)
            {
                var key = (KeyCode)i;
                m_sb.Append(key);
                m_sb.Append(": ");
                m_sb.Append(GetButtonState(key));
                m_sb.Append("\n");
            }

            m_Button.text = m_sb.ToString();
        }


        string GetButtonState(KeyCode key)
        {
            string state = "";
            if (Input.GetKeyDown(key))
            {
                state += "Down";
            }
            if (Input.GetKey(key))
            {
                state += "Held";
            }
            if (Input.GetKeyUp(key))
            {
                state += "Up";
            }
            return state;
        }

    }
}
