/*
Copyright(c) 2017 yutoVR

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace VRFI
{
    public class VRFI_KanjiConvert : MonoBehaviour
    {
        [SerializeField] List<VRFI_KanjiSelectKey> kanji = new List<VRFI_KanjiSelectKey>();

        VRFI_InputTextHandler textHandler;

        private void Awake()
        {
            textHandler = GetComponent<VRFI_InputTextHandler>();
        }

        public void KanjiConvert()
        {
            StartCoroutine(Convert());
        }

        IEnumerator Convert()
        {
            //変換可能な文字がない場合に空文字を入れる
            if (textHandler.GetTemporaryText().Length == 0)
            {
                for (int j = 0; j < kanji.Count; j++)
                {
                    kanji[j].SetKanjiConvert("", "");
                }

                yield break;
            }

            UnityWebRequest www = UnityWebRequest.Get("http://www.google.com/transliterate?langpair=ja-Hira|ja&text=" + WWW.EscapeURL(textHandler.GetTemporaryText()));

            yield return www.SendWebRequest();


            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //変換候補を取得
                string result = www.downloadHandler.text;

                //文節に分割
                var phrases = result.Split(new string[] { "]],[" }, System.StringSplitOptions.None);

                //不要な文字列（",[]）を削除
                for (int i = 0; i < phrases.Length; i++)
                {
                    phrases[i] = phrases[i].Replace("\"", "");
                    phrases[i] = phrases[i].Replace("[", "");
                    phrases[i] = phrases[i].Replace("]", "");
                }

                //変換候補
                List<string> candidates = new List<string>(phrases[0].Split(','));

                //変換前文字列
                string original = candidates[0];

                //変換候補から変換前文字列を除去
                candidates.RemoveAt(0);

                //変換候補を TextMesh kanji に並べる
                for (int j = 0; j < candidates.Count; j++)
                {
                        kanji[j].SetKanjiConvert(candidates[j], original);
                }

                yield return null;
            }
        }
    }
} 
