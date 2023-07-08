using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GMTK2023.Game {

	public class PotPiece : MonoBehaviour {

#region Fields

		[SerializeField] private Image? imageComponent;
		[SerializeField] private Sprite[] potPieceSprites = Array.Empty<Sprite>();

#endregion

#region Methods

		public void Awake() {
			SetRandomPieceSprite();
		}

		private void SetRandomPieceSprite() {
			imageComponent!.sprite = potPieceSprites[Random.Range(0, potPieceSprites.Length - 1)];
		}

#endregion

	}

}