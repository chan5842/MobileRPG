using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    RectTransform rectTransform;
    // 터치 ID: 터치가 되면 0 ~ 양수값, 터치를 하지 않으면 -1
    int touchID = -1;
    Vector3 StartPos = Vector3.zero;
    public float dragRadius = 80f;
    bool btnPressed;
    PlayerMovement playerMovement;
    readonly string playerTag = "Player";

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        playerMovement = GameObject.FindGameObjectWithTag(playerTag)
            .GetComponent<PlayerMovement>();
        StartPos = rectTransform.position;
    }
    public void ButtonDown()
    {
        btnPressed = true;
    }
    public void ButtonUp()
    {
        btnPressed = false;
        HandleInput(StartPos);
    }

    void HandleTouchInput()
    {
        int i = 0;
        // 몇번 터치 했는지  자동으로 카운트 된다
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches) // 터치 횟수가 저장된 배열
            {
                i++;
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);
                // 터치가 막 시작되었다면
                if(touch.phase == TouchPhase.Began)
                {
                    // 조이스틱 범위 안에 든다면
                    if(touch.position.x <= (StartPos.x + dragRadius))
                    {
                        touchID = i;
                    }
                    if (touch.position.y <= (StartPos.y + dragRadius))
                    {
                        touchID = i;
                    }
                }
                // 터치된 상태에서 움직이거나 멈춘다면
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // 원 반지름 안에 들어있다면
                    if(touchID == i)
                    {
                        HandleInput(touchPos);
                    }
                }
                // 터치가 끝났따면
                if(touch.phase == TouchPhase.Ended)
                {
                    if(touchID == i)
                    {
                        touchID = -1;
                    }
                }
            }
        }
    }
    void HandleInput(Vector3 input)
    {
        if (btnPressed)
        {
            Vector3 diffVector = (input - StartPos);    // 거리
            // 터치 지점이 원의 넓이를 넘어갔다면
            if (diffVector.sqrMagnitude > dragRadius * dragRadius)
            {
                diffVector.Normalize(); // 정규화
                // 방향만 유지하고 원밖으로 나가지 않는다.
                rectTransform.position = StartPos + diffVector * dragRadius;
                Debug.Log(StartPos + diffVector);
            }
            else
            {
                rectTransform.position = input;
            }
        }
        else
        {
            rectTransform.position = StartPos;  // 터치를 떼면 초기 위치로 돌아감
        }
        Vector3 diff = rectTransform.position - StartPos;
        // 거리를 구한 값에서 원의 반지름으로 나누면 방향이 구해진다
        Vector3 normalDiff = new Vector3(diff.x / dragRadius,
                                         diff.y / dragRadius 
                                         );
        if(playerMovement != null)
        {
            playerMovement.OnStickChanged(normalDiff);
        }
    }

    private void Update()
    {
        // 안드로이드
        if(Application.platform == RuntimePlatform.Android)
        {
            HandleTouchInput();
        }
        // 컴퓨터
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            HandleInput(Input.mousePosition);
        }
    }

}
