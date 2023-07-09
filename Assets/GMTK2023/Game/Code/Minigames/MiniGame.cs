using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2023.Game.MiniGames {

	public abstract class MiniGame : MonoBehaviour, IMiniGame {

#region Events

		public event Action? AllMiniGameTasksCompleted;
		public event Action<int>? MiniGameTaskCompleted;
		public event Action? AdventurerEnteredUnpreparedRoom;

#endregion

#region Fields

		[SerializeField] internal Canvas? miniGameCanvas;
		[SerializeField] internal PlayerInput? playerActions;
		[SerializeField] private MiniGameTask[] miniGameTasks = Array.Empty<MiniGameTask>();
		[SerializeField] private float questDurationInSeconds;

#endregion

#region Properties

		public bool IsPrepared { get; set; } = true;
		public abstract bool IsCredible { get; }

		// NOTE: We re-use the game-objects name for the mini-games name
		public string Name => gameObject.name;
		public TimeSpan Duration => TimeSpan.FromSeconds(questDurationInSeconds);
		public int CurrentTaskStep { get; set; } = 0;
		public MiniGameTask[] MiniGameTasks => miniGameTasks;
		public Camera? MainCamera { get; private set; }

#endregion

#region Methods

		private void Awake() {
			MainCamera = Camera.main;
		}

		public abstract void SetActive(bool state);

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

		protected virtual void OnTasksCompleted() {
			AllMiniGameTasksCompleted?.Invoke();
		}

		protected virtual void OnAdventurerEnteredUnpreparedRoom() {
			AdventurerEnteredUnpreparedRoom?.Invoke();
		}

		protected virtual void OnMiniGameTaskCompleted(int currentTaskStep) {
			MiniGameTaskCompleted?.Invoke(CurrentTaskStep);
		}

#endregion

	}

}