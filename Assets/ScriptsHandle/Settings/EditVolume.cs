using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditVolume : MonoBehaviour
{
    public Slider volumeSlider;

    private void Update()
    {
        GeneralInformation.Instance.volume = volumeSlider.value;
    }
}
