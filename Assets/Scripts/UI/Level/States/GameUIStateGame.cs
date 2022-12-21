using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level.States
{
    public class GameUIStateGame : StateMachineBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Trace("UI state Game entered");
            var gameObject = animator.GetComponent<UIManager>()?.UIObjectGame;

            // TODO: THIS IS INVALID, REMOVE!
            // TODO: Use TimeManager instead
            Time.timeScale = 1.0f;

            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
            else
            {
                Logger.Warn("Cannot set Game UI GameObject as active");
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Trace("UI state Game exited");
            var gameObject = animator.GetComponent<UIManager>()?.UIObjectGame;
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Logger.Warn("Cannot set Game UI GameObject as inactive");
            }
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
