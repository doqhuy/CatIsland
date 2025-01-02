using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class GeneralInformation : MonoBehaviour
{
    //Singleton Declare
    private static GeneralInformation _instance;
	public static GeneralInformation Instance => _instance;
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	//Talking, Shopping, Playing, Eventing
	public string Actioning = "Playing";
	public string currentScene;
    public int charSelectionNumber = 0;
	public bool ToSavePointNow = false;
	public bool IsWarp = false;
    public float volume = 1f;

    public string TargetMap = "Island";
	public string TargetPortal = "Door";

    public List<Enemy> EnemyInBattle;
	public List<Equipment> EquipmentDrop;
	public List<Item> ItemDrop;
	public int Zone;
	public bool WinLose = false;


    private void Start()
    {
		StartCoroutine( TimeCount());
    }

	IEnumerator TimeCount()
	{
		while (true)
		{
			SaveSystem.Instance.saveLoad.Time++;
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
		VolumeHanle();
    }

    private void VolumeHanle()
    {
		GameObject MainCameraObj = GameObject.Find("Main Camera");
		if(MainCameraObj != null )
		{
            AudioSource audio = MainCameraObj.GetComponent<AudioSource>();
			if(audio != null )
			{
                audio.volume = volume;
            }
        }	
    }

	public Coroutine NotiCorou;

	public void CallPlayerNoti(string Text)
	{
		if(NotiCorou != null)
		{
			StopCoroutine(NotiCorou);
		}	
		Debug.Log(Text);
		NotiCorou = StartCoroutine(ShowPlayerNotiCoroutine(Text));
	}
    IEnumerator ShowPlayerNotiCoroutine(string Text)
    {
		GameObject PlayerObj = GameObject.Find("Player");
        GameObject PlayerNotiObj = PlayerObj.transform.Find("PlayerNoti").gameObject;
        GameObject TextObj = PlayerNotiObj.transform.Find("Text").gameObject;
        TMP_Text text = TextObj.GetComponent<TMP_Text>();
        text.text = Text;
        PlayerNotiObj.SetActive(true);
        yield return new WaitForSeconds(4);
		if(PlayerNotiObj != null )
		{
            PlayerNotiObj.SetActive(false);
        }
    }



}
