using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VRFI
{
   [RequireComponent(typeof(Rigidbody))]
    public class VRFI_Operater : MonoBehaviour
    {
        public VRFI_ControllerInput.Controllers ControllerSide = VRFI_ControllerInput.Controllers.right;

        [SerializeField] GameObject customColliderContainer;//自前のコントローラのCollider

        [SerializeField] GameObject VRFlickKeyBoard;

        private VRFI_ControllerInput controllerInput;
        private VRFI_InputTextHandler textHandler;
        private VRFI_KanjiConvert kanjiConvert;

        private GameObject defaultColliderContainer;

        private class Keyinfo
        {
            public Collider collider;
            public IFlickKeyAction FlickKey;
            public bool InOperation;
        }

        List<Keyinfo> OperatableKeys = new List<Keyinfo>();
        Keyinfo InOperationKey = null;

        void Start()
        {
            controllerInput = VRFlickKeyBoard.GetComponent<VRFI_ControllerInput>();
            textHandler = VRFlickKeyBoard.GetComponent<VRFI_InputTextHandler>();
            kanjiConvert = VRFlickKeyBoard.GetComponent<VRFI_KanjiConvert>();

            InitializeRigidbody();
            CreateCollider();
        }

        private void OnEnable()
        {
            defaultColliderContainer = Resources.Load("[VRFI]DefaultCollider", typeof(GameObject)) as GameObject;
        }

        void Update()
        {
            //グリップを握ったら文字を消す
            if (controllerInput.GetControllerGripDown(ControllerSide))
            {
                textHandler.EraseOneCharacter();
                kanjiConvert.KanjiConvert();
            }


            if (OperatableKeys.Count == 0) return;
            SelectKey();
            ReleaseKey();
        }

        void SelectKey()
        {
            //Operater(コントローラー)と一番っ距離が近いキーを操作対象とする
            if (controllerInput.GetControllerTriggerDown(ControllerSide))
            {
                float mindist = OperatableKeys.Min(x => Vector3.Distance(x.collider.transform.position, transform.position));

                InOperationKey = OperatableKeys.First(x => Vector3.Distance(x.collider.transform.position, transform.position) == mindist);
                InOperationKey.FlickKey.OnFlickStart(this);
                InOperationKey.InOperation = true;
            }
        }

        void ReleaseKey()
        {
            if (InOperationKey == null) return;

            if (controllerInput.GetControllerTriggerUp(ControllerSide))
            {
                InOperationKey.FlickKey.OnFlickEnd(this);

                OperatableKeys.RemoveAll(key => key == InOperationKey);
                InOperationKey = null;
            }
        }

        void InitializeRigidbody()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (!rigidbody) rigidbody = gameObject.AddComponent<Rigidbody>();

            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        void CreateCollider()
        {
            if (customColliderContainer == null)
            {
                if (defaultColliderContainer == null)
                {
                    Debug.LogError("gameobject not find of DefaultColliderContainer");
                    return;
                }
                GameObject ColliderObject = Instantiate(defaultColliderContainer, transform.position, transform.rotation);
                ColliderObject.transform.SetParent(transform);
                ColliderObject.transform.localScale = transform.localScale;
            }
            else
            {
                GameObject ColliderObject = Instantiate(customColliderContainer, transform.position, transform.rotation);
                ColliderObject.transform.SetParent(transform);
                ColliderObject.transform.localScale = transform.localScale;
            }
        }

        void OnTriggerStay(Collider collider)
        {
            IFlickKeyAction flickkey = collider.GetComponent<IFlickKeyAction>();
            if (flickkey == null) return;

            if (!OperatableKeys.Any(key => key.collider == collider))
            {
                Keyinfo info = new Keyinfo();
                info.collider = collider;
                info.FlickKey = flickkey;
                info.InOperation = false;
                OperatableKeys.Add(info);
            }
        }

        void OnTriggerExit(Collider collider)
        {
            OperatableKeys.RemoveAll(key => key.collider == collider && key.InOperation == false);
        }

        public Vector3 GetControllerPosition()
        {
            return controllerInput.GetHandPosition(ControllerSide);
        }

        public Vector2 GetPadTouchPoint()
        {
            return controllerInput.GetControllerTouchDownAxis(ControllerSide);
        }
    }
}


