using System;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public abstract class MiniGame : MonoBehaviour {

#region Events

		public Action? AllMiniGameTasksCompleted;

#endregion

#region Methods

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

#endregion

	}

}