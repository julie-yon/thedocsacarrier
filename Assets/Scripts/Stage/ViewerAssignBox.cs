using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Michsky.UI.ModernUIPack;

namespace Docsa.Events
{
    public class ViewerAssignBox : MonoBehaviour
    {
        public void Open()
        {
            ESCUIManager.instance.OnESCPerformed(new UnityEngine.InputSystem.InputAction.CallbackContext());
            ESCUIManager.instance.GetComponentInChildren<WindowManager>().currentWindowIndex = 1;
        }
    }
}