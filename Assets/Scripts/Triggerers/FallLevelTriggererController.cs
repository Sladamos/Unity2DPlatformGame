using UnityEngine;

namespace MIIProjekt.Triggerers
{
    public class FallLevelTriggererController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.SendMessage("GetHit");
            }
        }
    }
}
