using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK2023.Game.MiniGames
{
    public abstract class MiniGame : MonoBehaviour, IMiniGame
    {
        #region Events

        public event Action? AllMiniGameTasksCompleted;
        public event Action<int>? MiniGameTaskCompleted;
        public event Action? AdventurerEnteredUnpreparedRoom;

        #endregion

        #region Fields

        [SerializeField] internal Canvas? miniGameCanvas;
        [SerializeField] internal PlayerInput? playerActions;
        [SerializeField] private ActivityAsset? activity;
        [SerializeField] private MiniGameTask[] miniGameTasks = Array.Empty<MiniGameTask>();

        #endregion

        #region Properties

        public bool IsPrepared { get; set; } = true;
        public abstract bool IsCredible { get; }

        public IActivity Activity => activity!;

        public int CurrentTaskStep { get; set; } = 0;
        public MiniGameTask[] MiniGameTasks => miniGameTasks;
        public Camera? MainCamera { get; private set; }

        protected Vector2 Origin { get; set; }

        #endregion

        #region Methods

        protected virtual void Awake()
        {
            MainCamera = Camera.main;
            Origin = transform.localPosition;
        }

        public abstract void SetActive(bool state);

        public abstract void OnAdventurerEntered();

        public abstract void OnAdventurerLeft();

        protected virtual void OnTasksCompleted()
        {
            AllMiniGameTasksCompleted?.Invoke();
        }

        protected virtual void OnAdventurerEnteredUnpreparedRoom()
        {
            AdventurerEnteredUnpreparedRoom?.Invoke();
        }

        protected virtual void OnMiniGameTaskCompleted(int currentTaskStep)
        {
            MiniGameTaskCompleted?.Invoke(CurrentTaskStep);
        }

        #endregion
    }
}