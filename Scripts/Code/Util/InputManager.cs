using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputManager : MonoSingleton<InputManager>
{
    public const float DistanceLimitMin = 0.75f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 holdPosition;
    public Vector2 StartPosition { get { return startPosition; } }
    public Vector2 EndPosition { get { return endPosition; } }
    public Vector2 HoldPosition { get { return holdPosition; } }
    private bool isPress = false;
    public bool IsPress => isPress;
    public System.Action OnInputEnd;
    
    public override void Initialize()
    {
        isPress = false;
    }
    void Update()
    {
        
#if UNITY_EDITOR
            ClickScreenMouse();
#else
            ClickScreenTouch();
#endif    
    }
    public void ClickScreenMouse()
    {
        if (isPress == false)
        {
            if (Input.GetMouseButtonDown(0))
                OnStart(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (isPress == true)
        {
            if (Input.GetMouseButton(0))
                OnHold(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetMouseButtonUp(0))
                OnEnd(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }



    public void ClickScreenTouch()
    {
        if (Input.touchCount > 0)
        {
            if (isPress == false)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    OnStart(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
            }
            if (isPress == true)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    OnHold(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    OnEnd(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
            }
        }
    }


    private void OnStart(Vector2 _position)
    {
        startPosition = _position;
        endPosition = _position;
        holdPosition = _position;

        isPress = true;
    }
    private void OnHold(Vector2 _position)
    {
        endPosition = _position;
        //여기

        holdPosition = _position;
    }
    private void OnEnd(Vector2 _position)
    {
        endPosition = _position;
        holdPosition = _position;
        isPress = false;
        if (OnInputEnd != null)
        {
            OnInputEnd();
            OnInputEnd = null;
        }

    }
    public Vector2 GetDirection()
    {
        return endPosition - startPosition;
    }
    public Direction GetSwipeDirection(bool isHold)
    {
        Vector2 direction; 
        float distance;
        if(isHold)
        {
            direction = endPosition - startPosition;
            distance = direction.sqrMagnitude;          
            if(distance < DistanceLimitMin)
                return Direction.Center;
        }
        else
        {
            direction = endPosition - endPosition;
            distance = direction.sqrMagnitude;
            if(distance < DistanceLimitMin * Time.deltaTime)
                return Direction.Center;
        }
        
        return direction.ToDirection();
    }

    private bool IsPointerOverGameObject(int _fingerId)
    {
        EventSystem eventSystem = EventSystem.current;

        return eventSystem.IsPointerOverGameObject(_fingerId);
    }
}
