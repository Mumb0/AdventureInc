using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Fields

		[SerializeField] private Transform[] potLocations = Array.Empty<Transform>();
		[SerializeField] private GameObject? potPrefab;

		[SerializeField] private Button[] toolButtons = Array.Empty<Button>();
		[SerializeField] private Sprite? selectedToolSprite;
		[SerializeField] private Sprite? deselectedToolSprite;

		private bool wasVisited = false;

#endregion

#region Properties

		private Vector2 MousePosition { get; set; } = new Vector2();
		public IList<Pot> ActivePots { get; set; } = new List<Pot>();
		public PotTool CurrentlySelectedTool { get; set; } = PotTool.None;

#endregion

#region Methods

		public void Start() {
			miniGameCanvas!.worldCamera = MainCamera;
			SetupRoom();
			playerActions!.SwitchCurrentActionMap("PotMiniGame");
		}

		public void SetupRoom() {

			ActivePots = new Collection<Pot>();

			foreach (Transform t in potLocations) {
				// NOTE: We force because this should never be null
				Pot pot = Instantiate(potPrefab, t.position, Quaternion.identity, t)!.GetComponent<Pot>();
				ActivePots?.Add(pot);
				pot.SetupPot();
			}

		}

		public void OnPointerMoveInput(InputAction.CallbackContext ctx) {
			if (ctx.performed) {
				MousePosition = ctx.ReadValue<Vector2>();
			}
		}

		public void OnMouseClickInput(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {

				switch (CurrentlySelectedTool) {

					case PotTool.Broom:

						GameObject? clickedObject = GetClickedObject(MousePosition, LayerMask.GetMask("PotPiece"));

						if (clickedObject) {
							clickedObject.GetComponent<PotPiece>().BroomPiece();
						}

						break;
					case PotTool.Pot:
						break;
					case PotTool.Coin:
						break;
					case PotTool.None:
						if (!wasVisited) {
							OnAdventurerLeft();
						}

						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

			}

		}

		private GameObject? GetClickedObject(Vector2 clickPos, LayerMask targetLayer) {
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(MainCamera!.ScreenPointToRay(clickPos), Mathf.Infinity, targetLayer);
			Collider2D? hitCollider = rayHit.collider;
			return hitCollider ? hitCollider.gameObject : null;
		}

		public void OnToolButtonClicked(int toolIndex) {

			PotTool selectingTool = (PotTool) toolIndex;

			if (CurrentlySelectedTool == selectingTool) {
				CurrentlySelectedTool = PotTool.None;
			}
			else {
				CurrentlySelectedTool = selectingTool;
			}

			for (int i = 0; i < toolButtons.Length; i++) {
				if (CurrentlySelectedTool == PotTool.None) {
					toolButtons[i].gameObject.GetComponent<Image>().sprite = deselectedToolSprite!;
				}
				else {
					toolButtons[i].gameObject.GetComponent<Image>().sprite = i == toolIndex ? selectedToolSprite! : deselectedToolSprite!;
				}
			}

		}

		public override void OnAdventurerEntered() { }

		public override void OnAdventurerLeft() {

			foreach (Pot p in ActivePots) {
				p.Smash();
			}

			wasVisited = true;

		}

#endregion

	}

}