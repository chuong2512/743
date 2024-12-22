using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI text;
    private PlayerData _playerData;
    public Button button;
    [FormerlySerializedAs("musicController")] public CMusicController cMusicController;
    private int price;
    private int id;

    private void Start()
    {
        button.onClick.AddListener(() => cMusicController.Pay(price, id));
    }

    public void Show(int price, int id)
    {
        gameObject.SetActive(true);
        text.text = $"x{price}";
        this.price = price;
        this.id = id;
    }
}