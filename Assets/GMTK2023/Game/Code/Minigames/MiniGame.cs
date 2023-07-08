using System;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public abstract class MiniGame : MonoBehaviour, IMiniGame{

#region Events

		public Action? AllMiniGameTasksCompleted;

#endregion

#region Fields

		[SerializeField] internal Canvas? miniGameCanvas;

#endregion

#region Methods

		private void Awake() {
			
		}

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

#endregion

	}

}