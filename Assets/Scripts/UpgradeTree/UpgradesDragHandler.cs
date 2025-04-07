using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradesDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IScrollHandler
{
   public float ScrollMultplier;
   private Vector2 dragStartOffset;
   public void OnBeginDrag(PointerEventData eventData)
   {
      dragStartOffset = this.transform.position.ToVector2() - eventData.position;
   }

   public void OnDrag(PointerEventData eventData)
   {
      this.transform.position = eventData.position + dragStartOffset;
   }
   private static readonly Vector3 minZoom = new Vector3(.1f, .1f, .1f);
   public void OnScroll(PointerEventData eventData)
   {
      Debug.Log(eventData.scrollDelta);
      this.transform.localScale = Vector3.Max(minZoom, this.transform.localScale + (eventData.scrollDelta.y * ScrollMultplier * Vector3.one));
   }


   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
