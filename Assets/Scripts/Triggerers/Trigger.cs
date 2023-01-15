using System.Collections;
using System.Collections.Generic;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;
using UnityEngine.Events;

namespace MIIProjekt.Triggerers
{
    public class Trigger : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private UnityEvent triggerFunction;

        [SerializeField]
        private UnityEvent triggerExitFunction;

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Logger.Debug("Player entered trigger {}", name);
                triggerFunction?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Logger.Debug("Player exited trigger {}", name);
                triggerExitFunction?.Invoke();
            }
        }
    }
}
