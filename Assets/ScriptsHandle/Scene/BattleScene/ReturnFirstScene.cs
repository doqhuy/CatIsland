using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnFirstScene : MonoBehaviour
{

    void Start()
    {
		
	}

    public void ReturnToFirstScene()
    {
        SceneManager.LoadScene(SaveSystem.Instance.saveLoad.currentScene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
