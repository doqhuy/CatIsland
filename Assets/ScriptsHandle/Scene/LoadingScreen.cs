using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Nếu sử dụng TextMeshPro
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;  // GameObject màn hình loading
    public GameObject loadingText;    // GameObject chứa văn bản loading

    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        Text uiText = null;
        TMP_Text tmpText = null;

        if (loadingText != null)
        {
            uiText = loadingText.GetComponent<Text>();
            tmpText = loadingText.GetComponent<TMP_Text>();
        }

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (uiText != null)
                uiText.text = $"{Mathf.RoundToInt(progress * 100)}%";

            if (tmpText != null)
                tmpText.text = $"{Mathf.RoundToInt(progress * 100)}%";

            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}
