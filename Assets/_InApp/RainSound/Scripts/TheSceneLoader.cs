using System.Collections;
using UnityEngine;
using SingleApp;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheSceneLoader : PersistentSingleton<TheSceneLoader>
{
    public GameObject loadingScreen;

    //public CanvasGroup canvasGroup;
    private float _loading = 0;
    public TextMeshProUGUI loadingTMP;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame(string sceneName)
    {
        loadingTMP.SetText("Loading...");
        StartCoroutine(StartLoad(sceneName));
    }

    IEnumerator StartLoad(string sceneName)
    {
        _loading = 0;
        loadingScreen.SetActive(true);
        yield return StartCoroutine(SlideLoadingScreen(100, 2));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(SlideLoadingScreen(1, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator SlideLoadingScreen(float targetValue, float duration)
    {
        float startValue = _loading;
        float time = 0;
        while (time < duration)
        {
            _loading = Mathf.Lerp(startValue, targetValue, time / duration);
            loadingTMP.SetText($"{(int) _loading}%");

            time += Time.deltaTime;
            yield return null;
        }

        _loading = targetValue;
    }
}