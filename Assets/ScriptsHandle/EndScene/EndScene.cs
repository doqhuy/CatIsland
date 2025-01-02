using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public GameObject EndCredit;
    public float slideSpeed = 1f; // Tốc độ di chuyển của hình ảnh
    public float slideDistance = 100f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = EndCredit.GetComponent<RectTransform>();
        StartCoroutine(CreditRolling());
    }

    IEnumerator CreditRolling()
    {
        float targetY = rectTransform.localPosition.y + slideDistance;
        while (rectTransform.localPosition.y < targetY)
        {
            rectTransform.localPosition += Vector3.up * slideSpeed * Time.deltaTime;
            yield return null; // Chờ một frame
        }
    }    
}
