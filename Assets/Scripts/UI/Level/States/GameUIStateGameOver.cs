using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level.States
{
    public class GameUIStateGameOver : StateMachineBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Trace("UI state GameOver entered");
            var gameObject = animator.GetComponent<UIManager>()?.UIObjectGameOver;
            if (gameObject != null)
            {
                gameObject.SetActive(true); 
            }
            else
            {
                Logger.Warn("Cannot set GameOver UI GameObject as active");
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Trace("UI state GameOver exited");
            var gameObject = animator.GetComponent<UIManager>()?.UIObjectGameOver;
            if (gameObject != null)
            {
                gameObject.SetActive(false); 
            }
            else
            {
                Logger.Warn("Cannot set GameOver UI GameObject as inactive");
            }
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
