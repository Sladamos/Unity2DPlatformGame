using System.Collections.Generic;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.Platforms
{
    public class GeneratedPlatformsContainer : MonoBehaviour
    {
        private const float MaximumProgress = 1.0f;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private List<Transform> Platforms { get; } = new();

        private float progress;

        [SerializeField]
        private GameObject platformPrefab;

        [SerializeField]
        private float speed;

        [SerializeField]
        private float radius;

        [SerializeField]
        private int targetPlatformCount;

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            DestroyExistingPlatforms();
            GeneratePlatforms();
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                if (Platforms.Count != targetPlatformCount)
                {
                    DestroyExistingPlatforms();
                    GeneratePlatforms();
                }
            }
        }

        private void FixedUpdate()
        {
            progress += (Time.deltaTime * speed) % MaximumProgress;

            for (int i = 0; i < Platforms.Count; i++)
            {
                float iterProgress = (progress + ((float)i / (float)Platforms.Count)) % MaximumProgress;
                float angle = Mathf.PI * 2f * iterProgress;
                Platforms[i].localPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            }
        }

        private void GeneratePlatforms()
        {
            if (platformPrefab == null)
            {
                Logger.Error("Platform prefab is not set. Cannot generate platforms");
                return;
            }

            for (int i = 0; i < targetPlatformCount; i++)
            {
                GameObject gameObject = Instantiate(platformPrefab, Vector2.zero, Quaternion.identity);
                gameObject.transform.parent = transform;
                Platforms.Add(gameObject.transform);
            }
        }

        private void DestroyExistingPlatforms()
        {
            foreach (var platform in Platforms)
            {
                Destroy(platform.gameObject);
            }

            Platforms.Clear();
        }
    }
}
