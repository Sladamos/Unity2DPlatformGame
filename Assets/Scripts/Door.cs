using System.Collections.Generic;
using MIIProjekt.Collectables;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class Door : MonoBehaviour, ICollectableTriggerTarget
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private List<ICollectable> Collectables { get; } = new();

        private float invokeTimestamp = 0.0f;

        [Header("CollectableSettings")]
        [SerializeField]
        private float collectableDelayPerSecond = 0.4f;

        [SerializeField]
        private float collectableAcceleration = 2.0f;

        [SerializeField]
        private float collectableContactDistance = 1.0f;

        public void InvokeCollectableTarget(List<ICollectable> collectables)
        {
            this.Collectables.AddRange(collectables);
            invokeTimestamp = Time.time;
        }

        private void OnKeyArrived(ICollectable collectable)
        {
            collectable.Active = false;
            Collectables.Remove(collectable);
            Logger.Trace("Key {} arrived at door {}", collectable, this);

            if (Collectables.Count == 0)
            {
                DisableDoor();
            }
        }

        private void DisableDoor()
        {
            Logger.Info("All keys collected. Opening door...");
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        private void Update()
        {
            for (int i = 0; i < Collectables.Count; i++)
            {
                float timeSinceInvoke = Time.time - invokeTimestamp;
                float velocityScalar = Mathf.Max(0.0f, timeSinceInvoke - (i * collectableDelayPerSecond)) * collectableAcceleration; 
                Vector2 difference = transform.position - Collectables[i].Transform.position;
                float distanceLeft = difference.magnitude;
                float deltaTimeVelocityScalar = velocityScalar * Time.deltaTime;

                if (distanceLeft - collectableContactDistance < deltaTimeVelocityScalar)
                {
                    OnKeyArrived(Collectables[i]);
                }
                else
                {
                    Collectables[i].Transform.position += (Vector3)(difference.normalized * deltaTimeVelocityScalar);
                }
            }
        }
    }
}
