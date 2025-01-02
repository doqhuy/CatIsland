using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public SaveSystem SaveSystem;
    void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Quit());

        GameObject SaveSysObj = GameObject.Find("SaveSystem");
        SaveSystem = SaveSysObj.GetComponent<SaveSystem>();
    }

	public void Quit()
	{
        #if UNITY_EDITOR
		EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
	}
}
