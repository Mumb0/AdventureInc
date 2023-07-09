using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game.MiniGames
{
    public class MealDisplay : MonoBehaviour
    {
        [SerializeField] private Sprite? breakfastSprite;
        [SerializeField] private Sprite? lunchSprite;
        [SerializeField] private Sprite? dinnerSprite;

        private Image image = null!;


        public void Display(MealType mealType)
        {
            image.sprite = mealType switch
            {
                MealType.Breakfast => breakfastSprite!,
                MealType.Lunch => lunchSprite!,
                MealType.Dinner => dinnerSprite!,
                _ => throw new ArgumentOutOfRangeException(nameof(mealType), mealType, null)
            };
        }

        private void Awake()
        {
            image = GetComponent<Image>();
            Display(MealType.Breakfast);
        }
    }
}