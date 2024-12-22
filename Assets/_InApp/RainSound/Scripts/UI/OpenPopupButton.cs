using UnityEngine;
using UnityEngine.UI;

public class OpenPopupButton : MonoBehaviour
{
    private Button _button;

    [SerializeField] private ScreenType _screenType;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        if (_screenType == ScreenType.Back)
        {
            CUIManager.Instance.Back();
        }
        else
        {
            CUIManager.Instance.OpenScreen(_screenType);
        }
    }
}