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

    //método that has the same firm(parameter in Vector2 and no out params) can be associated to this delegate
    public delegate void OnStickInputValueUpdated(Vector2 inputVal);
    //event, a way that lets other components or scripts suscribe  to be notified when something happen
    //this type of event is OnStickInputValueUpdated, meaning any method matching the signature of that delegate
    //(accepting a Vector2 and returning void) can subscribe to this event
    //When the event is "fired" (or invoked), it will notify all subscribed methods and execute them with the provided value as an argument.
    public event OnStickInputValueUpdated onStickValueUpdated;

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

        // normalizes the joystick input to a value between -1 and 1 for both the x and y axes
        //joystick on center, inputVal = 0,0
        //When the joystick is fully pushed in any direction, the value will be (1, 0), (0, 1), (-1, 0), or (0, -1), depending on the direction.
        Vector2 inputVal = localOffset / BackgroundTrans.sizeDelta.x / 2;
        //This line checks if there are any subscribers to the onStickValueUpdated event using the ?. operator.
        //If there are, it invokes the event and passes the normalized inputVal as a parameter.
        onStickValueUpdated?.Invoke(inputVal);
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

        //This line checks if there are any subscribers to the onStickValueUpdated event using the ?. operator.
        //If there are, it invokes the event and passes the normalized inputVal as a parameter.
        onStickValueUpdated?.Invoke(Vector2.zero);
    }
}
