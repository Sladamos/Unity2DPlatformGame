using TMPro;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    [RequireComponent(typeof(TMP_Text))]
    public class CheckpointMessageDisplayer : MonoBehaviour
    {
        private TMP_Text text;

        private float timeDisplayActivated;

        [SerializeField]
        private float animationDuration = 1.0f;

        [SerializeField]
        private AnimationCurve visibilityAnimationCurve;

        public void Display()
        {
            timeDisplayActivated = Time.time;
        }

        private void Awake()
        {
            this.text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            float timeSinceDisplay = Time.time - timeDisplayActivated;
            float progressPercent = Mathf.Clamp01(timeSinceDisplay / animationDuration);
            float alpha = visibilityAnimationCurve.Evaluate(progressPercent);

            text.alpha = alpha;
        }
    }
}
