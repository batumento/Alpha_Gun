using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyManager : MonoBehaviour
{
    public TextMeshProUGUI key_Txt;
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
        key_Txt.text = (int.Parse(key_Txt.text) + 1).ToString();
        Destroy(gameObject);
    }

}
