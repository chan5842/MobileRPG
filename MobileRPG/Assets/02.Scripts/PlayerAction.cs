using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Animator animator;
    AudioSource source;

    // ���� ��ư UI
    float lastAttackTime = 0f;  // ���������� ������ Ÿ��
    bool isAttaking;            // ������ ���� Ȯ��
    readonly int hashCombo = Animator.StringToHash("IsCombo");

    // �뽬 ��ư UI
    float lastDashTime = 0f;
    bool isDashing; 
    readonly int hashDash = Animator.StringToHash("Dash");


    // ��ų ��ư UI
    [SerializeField]
    AudioClip swordClip;
    float lastSkillTime = 0f;
    readonly int hashSkill = Animator.StringToHash("Skill");

    void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        swordClip = Resources.Load("Sounds/coconut_throw") as AudioClip;
    }

    public void OnAttackDown()
    {
        isAttaking = true;
        animator.SetBool(hashCombo, true);
        StartCoroutine(StartAttack());
    }
    public void OnAttackUp()
    {
        isAttaking = false;
        animator.SetBool(hashCombo, false);
    }

    public void OnSkillDown()
    {
        if(Time.time - lastSkillTime > 1f)
        {
            animator.SetTrigger(hashSkill);
            lastSkillTime = Time.time;
            source.clip = swordClip;
            source.PlayDelayed(0.7f);
        }
    }


    public void OnDashDown()
    {
        if(Time.time - lastDashTime > 1f)
        {
            lastDashTime = Time.time;
            isDashing = true;
            animator.SetTrigger(hashDash);
        }
        
    }
    public void OnDashUp()
    {
        isDashing = false;
    }

    IEnumerator StartAttack()
    {
        if(Time.time - lastAttackTime > 1f) // 1�� �������� ����Ű�� ���ȴ��� Ȯ��
        {
            lastAttackTime = Time.time;
            while(isAttaking)
            {
                animator.SetBool(hashCombo, true);
                yield return new WaitForSeconds(1f);
            }
        }
        //yield return null;
    }
}
