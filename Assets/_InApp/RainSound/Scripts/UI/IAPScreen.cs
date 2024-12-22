using BabySound.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BabySound
{
    public class IAPScreen : MonoBehaviour
    {
        [SerializeField] private BuyCoinButton[] _buttons;

        void Start()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Index = i;
            }
        }
    }
}