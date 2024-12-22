using BabySound.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BabySound
{
    public class RegisterScreen : MonoBehaviour
    {
        [SerializeField] private BuyTimeButton[] _buttons;
        [SerializeField] private TextMeshProUGUI _text;

        protected  void Start()
        {

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Index = i;
            }
        }
    }
}