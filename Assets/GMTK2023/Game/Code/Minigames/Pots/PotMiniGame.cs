using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Fields

		[SerializeField] private Transform[] potLocations = Array.Empty<Transform>();
		[SerializeField] private GameObject? potPrefab;

#endregion

#region Properties

		public IList<Pot> ActivePots { get; set; } = new List<Pot>();

		public PotTool CurrentlySelectedTool { get; set; } = PotTool.None;

#endregion

#region Methods

		public void Awake() {
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

		/*
		public void OnMouseClickInput(InputAction.CallbackContext ctx) {
			
		}
		
		*/

		public override void OnAdventurerEntered() {
			throw new NotImplementedException();
		}

		public override void OnAdventurerLeft() {
			throw new NotImplementedException();
		}

#endregion

	}

}