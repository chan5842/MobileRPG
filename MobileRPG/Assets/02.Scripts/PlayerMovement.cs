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

    // 조이스틱 움직에 의한 함수
    public void OnStickChanged(Vector2 stick)
    {
        h = stick.x;
        v = stick.y;
    }

    private void FixedUpdate()
    {
        if (animator)
        {
            // 조이스틱에 의한 움직임 구현
            animator.SetFloat("Speed", h * h + v * v);
            if(rb)
            {
                Vector3 Speed = rb.velocity; // 리지드 바디의 속도를 가져옴
                Speed.x = 4 * h;
                Speed.z = 4 * v;
                rb.velocity = Speed;
                // 움직이고 있는 상태라면
                if(Speed != Vector3.zero)
                {
                    tr.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));

                    // 소스가 재생중이 아니라면 발걸음 실행
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
