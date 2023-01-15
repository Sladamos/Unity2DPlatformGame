using System.Collections.Generic;
using NLog;
using UnityEngine;

namespace MIIProjekt.Parallax
{
    public class ParallaxManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private List<ParallaxLayer> ParallaxItems { get; } = new();

        private Vector2 lastTransformPosition;

        [SerializeField]
        private Transform cameraTransform;

        private void Awake()
        {
            if (cameraTransform == null)
            {
                Logger.Warn("Camera transform is not set on Parallax instance! GameObject name: {}", name);
            }

            lastTransformPosition = cameraTransform.position;
        }

        private void Start()
        {
            foreach (var child in transform)
            {
                if (child is Transform childTransform)
                {
                    var parallaxItem = childTransform.GetComponent<ParallaxLayer>();
                    if (parallaxItem != null)
                    {
                        ParallaxItems.Add(parallaxItem);
                    }
                }
            }

            Logger.Debug("Found {} ParallaxItems", ParallaxItems.Count);
        }

        private void FixedUpdate()
        {
            Vector2 currentPosition = cameraTransform.position;
            Vector2 difference = lastTransformPosition - currentPosition;
            foreach (var item in ParallaxItems)
            {
                item.UpdatePosition(difference);
            }

            lastTransformPosition = currentPosition;
        }
    }
}
