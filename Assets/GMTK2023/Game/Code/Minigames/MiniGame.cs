using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2023.Game.MiniGames {

	public abstract class MiniGame : MonoBehaviour, IMiniGame {

#region Events

		public Action? AllMiniGameTasksCompleted;

#endregion

#region Fields

		[SerializeField] internal Canvas? miniGameCanvas;
		[SerializeField] internal PlayerInput? playerActions;

#endregion

#region Properties

		internal Camera? MainCamera { get; private set; }

#endregion

#region Methods

		private void Awake() {
			MainCamera = Camera.main;
		}

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

#endregion

	}

}