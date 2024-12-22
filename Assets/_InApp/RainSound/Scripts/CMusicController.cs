using MyNamespace;
using TMPro;
using UnityEngine;

public class CMusicController : MonoBehaviour
{
    public int currentMusic;
    private PlayerData playerData; //todo delete
    private GameDataManager gameData; //todo delete
    public TextMeshProUGUI diamonds;
    public TextMeshProUGUI musicCoins;
    public GameObject inApp;
    public PopUp popup;

    void OnEnable()
    {
        gameData = GameDataManager.Instance;
        playerData = gameData.playerData;

        diamonds.text = "x" + playerData.intDiamond;
        musicCoins.text = diamonds.text;
        if (currentMusic > -1)
        {
            //AudioManager.Instance.PlaySong(currentMusic);
            //AudioManager.Instance.Play();
        }
    }

    void Start()
    {
        gameData = GameDataManager.Instance;
        playerData = gameData.playerData;
    }

    public void ShowPopup(int price, int id)
    {
        popup.Show(price, id);
    }

    public void Pay(int price, int id)
    {
        if (!playerData.CheckCanUnlock(price, id)) return;
        playerData.Unlock(id);
        diamonds.text = "x" + playerData.intDiamond;
        musicCoins.text = diamonds.text;
        popup.gameObject.SetActive(false);
    }

    public void AddDiamonds(int value)
    {

    }
}