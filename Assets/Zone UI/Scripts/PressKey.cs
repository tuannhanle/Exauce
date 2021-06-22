using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Michsky.UI.Zone
{
    public class PressKey : MonoBehaviour
    {
        [Header("KEY")]
        [SerializeField]
        public bool pressAnyKey;
        public bool invokeAtStart;

        [Header("KEY ACTION")]
        [SerializeField]
        public UnityEvent pressAction;

        void Start()
        {
            
            if (invokeAtStart == true)
                pressAction.Invoke();
        }
        private void CallSomething(Vector2 touch)
        {
            Debug.Log($"Touch Screen Position: {touch}");
            var world = Camera.main.ScreenToWorldPoint(touch);
            Debug.Log($"Touch World Position: {world}");
            Debug.DrawLine(world, world + Vector3.one, Color.magenta, 5f);
        }
        void Update()
        {
            if(pressAnyKey == true)
            {
                if (Input.touchCount>0)
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
        public void Pressed()
        {
            pressAction.Invoke();

        }
    }

}