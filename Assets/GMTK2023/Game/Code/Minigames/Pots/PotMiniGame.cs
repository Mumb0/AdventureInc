using System;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Methods

		public void Awake() {
			SetupRoom();
		}

		public void SetupRoom() { }

		public override void OnAdventurerEntered() {
			throw new NotImplementedException();
		}

		public override void OnAdventurerLeft() {
			throw new NotImplementedException();
		}

#endregion

	}

}