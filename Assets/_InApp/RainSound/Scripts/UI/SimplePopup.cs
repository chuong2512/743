using BabySound.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace ABCBoard.Scripts.UI
{
    public class SimplePopup : AppPopup
    {
        [SerializeField] private ScreenType _type;

        public override ScreenType GetID() => _type;
    }
}