using System;
using UnityEngine;

namespace GMTK2023.Game
{
    public class ShiftManager : MonoBehaviour, IShiftLoader
    {
        public event Action<IShiftLoader.ShiftLoadedEvent>? OnShiftLoaded;


        private void OnGameLoaded(IGameLoader.GameLoadEvent e)
        {
            // NOTE: We force the nullable here because a shift should always be found
            var shift = ShiftDb.TryLoadShiftByIndex(e.Game.ShiftIndex)!;

            OnShiftLoaded?.Invoke(new IShiftLoader.ShiftLoadedEvent(shift));
        }

        private void Awake()
        {
            Singleton.TryFind<IGameLoader>()!.OnGameLoaded += OnGameLoaded;
        }
    }
}