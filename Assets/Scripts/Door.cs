using UnityEngine;

namespace MIIProjekt
{
    public class Door : MonoBehaviour
    {
        private void OnAllKeysCollected()
        {
            Debug.Log("All keys collected. Opening door...");
            gameObject.SetActive(false);
        }
    }
}
