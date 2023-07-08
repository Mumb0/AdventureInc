using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class Pot : MonoBehaviour {

#region Fields

		[SerializeField] private GameObject? potPiecePrefab;
		[SerializeField] private float pieceRadius;

#endregion

#region Properties

		public bool IsFilledWithCoins { get; set; }

#endregion

#region Methods

		public void Smash() {

			int brokenPieces = Random.Range(3, 6);

			for (int i = 0; i < brokenPieces; i++) {
				
			}

		}

#endregion

	}

}