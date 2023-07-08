using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class PotMiniGame : MiniGame {

#region Fields

		[SerializeField] private RectTransform[] potLocations =
				Array.Empty<RectTransform>();
		[SerializeField] private GameObject? potPrefab;

#endregion

#region Properties

public IList<Pot> ActivePots { get; set; } =
	new List<Pot>();

#endregion

#region Methods

		public void Awake() {
			SetupRoom();
		}

		public void SetupRoom() {

			ActivePots = new Collection<Pot>();

			foreach (RectTransform rt in potLocations)
			{
				// NOTE: We force because this should never be null
				var pot = Instantiate(potPrefab, rt.anchoredPosition, Quaternion.identity)!.GetComponent<Pot>();
				ActivePots?.Add(pot);
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