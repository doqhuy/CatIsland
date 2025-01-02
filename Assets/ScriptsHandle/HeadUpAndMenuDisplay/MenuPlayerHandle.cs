using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerHandle : MonoBehaviour
{
    private void OnEnable()
    {
        GeneralInformation.Instance.Actioning = "Menuing";
    }

    private void OnDisable()
    {
        GeneralInformation.Instance.Actioning = "Playing";
    }
}
