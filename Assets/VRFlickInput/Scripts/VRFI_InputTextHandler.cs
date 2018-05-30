using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace VRFI
{
    public class VRFI_InputTextHandler : MonoBehaviour
    {
        [SerializeField] TextMesh Text;
        private StringBuilder FixidText;　　　//変換前文字列
        private StringBuilder TemporaryText; //変換後文字列

        [System.Serializable] public class UpdateText : UnityEvent<string> { }

        public UpdateText updateText;
        public UnityEvent onStringDelete;
        
        void Start()
        {
            FixidText = new StringBuilder();
            TemporaryText = new StringBuilder();
            TemporaryText.Append(Text.text);
        }

        public void AddTemporaryText(string input)
        {
            TemporaryText.Append(input);

            Text.text = GetInputText();
        }

        public void AddFixidText(string input)
        {
            FixidText.Append(input);

            Text.text = GetInputText();
        }

        public void OverwiteKanjiConvert(int delLength, string kanji)
        {
            TemporaryText.Remove(0, delLength);
            AddFixidText(kanji);
        }

        public void EraseOneCharacter()
        {
            //TemporaryTextから消していく
            if (TemporaryText.Length != 0) TemporaryText.Remove(TemporaryText.Length - 1, 1);
            else if(FixidText.Length != 0) FixidText.Remove(FixidText.Length - 1, 1);
            Text.text = GetInputText();

            onStringDelete.Invoke();
        }

        public string GetInputText()
        {
            StringBuilder Inputstring = new StringBuilder();
            Inputstring.Append(FixidText.ToString());
            Inputstring.Append(TemporaryText.ToString());

            updateText.Invoke(Inputstring.ToString());

            return Inputstring.ToString();
        }

        public string GetTemporaryText()
        {
            return TemporaryText.ToString();
        }
    }
}
