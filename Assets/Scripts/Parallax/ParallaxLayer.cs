using NLog;
using UnityEngine;

namespace MIIProjekt.Parallax
{
    public class ParallaxItem : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private float depth;

        [SerializeField]
        private bool isActive = true;

        public void UpdatePosition(Vector2 delta)
        {
            if (!isActive)
            {
                return;
            }

            transform.position -= (Vector3)(delta * depth);
            Logger.Trace("{} UpdatePosition: {}", name, delta);
        }
    }
}
