using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK2023.Game {

	public class PotPiece : MonoBehaviour {

#region Events

		public Action? PieceBroomed;

#endregion

#region Fields

		[SerializeField] private SpriteRenderer? spriteRenderer;
		[SerializeField] private Sprite[] potPieceSprites = Array.Empty<Sprite>();

#endregion

#region Methods

		public void Awake() {
			SetRandomPieceSprite();
		}

		private void SetRandomPieceSprite() {
			spriteRenderer!.sprite = potPieceSprites[Random.Range(0, potPieceSprites.Length - 1)];
		}

		public void BroomPiece() {
			PieceBroomed?.Invoke();
			Destroy(gameObject);
		}

#endregion

	}

}