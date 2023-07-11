using System;
using System.Collections.Generic;

namespace GMTK2023.Game.MiniGames {

	public interface IMiniGame {


		public IActivity Activity { get; }
		

		public bool IsCredible { get; }
		

		public MiniGameTask[] MiniGameTasks { get; }

		public void SetActive(bool state);

		public void OnAdventurerEntered();

		public void OnAdventurerLeft();
		
		public event Action? AllMiniGameTasksCompleted;

		public event Action<int>? MiniGameTaskCompleted;

		public event Action? AdventurerEnteredUnpreparedRoom;

	}

	public interface IMiniGameTracker {

		public IReadOnlyList<IMiniGame> AllMiniGames { get; }

	}

}