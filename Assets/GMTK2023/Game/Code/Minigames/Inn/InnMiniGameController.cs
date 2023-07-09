using System;
using UnityEngine;

namespace GMTK2023.Game.MiniGames
{
    public class InnMiniGameController : MiniGame
    {
        [SerializeField] private int breakfastStartTime;
        [SerializeField] private int lunchStartTime;
        [SerializeField] private int dinnerStartTime;

        private IIngameTimeKeeper ingameTimeKeeper = null!;
        private MealDisplay mealDisplay = null!;
        private MealType servedMealType;


        private MealType CorrectMealType
        {
            get
            {
                var time = ingameTimeKeeper.Hour;
                if (time < breakfastStartTime) return MealType.Dinner;
                if (time < lunchStartTime) return MealType.Breakfast;
                if (time < dinnerStartTime) return MealType.Lunch;
                return MealType.Dinner;
            }
        }

        public override bool IsCredible => ServedMealType == CorrectMealType;

        public MealType ServedMealType
        {
            get => servedMealType;
            set
            {
                servedMealType = value;
                mealDisplay.Display(servedMealType);
            }
        }


        public override void SetActive(bool state)
        {
            if (state)
            {
                playerActions!.SwitchCurrentActionMap("Inn");
                gameObject.transform.localPosition = Vector3.zero;
                return;
            }

            gameObject.transform.localPosition = Origin;
        }

        public override void OnAdventurerEntered()
        {
        }

        public override void OnAdventurerLeft()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            ingameTimeKeeper = Singleton.TryFind<IIngameTimeKeeper>()!;
            mealDisplay = GetComponentInChildren<MealDisplay>();
        }

        private void Start()
        {
            ServedMealType = MealType.Dinner;
        }

        public void CycleMeal()
        {
            ServedMealType = ServedMealType switch
            {
                MealType.Breakfast => MealType.Lunch,
                MealType.Lunch => MealType.Dinner,
                MealType.Dinner => MealType.Breakfast,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}