using System;
using UnityEngine;

namespace GMTK2023.Game
{
    [Serializable]
    public struct MiniGameTask
    {
        [SerializeField] private bool isCompleted;
        [SerializeField] private string taskText;

        public bool IsCompleted
        {
            get => isCompleted;
            set => isCompleted = value;
        }

        public string TaskText => taskText;
    }
}