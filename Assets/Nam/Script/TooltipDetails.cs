using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public string TitleText;
    public string DetailsText;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(TitleText, DetailsText);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
