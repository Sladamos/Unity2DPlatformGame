using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.Enemy.Eagle
{
    public class EagleStateDead : StateMachineBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Debug("Eagle entered dead state.");
            animator.gameObject.SetActive(false);
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
