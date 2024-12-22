using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.WSANative.Store;
using CI.WSANative.Common;
public class IAP_UWP : MonoBehaviour
{
   
    void Awake()
    {
        WSANativeCore.Initialise();
    }
   
   //store id get from microsoft partner 
    public void BuyProduct(string storeid, int points)
    {
        WSANativeStore.RequestPurchase(storeid, result =>
        {
            UnityEngine.Debug.Log(result.Status);
            if(result.Status == WSAStorePurchaseStatus.Succeeded)
            {
                   // do something here to add point value

            }
        });
    }


}
