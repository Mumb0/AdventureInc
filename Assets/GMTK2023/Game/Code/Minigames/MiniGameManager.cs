using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class MiniGameManager : MonoBehaviour, IMiniGameTracker {

#region Fields

		[SerializeField] private GameObject[] availableMiniGames = Array.Empty<GameObject>();

#endregion

#region Properties

		public IReadOnlyList<MiniGame> AllMiniGames { get; }

#endregion

	}

}