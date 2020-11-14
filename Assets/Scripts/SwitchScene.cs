using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace idbrii.InputValidation
{
    public class SwitchScene : MonoBehaviour
    {
        public string m_Destination;

        public void RequestSwitch()
        {
            //~ Debug.Log($"Changing to scene {m_Destination}.", this);
            SceneManager.LoadScene(m_Destination);
        }
        
    }
}
