using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK2023.Game.MiniGames {

	public class Pot : MonoBehaviour {

#region Events

		public Action? CleanedALlPieces;
		public Action? PlacedPot;
		public Action? FilledPot;

#endregion

#region Fields

		[SerializeField] private SpriteRenderer? spriteRenderer;
		[SerializeField] private GameObject? potPiecePrefab;
		[SerializeField] private float pieceRadius;
		[SerializeField] private int minGeneratedPieces;
		[SerializeField] private int maxGeneratedPieces;
		[SerializeField] private Sprite? emptyPotSpaceSprite;
		[SerializeField] private Sprite? potSprite;
		[SerializeField] private Sprite? filledPotSprite;

#endregion

#region Properties

		public int BrokenPiecesCount { get; set; }
		public PotState CurrentState { get; set; }

#endregion

#region Methods

		public void Smash() {

			BrokenPiecesCount = Random.Range(minGeneratedPieces, maxGeneratedPieces);

			for (int i = 0; i < BrokenPiecesCount; i++) {
				Vector2 newPos = new Vector2(
						transform.position.x + Random.Range(-pieceRadius, pieceRadius),
						transform.position.y + Random.Range(-pieceRadius, pieceRadius)
					);

				PotPiece fallenPiece = Instantiate(potPiecePrefab, newPos, Quaternion.identity, transform)!.GetComponent<PotPiece>();
				fallenPiece.CleanedPiece += OnPieceCleaned;

			}

			spriteRenderer!.sprite = null;
			CurrentState = PotState.Broken;

		}

		public void OnPieceCleaned() {

			if (CurrentState == PotState.Broken) {

				BrokenPiecesCount--;

				if (BrokenPiecesCount <= 0) {
					spriteRenderer!.sprite = emptyPotSpaceSprite;
					CurrentState = PotState.Cleaned;
					CleanedALlPieces?.Invoke();
				}

			}

		}

		public void PlacePot() {

			if (CurrentState == PotState.Cleaned) {
				spriteRenderer!.sprite = potSprite;
				CurrentState = PotState.Placed;
				PlacedPot?.Invoke();
			}

		}

		public void FillPot() {

			if (CurrentState == PotState.Placed) {
				spriteRenderer!.sprite = filledPotSprite;
				CurrentState = PotState.Filled;
				FilledPot?.Invoke();
			}

		}

#endregion

	}

}