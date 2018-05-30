
using UnityEngine;

namespace VRFI
{
    public class VRFI_ControllerInput : MonoBehaviour
    {
        [SerializeField] private SteamVR_TrackedObject rightControllerTracked;
        [SerializeField] private SteamVR_TrackedObject leftControllerTracked;

        public enum Controllers
        {
            right,
            left,
        }

        public Vector3 GetHandPosition(Controllers controller)
        {
            if(controller == Controllers.right)
            {
                return rightControllerTracked.transform.position;
            }
            else
            {
                return leftControllerTracked.transform.position;
            }
        }

        public bool GetControllerTriggerDown(Controllers controller)
        {
            bool input = false;
            if (controller == Controllers.right)
            {
                if (rightControllerTracked.gameObject.activeInHierarchy)
                {
                    var rightdevice = SteamVR_Controller.Input((int)rightControllerTracked.index);

                    input = rightdevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
                }
            }
            else
            {
                if (leftControllerTracked.gameObject.activeInHierarchy)
                {
                    var leftdevice = SteamVR_Controller.Input((int)leftControllerTracked.index);

                    input = leftdevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
                }
            }
            return input;
        }

        public bool GetControllerTriggerUp(Controllers controller)
        {
            bool input = false;
            if (controller == Controllers.right)
            {
                if (rightControllerTracked.gameObject.activeInHierarchy)
                {
                    var rightdevice = SteamVR_Controller.Input((int)rightControllerTracked.index);

                    input = rightdevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
                }
            }
            else
            {
                if (leftControllerTracked.gameObject.activeInHierarchy)
                {
                    var leftdevice = SteamVR_Controller.Input((int)leftControllerTracked.index);

                    input = leftdevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
                }
            }
            return input;
        }

        public Vector2 GetControllerTouchDownAxis(Controllers controller)
        {
            Vector2 input = Vector2.zero;
            if (controller == Controllers.right)
            {
                if (rightControllerTracked.gameObject.activeInHierarchy)
                {
                    var rightdevice = SteamVR_Controller.Input((int)rightControllerTracked.index);
                    input = rightdevice.GetAxis();
                }
            }
            else
            {
                if (leftControllerTracked.gameObject.activeInHierarchy)
                {
                    var leftdevice = SteamVR_Controller.Input((int)leftControllerTracked.index);
                    input = leftdevice.GetAxis();
                }
            }
            return input;
        }

        public bool GetControllerGripDown(Controllers controller)
        {
            bool input = false;
            if (controller == Controllers.right)
            {
                if (rightControllerTracked.gameObject.activeInHierarchy)
                {
                    var rightdevice = SteamVR_Controller.Input((int)rightControllerTracked.index);

                    input = rightdevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
                }
            }
            else
            {
                if (leftControllerTracked.gameObject.activeInHierarchy)
                {
                    var leftdevice = SteamVR_Controller.Input((int)leftControllerTracked.index);

                    input = leftdevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
                }
            }
            return input;
        }
    }
}

