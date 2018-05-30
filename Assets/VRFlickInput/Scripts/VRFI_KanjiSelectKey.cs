using UnityEngine;

namespace VRFI
{
    public class VRFI_KanjiSelectKey : MonoBehaviour, IFlickKeyAction
    {
        VRFI_InputTextHandler textHandler;
        VRFI_KanjiConvert kanjiConvert;

        TextMesh Displaytext;
        private string RawText;

        private string BeforeConvertString = "";

        public void Awake()
        {
            textHandler = GetComponentInParent<VRFI_InputTextHandler>();
            kanjiConvert = GetComponentInParent<VRFI_KanjiConvert>();

            Displaytext = gameObject.GetComponent<TextMesh>();
        }

        public void OnFlickStart(VRFI_Operater operater)
        {
            if (BeforeConvertString.Length == 0) return;

            textHandler.OverwiteKanjiConvert(BeforeConvertString.Length, RawText);
            BeforeConvertString = "";

            kanjiConvert.KanjiConvert();
        }

        public void OnFlickEnd(VRFI_Operater operater)
        {

        }

        public void SetKanjiConvert(string kanji,string BeforeConvert)
        {
            RawText = kanji;
            //7文字を超えると省略する
            if (RawText.Length >= 7)
            {
                Displaytext.text = RawText.Substring(0, 6) + "...";
            }
            else
            {
                Displaytext.text = kanji;
            }

            BeforeConvertString = BeforeConvert;
        }
    }
}
