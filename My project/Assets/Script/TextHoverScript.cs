using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Numerics;

public class TextHoverScript : EventTrigger
{
    // The actual transform of the text that we're attached to
    RectTransform rectTransform;


    [SerializeField]
    private float hoverOffsetAmount = 200;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public override void OnPointerEnter(PointerEventData data)
    {
        //GetComponent<Text>().color = Color.yellow; // Change color on hover
        UnityEngine.Vector3 modifPosition = rectTransform.position;
        modifPosition.y += hoverOffsetAmount;

        rectTransform.SetPositionAndRotation(modifPosition, rectTransform.rotation);
        Debug.Log("OnPointerEnter called.");
    }

    public override void OnPointerExit(PointerEventData data)
    {
        //GetComponent<Text>().color = Color.white; // Change back to original color

        UnityEngine.Vector3 modifPosition = rectTransform.position;
        modifPosition.y -= hoverOffsetAmount;

        rectTransform.SetPositionAndRotation(modifPosition, rectTransform.rotation);
        Debug.Log("OnPointerExit called.");
    }
}
