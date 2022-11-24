using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    RectTransform rectTransform;
    // ��ġ ID: ��ġ�� �Ǹ� 0 ~ �����, ��ġ�� ���� ������ -1
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
        // ��� ��ġ �ߴ���  �ڵ����� ī��Ʈ �ȴ�
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches) // ��ġ Ƚ���� ����� �迭
            {
                i++;
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);
                // ��ġ�� �� ���۵Ǿ��ٸ�
                if(touch.phase == TouchPhase.Began)
                {
                    // ���̽�ƽ ���� �ȿ� ��ٸ�
                    if(touch.position.x <= (StartPos.x + dragRadius))
                    {
                        touchID = i;
                    }
                    if (touch.position.y <= (StartPos.y + dragRadius))
                    {
                        touchID = i;
                    }
                }
                // ��ġ�� ���¿��� �����̰ų� ����ٸ�
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // �� ������ �ȿ� ����ִٸ�
                    if(touchID == i)
                    {
                        HandleInput(touchPos);
                    }
                }
                // ��ġ�� ��������
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
            Vector3 diffVector = (input - StartPos);    // �Ÿ�
            // ��ġ ������ ���� ���̸� �Ѿ�ٸ�
            if (diffVector.sqrMagnitude > dragRadius * dragRadius)
            {
                diffVector.Normalize(); // ����ȭ
                // ���⸸ �����ϰ� �������� ������ �ʴ´�.
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
            rectTransform.position = StartPos;  // ��ġ�� ���� �ʱ� ��ġ�� ���ư�
        }
        Vector3 diff = rectTransform.position - StartPos;
        // �Ÿ��� ���� ������ ���� ���������� ������ ������ ��������
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
        // �ȵ���̵�
        if(Application.platform == RuntimePlatform.Android)
        {
            HandleTouchInput();
        }
        // ��ǻ��
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            HandleInput(Input.mousePosition);
        }
    }

}
