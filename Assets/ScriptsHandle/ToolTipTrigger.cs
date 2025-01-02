using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject skillbutton;

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject Name = skillbutton.transform.Find("Name").gameObject;
        GameObject Effect = skillbutton.transform.Find("Effect").gameObject;
        GameObject Type = skillbutton.transform.Find("Type").gameObject;
        GameObject Mp = skillbutton.transform.Find("Mp").gameObject;
        GameObject Target = skillbutton.transform.Find("Target").gameObject;
        GameObject Power = skillbutton.transform.Find("Power").gameObject;
        TMP_Text NameText = Name.GetComponentInChildren<TMP_Text>();
        TMP_Text EffectText = Effect.GetComponentInChildren<TMP_Text>();
        TMP_Text MpText = Mp.GetComponentInChildren<TMP_Text>();
        TMP_Text PowerText = Power.GetComponentInChildren<TMP_Text>();
        TMP_Text TypeText = Type.GetComponentInChildren<TMP_Text>();
        TMP_Text TargetText = Target.GetComponentInChildren<TMP_Text>();



        ToolTipSystem.Show(EffectText.text.ToString(), NameText.text.ToString(), MpText.text.ToString(), PowerText.text.ToString(), TypeText.text.ToString(), TargetText.text.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Hide();
    }

    // Update is called once per frame

}
