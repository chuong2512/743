using System.Collections;
using BabySound;
using UnityEngine;

public class StartApp : MonoBehaviour
{
   private IEnumerator Start()
   {
      yield return new WaitForSeconds(2);
      TheSceneLoader.Instance.StartGame(Constants.MENU_SCENE);
   }
}
