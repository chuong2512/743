using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyTimeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private int _index;

    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            SetDataWithIndex();
        }
    }

    private int _timer;

    private void OnValidate()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetDataWithIndex();

        _button?.onClick.AddListener(OnClickButton);
    }

    private void SetDataWithIndex()
    {
        switch (_index)
        {
            case 0:
                _text.SetText("7 day");
                _priceText.SetText("0.99$");
                _timer = 7;
                break;
            case 1:
                _text.SetText("30 days");
                _priceText.SetText("1.99$");
                _timer = 30;
                break;
            case 2:
                _text.SetText("6 months");
                _priceText.SetText("4.99$");
                _timer = 180;
                break;
            case 3:
                _text.SetText("1 year");
                _priceText.SetText("9.99$");
                _timer = 365;
                break;
        }
    }

    private void OnClickButton()
    {
        IAPManager.OnPurchaseSuccess = () => AddTime(_timer);

        switch (_index)
        {
            case 0:
                IAPManager.Instance.BuyProductID(IAPKey.PACK1_RE);
                break;
            case 1:
                IAPManager.Instance.BuyProductID(IAPKey.PACK2_RE);
                break;
            case 2:
                IAPManager.Instance.BuyProductID(IAPKey.PACK3_RE);
                break;
            case 3:
                IAPManager.Instance.BuyProductID(IAPKey.PACK4_RE);
                break;
        }
    }

    private void AddTime(int time)
    {
        AGameManager.SetRegisterTime.Invoke(time);
    }
}