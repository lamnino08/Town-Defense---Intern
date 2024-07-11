using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipManager : MonoBehaviour
{
    public Canvas parentCanvas;
    public Transform ToolTipTransform;
    public static TooltipManager Instance;
    public TMP_Text Title, Details;
    void Start()
    {
        Instance = this;
    }

    // Update được gọi một lần mỗi frame
    void Update()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition,
            parentCanvas.worldCamera,
            out movePos
        );
        ToolTipTransform.position = parentCanvas.transform.TransformPoint(movePos);
    }
    public void Show(string TitleText, string DetailsText) 
    {
        Title.text = TitleText;
        Details.text = DetailsText;
        ToolTipTransform.SetAsLastSibling();
        ToolTipTransform.gameObject.SetActive(true);
    }
    public void Hide()
    {
        ToolTipTransform.gameObject.SetActive(false);
    }
}
