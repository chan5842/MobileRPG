using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float height = 5f;
    public float distance = 7f;
    public float targetOffset = 2f;
    public float moveDamping = 15f;
    public float rotDamping = 20f;

    Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }
    
    void LateUpdate()
    {
        Vector3 camPos = target.position - (target.forward * distance)
                                     + (target.up * height);

        tr.position = Vector3.Lerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotDamping);
        tr.LookAt(target.position + (target.up * targetOffset));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.3f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(target.position + (target.up * targetOffset), transform.position);
        Gizmos.DrawWireSphere(transform.position, 1f);
        
    }
}
