using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpTime = 1;

    private Animator anim;

    
    void Start()
    {
        offset = transform.position - target.position;
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,offset + target.position, Time.deltaTime * lerpTime);
    }

    public void ShakeIt()
    {
        anim.SetTrigger("Shaking");
    }
}
