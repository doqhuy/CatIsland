using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public class ActionButton : MonoBehaviour
{
    private static ActionButton _instance;
    public static ActionButton Instance => _instance;
    private void Awake()
    {
        _instance = this; 
    }

    public bool IsPress = false;

    public void PressJKey()
    {
        IsPress = true;
        Invoke("ResetIsPress", Time.deltaTime);
    }

    public void ResetIsPress()
    {
        IsPress = false;
    }
}
