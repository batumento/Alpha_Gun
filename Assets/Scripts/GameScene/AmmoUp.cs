using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : MonoBehaviour
{
    public float rotateSpeed;
    public float rotangle; 
    void Start()
    {
        InvokeRepeating("rot", 0, rotateSpeed);
    }
    public void Rotate()
    {
        transform.Rotate(Vector3.up * rotangle);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
}
