using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI nameF;
    public TextMeshProUGUI effectF;
    public TextMeshProUGUI mpF;
    public TextMeshProUGUI powerF;
    public TextMeshProUGUI targetF;
    public TextMeshProUGUI typeF;
    public LayoutElement layoutElement;
    public RectTransform backgroundRectTransform;
    public int characterWrapLimit;
    // Start is called before the first frame update
    public void SetText(string effect, string name ="",string mp = "---", string power = "---", string target = "---", string type = "---")
    {
        if (string.IsNullOrEmpty(name))
        {
            nameF.gameObject.SetActive(false);
        }
        else
        {
            nameF.gameObject.SetActive(true);
            nameF.text = name;
        }
        effectF.text = effect;
        mpF.text = mp;
        powerF.text = power;
        targetF.text = target;
        typeF.text = type;

        int nameLength = nameF.text.Length;
        int effectLength = effectF.text.Length;
        layoutElement.enabled = (nameLength > characterWrapLimit || effectLength > characterWrapLimit) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        // Convert screen coordinates to local coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            mousePosition,
            null,
            out Vector2 localPoint
        );

        // Set the tooltip position
        transform.localPosition = localPoint;

        // Shift the tooltip to the top right of the mouse
        Vector2 tooltipSize = backgroundRectTransform.sizeDelta;
        transform.localPosition += new Vector3(tooltipSize.x / 2f, tooltipSize.y / 2f, 0f);


    }
}
