using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MIIProjekt.UI
{
    public class DrawingManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text checkpointsText;

        [SerializeField]
        private TMP_Text highTemperaturesText;

        [SerializeField]
        private TMP_Text dashTutorialText;

        [SerializeField]
        private TMP_Text jumpTutorialText;

        [SerializeField]
        private TMP_Text longJumpTutorialText;

        public void DrawDashTutorialText()
        {
            DrawFadedText(dashTutorialText, 4.0f, 0.75f);
        }

        public void DrawJumpTutorialText()
        {
            DrawFadedText(jumpTutorialText, 4.0f, 0.75f);
        }

        public void DrawLongJumpTutorialText()
        {
            DrawFadedText(longJumpTutorialText, 4.0f, 0.75f);
        }

        public void DrawHighTemperaturesText()
        {
            DrawFadedText(highTemperaturesText, 3.0f, 1.0f);
        }

        public void DrawCheckpointsText(Vector2 trash)
        {
            DrawFadedText(checkpointsText, 1.5f, 0.7f);
        }

        private void DrawFadedText(TMP_Text text, float duration, float fadeTime)
        {
            StartCoroutine(FadeInText(text, fadeTime));
            StartCoroutine(DrawText(text, duration, fadeTime));
            StartCoroutine(FadeOutText(text, fadeTime, fadeTime + duration));
        }

        private IEnumerator DrawText(TMP_Text textToDraw, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            yield break;
        }

        private IEnumerator FadeInText(TMP_Text text, float fadeTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.gameObject.SetActive(true);
            float timeSpeed = 1 / fadeTime;
            while (text.color.a < 1.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
                yield return null;
            }
        }

        private IEnumerator FadeOutText(TMP_Text text, float fadeTime, float delay)
        {
            yield return new WaitForSeconds(delay);
            float timeSpeed = text.color.a / fadeTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
            while (text.color.a > 0.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
                yield return null;
            }
            text.gameObject.SetActive(false);
        }
    }
}
