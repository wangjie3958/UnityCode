using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour,IBeginDragHandler,IDragHandler
{
    private RectTransform rectTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 pos;
        //offset
     RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        offset = rectTransform.position - pos;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos+ offset;
    }




}
