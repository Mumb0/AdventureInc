using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class MiniGameManager : MonoBehaviour, IMiniGameTracker {

#region Fields

		[SerializeField] private MiniGame[] availableMiniGames = Array.Empty<MiniGame>();

#endregion

#region Properties

		public IReadOnlyList<IMiniGame> AllMiniGames => availableMiniGames;

#endregion

	}

}