using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRegisButton : MonoBehaviour
{
   private Button _button;
   public Button _targetButton;
   public GameObject _subPanel;

   private void OnValidate()
   {
      if (_targetButton == null)
      {
         _targetButton = transform.parent.GetComponentInParent<Button>();
      }
   }

   private void Start()
   {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(OnClickButton);
   }

   private void OnClickButton()
   {
      if (GameDataManager.Instance.playerData.IsRegister)
      {
         _targetButton.onClick.Invoke();
      }
      else
      {
         _subPanel.SetActive(true);
      }
   }
}
