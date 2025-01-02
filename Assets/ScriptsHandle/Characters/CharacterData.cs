using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterData : MonoBehaviour
{
	private GeneralInformation GeneralInformation = GeneralInformation.Instance;
	private SaveSystem SaveSystem = SaveSystem.Instance;

    public Character character;
    // Start is called before the first frame update"
    void Start()
    {
        SaveSystem.Instance.saveLoad.currentScene = SceneManager.GetActiveScene().name;
        setAnimator();
        if (SaveSystem.saveLoad.currentPosition != null)
        {
			transform.position = SaveSystem.saveLoad.currentPosition;
		}
		if(GeneralInformation.ToSavePointNow)
		{
			GeneralInformation.ToSavePointNow = false;
            GameObject MapObj = GameObject.Find("Map");
			GameObject TargetMap = MapObj.transform.Find(SaveSystem.saveLoad.SavePointMap).gameObject;
			TargetMap.SetActive(true);
			GameObject SavePoint = TargetMap.transform.Find("SavePoint").gameObject;
			transform.position = SavePoint.transform.position;
		}
		if(GeneralInformation.IsWarp)
		{
            GeneralInformation.IsWarp = false;
            GameObject MapObj = GameObject.Find("Map");
            GameObject TargetMap = MapObj.transform.Find(GeneralInformation.TargetMap).gameObject;
            TargetMap.SetActive(true);
            GameObject WarpPointObj = TargetMap.transform.Find("WarpPoint").gameObject;
            GameObject WarpEndPoint = WarpPointObj.transform.Find(GeneralInformation.TargetPortal).gameObject;
            transform.position = WarpEndPoint.transform.position;
        }	
	}

	private void Update()
	{
		setAnimator();
        Vector3 position = transform.position;
        position.z = 0;
        transform.position = position;
        SaveSystem.saveLoad.currentPosition = transform.position;
    }

    private void OnDisable()
    {
        SaveSystem.saveLoad.currentPosition = transform.position;
    }


    public void setAnimator() 
    {
		character = SaveSystem.saveLoad.team.Teamate[0];
		Animator animator = this.GetComponent<Animator>();
        animator.runtimeAnimatorController = character.AnimatorController;
    }

}
