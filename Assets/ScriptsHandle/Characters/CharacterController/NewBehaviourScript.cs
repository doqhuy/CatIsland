using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class MobileGamePad : MonoBehaviour
{
    public Button UP;
    public Button DOWN;
    public Button LEFT;
    public Button RIGHT;

    private static MobileGamePad _instance;
    public static MobileGamePad Instance => _instance;
    private void Awake()
    {
        _instance = this;
    }


}
