using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    AudioSource source;
    Animator animator;
    
    [SerializeField]
    AudioClip[] FootSteps;

    float h = 0;
    float v = 0;
    void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        FootSteps = Resources.LoadAll<AudioClip>("Sounds/FootStep");
    }

    // ���̽�ƽ ������ ���� �Լ�
    public void OnStickChanged(Vector2 stick)
    {
        h = stick.x;
        v = stick.y;
    }

    private void FixedUpdate()
    {
        if (animator)
        {
            // ���̽�ƽ�� ���� ������ ����
            animator.SetFloat("Speed", h * h + v * v);
            if(rb)
            {
                Vector3 Speed = rb.velocity; // ������ �ٵ��� �ӵ��� ������
                Speed.x = 4 * h;
                Speed.z = 4 * v;
                rb.velocity = Speed;
                // �����̰� �ִ� ���¶��
                if(Speed != Vector3.zero)
                {
                    tr.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));

                    // �ҽ��� ������� �ƴ϶�� �߰��� ����
                    if (!source.isPlaying)
                        source.PlayOneShot(FootSteps[Random.Range(0, 1)], 1f);
                }
            }
        }
            
    }

    void Update()
    {
        
    }
}
