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

    Ray ray;        // ����
    RaycastHit hit; // ���� �浹
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
        // ���� ī�޶� ���� ���콺 ��ǥ���� ������ ����
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        if(Input.GetMouseButtonDown(0))
        {
            // ��Ŭ���� ��Ʈ ������ �ִٸ�(������ �¾Ҵٸ�)
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                target = hit.point;
                GameObject Pointer = Instantiate(pointerPrefab, target, Quaternion.identity);
                Destroy(Pointer,1f);
                agent.SetDestination(target); // �������� Ŭ���� ��ǥ�� ����
                agent.isStopped = false;
            }
        }
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        // ������ǥ�� ���� ��ǥ�� ��ȯ
        Vector3 localVelocity = tr.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("ForwardSpeed", speed);
    }
}
