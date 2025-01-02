using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    public bool IsChangeScene = false;
    public string TargetScene = "FirstScene";
    public string TargetMap = "StartVillage";
    public string TargetPortal = "HouseA_Door";
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;
    private SaveSystem SaveSystem = SaveSystem.Instance;
    void WarpToPlace()
    {
        GeneralInformation.TargetMap = TargetMap;
        GeneralInformation.TargetPortal = TargetPortal;
        if(IsChangeScene)
        {
            SceneManager.LoadScene(TargetScene);
            GeneralInformation.IsWarp = true;
        }    
        else
        {
            GameObject PlayerObj = GameObject.Find("Player");
            GameObject MapObj = GameObject.Find("Map");
            GameObject TargetMap = MapObj.transform.Find(GeneralInformation.TargetMap).gameObject;
            TargetMap.SetActive(true);
            GameObject WarpPointObj = TargetMap.transform.Find("WarpPoint").gameObject;
            GameObject WarpEndPoint = WarpPointObj.transform.Find(GeneralInformation.TargetPortal).gameObject;
            PlayerObj.transform.position = WarpEndPoint.transform.position;
            SaveSystem.Instance.saveLoad.currentScene = SceneManager.GetActiveScene().name;
        }    
    }

    private void Update()
    {
        GameObject PlayerObj = GameObject.Find("Player");
        if(Vector2.Distance(PlayerObj.transform.position, transform.position) < 0.7f)
        {
            WarpToPlace();
        }    
    }
}
