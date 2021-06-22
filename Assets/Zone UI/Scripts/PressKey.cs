using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Michsky.UI.Zone
{
    public class PressKey : MonoBehaviour
    {
        [Header("KEY")]
        [SerializeField]
        public KeyCode hotkey;
        public bool pressAnyKey;
        public bool invokeAtStart;

        [Header("KEY ACTION")]
        [SerializeField]
        public UnityEvent pressAction;

        void Start()
        {
            if(invokeAtStart == true)
                pressAction.Invoke();
        }

        void Update()
        {
            if(pressAnyKey == true)
            {
                if (Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed || Mouse.current.middleButton.isPressed)
                {
                    pressAction.Invoke();
                } 
            }

            //else
            //{
            //    if (Input.GetKeyDown(hotkey))
            //    {
            //        pressAction.Invoke();
            //    } 
            //}
        }
    }
}