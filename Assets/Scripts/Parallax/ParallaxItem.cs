using NLog;
using UnityEngine;

namespace MIIProjekt.Parallax
{
    public class ParallaxItem : MonoBehaviour
    {
        private const float InvalidDepth = Mathf.Infinity;

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private float customDepth = InvalidDepth;

        [SerializeField]
        private bool isActive = true;

        private float Depth => customDepth == InvalidDepth ? transform.position.z : customDepth;

        public void UpdatePosition(Vector2 delta)
        {
            if (!isActive)
            {
                return;
            }

            transform.position -= (Vector3)(delta * Depth);
            Logger.Trace("{} UpdatePosition: {}", name, delta);
        }
    }
}
