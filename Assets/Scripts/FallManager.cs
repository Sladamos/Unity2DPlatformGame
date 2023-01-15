using MIIProjekt.Player;
using NLog;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

    public void OnTrigger(Collider2D collider)
    {
        var playerLife = collider.GetComponent<PlayerLife>();
        if (playerLife == null)
        {
            Logger.Warn("No PlayerLife found on instance {}", collider.name);
            return;
        }

        playerLife.GetHit();
    }
}
