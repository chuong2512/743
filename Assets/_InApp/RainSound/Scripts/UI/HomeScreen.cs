using System;
using TMPro;
using UnityEngine;

namespace BabySound
{
    public class HomeScreen : AppScreen
    {
        [SerializeField] private TextMeshProUGUI _coinTMP;

        public override ScreenType GetID()
        {
            return ScreenType.HomeScreen;
        }

        protected override void Start()
        {
            base.Start();
            //SetCoinText();
            //GameManager.OnChangeCoin += OnChangeCoin;
        }

        private void OnChangeCoin(int i)
        {
            SetCoinText();
        }

        private void SetCoinText()
        {
            _coinTMP.SetText(GameDataManager.Instance.playerData.intDiamond.ToString() + "<sprite=0>");
        }

        private void OnDestroy()
        {
            AGameManager.OnChangeCoin -= OnChangeCoin;
        }
    }
}