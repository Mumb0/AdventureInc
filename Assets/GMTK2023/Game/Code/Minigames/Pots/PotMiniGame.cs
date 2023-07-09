using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

#endregion

#region Properties

		public override bool IsCredible => IsPrepared;
		private Vector2 MousePosition { get; set; } = new Vector2();
		public IList<Pot> ActivePots { get; set; } = new List<Pot>();
		public PotTool CurrentlySelectedTool { get; set; } = PotTool.None;

#endregion

#region Methods

		public void Start() {
			miniGameCanvas!.worldCamera = MainCamera;
			SetupRoom();
		}

		public void SetupRoom() {

			ActivePots = new Collection<Pot>();

			foreach (Transform t in potLocations) {
				// NOTE: We force because this should never be null
				Pot pot = Instantiate(potPrefab, t.position, Quaternion.identity, t)!.GetComponent<Pot>();
				ActivePots?.Add(pot);
				pot.CleanedALlPieces += OnGameTaskCompleted;
				pot.PlacedPot += OnGameTaskCompleted;
				pot.FilledPot += OnGameTaskCompleted;
			}

		}

		public void OnPointerMoveInput(InputAction.CallbackContext ctx) {
			if (ctx.performed) {
				MousePosition = ctx.ReadValue<Vector2>();
			}
		}

		public void OnMouseClickInput(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {

				GameObject? clickedObject;

				switch (CurrentlySelectedTool) {

					case PotTool.Broom:

						if (CurrentTaskStep == 0) {
							clickedObject = GetClickedObject(MousePosition, LayerMask.GetMask("PotPiece"));

							if (clickedObject != null) {
								clickedObject.GetComponent<PotPiece>().CleanPiece();
							}
						}

						break;
					case PotTool.Pot:

						if (CurrentTaskStep == 1) {
							clickedObject = GetClickedObject(MousePosition, LayerMask.GetMask("Pot"));

							if (clickedObject != null) {
								clickedObject.GetComponent<Pot>().PlacePot();
							}
						}

						break;
					case PotTool.Coin:

						if (CurrentTaskStep == 2) {
							clickedObject = GetClickedObject(MousePosition, LayerMask.GetMask("Pot"));

							if (clickedObject != null) {
								clickedObject.GetComponent<Pot>().FillPot();
							}
						}

						break;
					case PotTool.None:
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

		public void OnGameTaskCompleted() {

			if (ActivePots.All(x => x.CurrentState == PotState.Cleaned)) {
				CompleteTask();
			}

			if (ActivePots.All(x => x.CurrentState == PotState.Placed)) {
				CompleteTask();
			}

			if (ActivePots.All(x => x.CurrentState == PotState.Filled)) {
				CompleteTask();
			}

			if (CurrentTaskStep == 3) {
				IsPrepared = true;
				OnTasksCompleted();
			}

		}

		private void CompleteTask() {
			MiniGameTasks[CurrentTaskStep].IsCompleted = true;
			OnMiniGameTaskCompleted(CurrentTaskStep);
			CurrentTaskStep++;
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

		public override void SetActive(bool state) {

			if (state) {
				playerActions!.SwitchCurrentActionMap("PotMiniGame");
				gameObject.transform.localPosition = new Vector2(0, 0);
				return;
			}

			gameObject.transform.localPosition = Origin;

		}

		public override void OnAdventurerEntered() {

			if (!IsPrepared) {
				OnAdventurerEnteredUnpreparedRoom();
			}
			else {
				Debug.Log("Adventurer is adventuring.");
			}

		}

		public override void OnAdventurerLeft() {

			foreach (Pot p in ActivePots) {
				p.Smash();
			}

			IsPrepared = false;
			CurrentTaskStep = 0;

		}

#endregion

	}

}