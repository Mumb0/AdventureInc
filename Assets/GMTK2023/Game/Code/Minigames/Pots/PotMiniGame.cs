using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Fields

		[SerializeField] private Transform[] potLocations = Array.Empty<Transform>();
		[SerializeField] private GameObject? potPrefab;

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
			MousePosition = ctx.ReadValue<Vector2>();
		}

		public void OnMouseClickInput(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {

				GameObject? clickedObject = GetClickedObject(MousePosition);

				string objectTag = string.Empty;

				if (clickedObject == null) {
					objectTag = string.Empty;
				}
				else {
					objectTag = clickedObject.tag;
				}

				switch (CurrentlySelectedTool) {

					case PotTool.Broom:

						if (objectTag == "PotPiece") {
							Debug.Log("PieceClicked");
							clickedObject?.GetComponent<PotPiece>().BroomPiece();
						}

						break;
					case PotTool.Pot:
						break;
					case PotTool.Coin:
						break;
					case PotTool.None:

						if (objectTag == string.Empty) {
							if (!wasVisited) {
								OnAdventurerLeft();
							}
						}

						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

			}

		}

		private GameObject? GetClickedObject(Vector2 clickPos) {
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(MainCamera!.ScreenPointToRay(clickPos), Mathf.Infinity);
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

			Debug.Log(CurrentlySelectedTool.ToString());

		}

		public override void OnAdventurerEntered() {

			Debug.Log("Hi I entered!");

		}

		public override void OnAdventurerLeft() {

			foreach (Pot p in ActivePots) {
				p.Smash();
			}

			wasVisited = true;

		}

#endregion

	}

}