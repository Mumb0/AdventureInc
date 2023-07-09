using System;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class RestingPlace : MiniGame {

		public override bool IsCredible => true;

		public override void SetActive(bool state) {

			if (state) {
				playerActions!.SwitchCurrentActionMap("General");
				gameObject.transform.localPosition = new Vector2(0, 0);
				return;
			}

			gameObject.transform.localPosition = new Vector2(0, 10);
		}

		public override void OnAdventurerEntered() {
			Debug.Log("The Adventurer is at a resting place!");
		}

		public override void OnAdventurerLeft() {
			Debug.Log("The Adventurer left a resting place!");
		}

	}

}