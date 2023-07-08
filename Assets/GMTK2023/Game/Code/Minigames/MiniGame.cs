using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public abstract class MiniGame : MonoBehaviour {

#region Methods

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

#endregion

	}

}