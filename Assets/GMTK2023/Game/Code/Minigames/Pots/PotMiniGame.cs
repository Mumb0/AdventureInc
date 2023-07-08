using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Fields

		[SerializeField] private RectTransform[]? potLocations;
		[SerializeField] private GameObject? potPrefab;

#endregion

#region Properties

		public IList<Pot>? ActivePots { get; set; }

#endregion

#region Methods

		public void Awake() {
			SetupRoom();
		}

		public void SetupRoom() {

			ActivePots = new Collection<Pot>();

			foreach (RectTransform rt in potLocations) {
				ActivePots?.Add(Instantiate(potPrefab, rt.anchoredPosition, Quaternion.identity).GetComponent<Pot>());
			}

		}

		public override void OnAdventurerEntered() {
			throw new NotImplementedException();
		}

		public override void OnAdventurerLeft() {
			throw new NotImplementedException();
		}

#endregion

	}

}