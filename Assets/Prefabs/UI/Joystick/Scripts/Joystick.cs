using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //transform of the thumbStick
    [SerializeField] RectTransform ThumbStickTrans;
    [SerializeField] RectTransform BackgroundTrans;
    [SerializeField] RectTransform CenterTrans;
    public void OnDrag(PointerEventData eventData)
    {
        

        //calculate the position the touch is right now
        Vector2 TouchPos = eventData.position;
        //the center position of the background
        Vector2 centerPos = BackgroundTrans.position;

        //The difference between the TouchPos and centerPos is clamped using Vector2.ClampMagnitude,
        //restricting the offset to the radius of the background (BackgroundTrans.sizeDelta.x / 2).
        //This ensures that the thumbstick stays within the circular boundary.
        Vector2 localOffset = Vector2.ClampMagnitude(TouchPos - centerPos, BackgroundTrans.sizeDelta.x/2);

        //moving thumbStickTrans with maximum the radius position of the background, it limits the Vector to the radius.
        ThumbStickTrans.position = centerPos + localOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //it centers all the joystick where the thumb touch the screen
        BackgroundTrans.position = eventData.position;
        ThumbStickTrans.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //when you leave the touch it restores the original position
        BackgroundTrans.position = CenterTrans.position;
        ThumbStickTrans.position = BackgroundTrans.position;
    }
}
