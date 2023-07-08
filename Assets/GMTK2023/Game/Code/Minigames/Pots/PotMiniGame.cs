using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Fields

		[SerializeField] private Transform[] potLocations = Array.Empty<Transform>();
		[SerializeField] private GameObject? potPrefab;


#endregion

#region Properties

		private Vector2 MousePosition { get; set; } = new Vector2();
		public IList<Pot> ActivePots { get; set; } = new List<Pot>();
		public PotTool CurrentlySelectedTool { get; set; } = PotTool.None;

#endregion

#region Methods

		public void Start() {
			SetupRoom();
		}

		public void SetupRoom() {

			ActivePots = new Collection<Pot>();

			foreach (Transform t in potLocations) {
				// NOTE: We force because this should never be null
				Pot pot = Instantiate(potPrefab, t.position, Quaternion.identity, t)!.GetComponent<Pot>();
				ActivePots?.Add(pot);
			}

		}

		public void OnPointerMoveInput(InputAction.CallbackContext ctx) {
			MousePosition = ctx.ReadValue<Vector2>();
		}

		public void OnMouseClickInput(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {

				switch (CurrentlySelectedTool) {

					case PotTool.Broom:
						break;
					case PotTool.Pot:
						break;
					case PotTool.Coin:
						break;
					case PotTool.None:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

			}

		}

		public override void OnAdventurerEntered() {

			Debug.Log("Hi I entered!");

		}

		public override void OnAdventurerLeft() {

			foreach (Pot p in ActivePots) {
				p.Smash();
			}

		}

#endregion

	}

}