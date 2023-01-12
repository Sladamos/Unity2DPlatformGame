using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MIIProjekt.UI
{
    public class DrawingManager : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text highTemperaturesText;

        public void DrawHighTemperaturesText()
        {
            DrawText(highTemperaturesText, 1.0f, 3.0f);
        }

        private void DrawText(TMP_Text textToDraw, float fadeTime, float duration)
        {
            textToDraw.gameObject.SetActive(true);
            Debug.Log("mimimimi");
            //textToDraw.
        }
    }
}
