using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2023.Game.MiniGames
{
    public class TorchesMiniGameController : MiniGame
    {
        private LinearLerper[] torchLerpers =
            Array.Empty<LinearLerper>();

        public override bool IsCredible =>
            // All torches must be lit
            torchLerpers.All(it => it.T > 0);


        public override void SetActive(bool state)
        {
            if (state)
            {
                playerActions!.SwitchCurrentActionMap("Torches");
                gameObject.transform.localPosition = Vector3.zero;
                return;
            }

            gameObject.transform.localPosition = Origin;
        }

        public override void OnAdventurerEntered()
        {
        }

        public override void OnAdventurerLeft()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            torchLerpers = GetComponentsInChildren<LinearLerper>();
        }
    }
}