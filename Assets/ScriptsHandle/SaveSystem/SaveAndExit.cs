using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveAndExit : MonoBehaviour
{
	private void Start()
	{
		GameObject GeneralInformation = GameObject.Find("GeneralInformation");
		GameObject SaveSystem = GameObject.Find("SaveSystem");
		SaveSystem saveSystem = SaveSystem.GetComponent<SaveSystem>();
		Button button = this.GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() =>
		{
			SceneManager.LoadScene("StartScene");
		}
		);
	}
}
