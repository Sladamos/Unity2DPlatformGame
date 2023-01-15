using NLog;
using UnityEngine;

namespace MIIProjekt.Parallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private float parallaxEffectMultiplier = 1.0f;

        [SerializeField]
        private bool isActive = true;

        public void UpdatePosition(Vector2 delta)
        {
            if (!isActive)
            {
                return;
            }

            transform.position -= (Vector3)(delta * parallaxEffectMultiplier);
            Logger.Trace("{} UpdatePosition: {}", name, delta);
        }
    }
}
