using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamateFight : MonoBehaviour
{
    public Slider HPBar;
    public Slider MPBar;

    public int TeamateIndex;
    public SaveSystem SaveSystem;
    public GeneralInformation GeneralInformation;

    public Image Avatar;
    public Image AvatarInBattle;
    public TMP_Text Name;
    public TMP_Text HP;
    public TMP_Text MP;

    public TMP_Text ATK;
    public TMP_Text Magic;
    public TMP_Text DEF;
    public TMP_Text MagicRES;
    public TMP_Text Luck;
    public TMP_Text Speed;
    public TMP_Text BuffATK;
    public TMP_Text BuffDEF;
    public TMP_Text BuffSpeed;
    //public TMP_Text Lv;

    public GameObject Protect;
    public GameObject Buff;

    public Character thisCharacter;
    private void Start()
    {
        SaveSystem = SaveSystem.Instance;
        GeneralInformation = GeneralInformation.Instance;

        thisCharacter = SaveSystem.saveLoad.team.Teamate[TeamateIndex];

        if (thisCharacter != null)
        {
            thisCharacter.InitialStat();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (thisCharacter != null)
        {
            GetCharacterInformation();
        }
        if (thisCharacter.HP <= 0)
        {
            this.gameObject.SetActive(false);
        }
        thisCharacter.ReStat();
    }

    void GetCharacterInformation()
    {
        Avatar.sprite = thisCharacter.Avatar;
        AvatarInBattle.sprite = thisCharacter.AvatarInBattle;
        Name.text = thisCharacter.Name;
        HP.text = thisCharacter.HP + "/" + thisCharacter.MaxHP;
        MP.text = thisCharacter.MP + "/" + thisCharacter.MaxMP;

        ATK.text = "" + thisCharacter.ATK;
        Magic.text = "" + thisCharacter.Magic;
        DEF.text = "" + thisCharacter.DEF;
        MagicRES.text = "" + thisCharacter.MagicRES;
        Luck.text = "" + thisCharacter.Luck;
        Speed.text = "" + thisCharacter.Speed;

        BuffATK.text = "" + thisCharacter.BuffATK;
        BuffDEF.text = "" + thisCharacter.BuffDEF;
        BuffSpeed.text = "" + thisCharacter.BuffSpeed;

        //Lv.text = " " + thisCharacter.Level;
        HPBar.value = (float)thisCharacter.HP / (float)thisCharacter.MaxHP;
        MPBar.value = (float)thisCharacter.MP / (float)thisCharacter.MaxMP;
        if (thisCharacter.CheckProtect() == true)
        {
            Protect.SetActive(true);
        }
        else
        { Protect.SetActive(false); }

        GameObject ATKBuffObj = Buff.transform.Find("ATKBuff").gameObject;
        GameObject ATKDebuffObj = Buff.transform.Find("ATKDebuff").gameObject;
        GameObject DEFBuffObj = Buff.transform.Find("DEFBuff").gameObject;
        GameObject DEFDebuffObj = Buff.transform.Find("DEFDebuff").gameObject;
        GameObject SpeedBuffObj = Buff.transform.Find("SpeedBuff").gameObject;
        GameObject SpeedDebuffObj = Buff.transform.Find("SpeedDebuff").gameObject;

        if (thisCharacter.BuffATK == 0) { ATKBuffObj.SetActive(false); ATKDebuffObj.SetActive(false); }
        else if (thisCharacter.BuffATK > 0) { ATKBuffObj.SetActive(true); ATKDebuffObj.SetActive(false); }
        else if (thisCharacter.BuffATK < 0) { ATKBuffObj.SetActive(false); ATKDebuffObj.SetActive(true); }


        if (thisCharacter.BuffDEF == 0) { DEFBuffObj.SetActive(false); DEFDebuffObj.SetActive(false); }
        else if (thisCharacter.BuffDEF > 0) { DEFBuffObj.SetActive(true); DEFDebuffObj.SetActive(false); }
        else if (thisCharacter.BuffDEF < 0) { DEFBuffObj.SetActive(false); DEFDebuffObj.SetActive(true); }

        if (thisCharacter.BuffSpeed == 0) { SpeedBuffObj.SetActive(false); SpeedDebuffObj.SetActive(false); }
        else if (thisCharacter.BuffSpeed > 0) { SpeedBuffObj.SetActive(true); SpeedDebuffObj.SetActive(false); }
        else if (thisCharacter.BuffSpeed < 0) { SpeedBuffObj.SetActive(false); SpeedDebuffObj.SetActive(true); }



    }




}
