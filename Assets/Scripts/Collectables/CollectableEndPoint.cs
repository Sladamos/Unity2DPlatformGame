using System.Collections.Generic;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.Collectables
{
    public class CollectableEndPoint : MonoBehaviour, ICollectableTriggerTarget
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private List<ICollectable> Collectables { get; } = new();

        private float invokeTimestamp = 0.0f;

        [Header("CollectableSettings")]
        [SerializeField]
        private float collectableDelayPerSecond = 0.4f;

        [SerializeField]
        private float collectableAcceleration = 15f;

        [SerializeField]
        private float collectableContactDistance = 0.2f;

        public void InvokeCollectableTarget(List<ICollectable> collectables)
        {
            this.Collectables.AddRange(collectables);
            invokeTimestamp = Time.time;

            if (Collectables.Count == 0)
            {
                AllArrived();
            }
        }

        private void OnKeyArrived(ICollectable collectable)
        {
            collectable.Active = false;
            Collectables.Remove(collectable);
            Logger.Trace("Key {} arrived at door {}", collectable, this);
            SendMessage("CollectableArrived", collectable);

            if (Collectables.Count == 0)
            {
                AllArrived();
            }
        }

        private void AllArrived()
        {
            Logger.Debug("All collectables arrived. Sending message");
            SendMessage("AllCollectablesArrived");
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
                Vector2 difference = ((Vector2)transform.position) - Collectables[i].Position;
                float distanceLeft = difference.magnitude;
                float deltaTimeVelocityScalar = velocityScalar * Time.deltaTime;

                if (distanceLeft - collectableContactDistance < deltaTimeVelocityScalar)
                {
                    OnKeyArrived(Collectables[i]);
                }
                else
                {
                    Collectables[i].Position += difference.normalized * deltaTimeVelocityScalar;
                }
            }
        }
    }
}
