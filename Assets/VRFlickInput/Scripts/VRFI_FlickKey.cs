using UnityEngine;

namespace VRFI
{
    public class VRFI_FlickKey : MonoBehaviour,IFlickKeyAction
    {
        public VRFI_InputBlock.InputAlias inputBlock = VRFI_InputBlock.InputAlias.あいうえお;

        VRFI_InputTextHandler textHandler;
        VRFI_KanjiConvert kanjiConvert;

        private Vector3 FlickStartPositiontoLocal;
        private Vector3 FlickEndPositiontoLocal;

        void Awake()
        {
            textHandler = GetComponentInParent<VRFI_InputTextHandler>();
            kanjiConvert = GetComponentInParent<VRFI_KanjiConvert>();
        }

        public void OnFlickStart(VRFI_Operater operater)
        {
            Vector3 flickStartPos = operater.GetControllerPosition();

            FlickStartPositiontoLocal = transform.InverseTransformPoint(flickStartPos);

        }

        public void OnFlickEnd(VRFI_Operater operater)
        {
            Vector3 flickEndPos = operater.GetControllerPosition();
            Vector2 PadTouchPoint = operater.GetPadTouchPoint();

            FlickEndPositiontoLocal = transform.InverseTransformPoint(flickEndPos);

            //フリック方向を取得
            int direction = FlickDirection();
            //パッドのタッチ位置でJapaneseTypeを取得
            VRFI_InputBlock.KeyTemplates type = SelectJapaneseType(PadTouchPoint);

            //選択しているキーとパッドのタッチ位置で入力文字を変更
            int block = (int)inputBlock;
            string Input = "";
            switch (type)
            {
                case VRFI_InputBlock.KeyTemplates.Japanese_Normal:
                    Input = VRFI_InputBlock.DefaultCharacterBlock[block, direction];
                    break;
                case VRFI_InputBlock.KeyTemplates.Japanese_dakuten:
                    Input = VRFI_InputBlock.DakutenCharacterBlock[block, direction];
                    break;
                case VRFI_InputBlock.KeyTemplates.Japanese_handakuten:
                    Input = VRFI_InputBlock.HandakutenCharacterBlock[block, direction];
                    break;
                case VRFI_InputBlock.KeyTemplates.Japanese_komozi:
                    Input = VRFI_InputBlock.KomoziCharacterBlock[block, direction];
                    break;
            }

            FlickStartPositiontoLocal = Vector3.zero;
            FlickEndPositiontoLocal = Vector3.zero;

            textHandler.AddTemporaryText(Input);

            kanjiConvert.KanjiConvert();
        }

        private int FlickDirection()
        {
            float DifferenceX = FlickEndPositiontoLocal.x - FlickStartPositiontoLocal.x;
            float DifferenceY = FlickEndPositiontoLocal.y - FlickStartPositiontoLocal.y;
            float DistanceX = Mathf.Abs(DifferenceX);
            float DistanceY = Mathf.Abs(DifferenceY);

            if (DistanceX > DistanceY)
            {
                if (DistanceX < 0.7f) return 0;

                if (DifferenceX > 0) return 1;
                else return 3;
            }
            else
            {
                if (DistanceY < 0.7f) return 0;

                if (DifferenceY > 0) return 2;
                else return 4;
            }
        }

        private VRFI_InputBlock.KeyTemplates SelectJapaneseType(Vector2 TouchPoint)
        {
            float DistanceX = Mathf.Abs(TouchPoint.x);
            float DistanceY = Mathf.Abs(TouchPoint.y);

            if (DistanceX > DistanceY)
            {
                if (DistanceX < 0.5f) return VRFI_InputBlock.KeyTemplates.Japanese_Normal;

                if (TouchPoint.x > 0) return VRFI_InputBlock.KeyTemplates.Japanese_handakuten;
                else return VRFI_InputBlock.KeyTemplates.Japanese_dakuten;
            }
            else
            {
                if (DistanceY < 0.5f) return VRFI_InputBlock.KeyTemplates.Japanese_Normal;

                if (TouchPoint.y > 0) return VRFI_InputBlock.KeyTemplates.Japanese_Normal;
                else return VRFI_InputBlock.KeyTemplates.Japanese_komozi;
            }
        }
    }
}
