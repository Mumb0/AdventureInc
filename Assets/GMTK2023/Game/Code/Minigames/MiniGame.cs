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
		[SerializeField] private string[] stepGuideTexts = Array.Empty<string>();

#endregion

#region Properties

		public bool IsPrepared { get; set; } = true;
		public int CurrentTaskStep { get; set; } = 0;
		public string[] StepTexts => stepGuideTexts;
		internal Camera? MainCamera { get; private set; }

#endregion

#region Methods

		private void Awake() {
			MainCamera = Camera.main;
		}

		public abstract void SetActive();

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

#endregion

	}

}