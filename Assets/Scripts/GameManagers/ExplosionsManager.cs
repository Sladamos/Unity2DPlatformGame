using System.Collections;
using System.Collections.Generic;
using MIIProjekt.Player;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class ExplosionsManager : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem onGroundHit;

        [SerializeField]
        private PlayerController2 player;

        private void Awake()
        {
            player.PlayerLanded += OnGroundHit;
        }

        public void OnGroundHit(Vector2 groundPosition)
        {
            onGroundHit.transform.position = groundPosition;
            onGroundHit.Play();
        }
    }

}
