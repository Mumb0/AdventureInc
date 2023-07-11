using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game.MiniGames
{
    [RequireComponent(typeof(Image))]
    public class SpriteBlender : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = Array.Empty<Sprite>();

        private Image image = null!;
        private float t;


        private int SpriteCount => sprites.Length;

        public float T
        {
            get => t;
            set
            {
                t = Mathf.Clamp01(value);
                if (SpriteCount == 0) return;

                var index = Mathf.CeilToInt(T * (SpriteCount - 1));
                image.sprite = sprites[index];
            }
        }


        private void Awake()
        {
            image = GetComponent<Image>();
            T = 0;
        }
    }
}