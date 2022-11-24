using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMover : MonoBehaviour
{
    Transform tr;
    NavMeshAgent agent;
    Animator animator;
    GameObject pointerPrefab;

    Ray ray;        // 광선
    RaycastHit hit; // 광선 충돌
    Vector3 target = Vector3.zero;

    void Start()
    {
        tr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        pointerPrefab = Resources.Load("FeedbackPointer") as GameObject;
    }

    void Update()
    {
        // 메인 카메라 부터 마우스 좌표까지 광선을 쏴줌
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        if(Input.GetMouseButtonDown(0))
        {
            // 좌클릭시 히트 정보가 있다면(광선에 맞았다면)
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                target = hit.point;
                GameObject Pointer = Instantiate(pointerPrefab, target, Quaternion.identity);
                Destroy(Pointer,1f);
                agent.SetDestination(target); // 목적지를 클릭한 좌표로 지정
                agent.isStopped = false;
            }
        }
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        // 월드좌표를 로컬 좌표로 변환
        Vector3 localVelocity = tr.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("ForwardSpeed", speed);
    }
}
