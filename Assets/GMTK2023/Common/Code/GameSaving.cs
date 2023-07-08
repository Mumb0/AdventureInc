using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace GMTK2023
{
    /// <summary>
    /// Module with types and logic to interact with saved-games
    /// </summary>
    public static class GameSaving
    {
        private const string SaveFileName = "save.json";

        private static readonly string saveFilePath =
            Path.Join(Application.persistentDataPath, SaveFileName);


        /// <summary>
        /// Represents a saved game
        /// </summary>
        /// <param name="ShiftIndex">The index of the current shift. 0-based</param>
        public record SavedGame(int ShiftIndex);


        /// <summary>
        /// Attempts to load the current saved game
        /// </summary>
        /// <returns>The saved game. Null if there is none</returns>
        public static async Task<SavedGame?> TryLoadSavedGameAsync()
        {
            if (!File.Exists(saveFilePath)) return null;

            var fileContent = await File.ReadAllTextAsync(saveFilePath);
            return JsonConvert.DeserializeObject<SavedGame>(fileContent!);
        }
    }
}