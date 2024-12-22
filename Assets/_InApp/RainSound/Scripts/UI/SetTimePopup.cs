using System;
using UnityEngine;

namespace BabySound.Scripts.UI
{
    public class SetTimePopup : BasePopup
    {
        [SerializeField] private SetTimeButton[] _setTimeButtons;

        public override void OnOpen()
        {
            base.OnOpen();
        }

        public override ScreenType GetID()
        {
            return ScreenType.SetTimePopup;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            _setTimeButtons = GetComponentsInChildren<SetTimeButton>();
        }

        private void Start()
        {
            for (int i = 0; i < _setTimeButtons.Length; i++)
            {
                _setTimeButtons[i].SetTime(i);
            }
        }
    }
}