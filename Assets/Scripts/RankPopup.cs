using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using LitJson;
using moon;
public class RankPopup : MonoBehaviour
{
    public static RankPopup instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        ShowMyInfo();

        SendUpdateMyRank();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        OpenPopup();
    }

    public NotificationPopup notificationPopup;
         
    public GameObject popup;
    public void OpenPopup()
    {
        popup.transform.localScale = Vector3.zero;
        popup.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void ClosePopup()
    {
        popup.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutExpo).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }

    public void TouchClose() {
        ClosePopup();
    }


    [Header("My Rank")]
    public Text txtMyIndex;
    public Text txtMyName;
    public Text txtmyLevel;

    public void ShowMyInfo() {
        txtMyIndex.text = "---";
        txtMyName.text = Config.GetUserName();
        txtmyLevel.text = "" + PlayerPrefs.GetInt("Level", 0);
    }


    public void ShowMyInfo_Rank(int _rank) {
        txtMyIndex.text = "" + _rank;
    }


    [Header("Loading")]
    public GameObject loading;
    public void ShowLoading() {
        loading.SetActive(true);
    }

    public void HideLoading() {
        loading.SetActive(false);
    }


    public void SendUpdateMyRank() {
        if (Config.lastUpdateRannk_Level != PlayerPrefs.GetInt("Level", 0))
        {
            Config.lastUpdateRannk_Level = PlayerPrefs.GetInt("Level", 0);

            StartCoroutine(RequestAddNewMyRank());
        }
        else {
            SendGetListRank();
        }
    }

    Task taskCheckMyUser;
    public IEnumerator CheckUserMyRank() {
        yield return new WaitForEndOfFrame();
        Debug.Log("CheckUserMyRankCheckUserMyRankCheckUserMyRank");
        coroutineCheckLoading = StartCoroutine(CheckLoading());
       
    }

    Task taskUpdateMyRank;
    public IEnumerator RequestUpdateMyRank()
    {
        yield return new WaitForEndOfFrame();
        var newUser = new Dictionary<string, object>();
        newUser[Config.NAME] = Config.GetUserName();
        newUser[Config.LEVEL] = PlayerPrefs.GetInt("Level", 0);
        Debug.Log(Config.userIdentify);
     
    }


    Task taskAddNewRank;
    public IEnumerator RequestAddNewMyRank() {
        Debug.Log("RequestAddNewMyRankRequestAddNewMyRank");
        yield return new WaitForEndOfFrame();
        coroutineCheckLoading = StartCoroutine(CheckLoading());
        var newUser = new Dictionary<string, object>();
        newUser[Config.NAME] = Config.GetUserName();
        newUser[Config.LEVEL] = PlayerPrefs.GetInt("Level", 0);
        Debug.Log(Config.userIdentify);
        while (!taskAddNewRank.IsCompleted) { yield return null; }

        SendGetListRank();

    }


    public void SendGetListRank()
    {
        Debug.Log("SendGetListRankSendGetListRankSendGetListRank");
       // StartCoroutine(RequestGetListRank_Check());
    }
    Task taskListRank;
   
    private Coroutine coroutineCheckLoading;
    public IEnumerator CheckLoading()
    {
        yield return new WaitForSeconds(Config.MAX_TIME_LOADING);
        HideLoading();
        notificationPopup.ShowInfo("Check your internet connection and try again!");

    }

    public RankBasicListAdapter rankBasicListAdapter;
}
