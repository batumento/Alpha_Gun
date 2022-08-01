using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyController : MonoBehaviour
{
    PlayerPrefsManager playerPrefsManager;
    Rigidbody rb;
    public float speed;

    private void Awake()
    {
        playerPrefsManager = GameObject.Find("CanvasUI").GetComponent<PlayerPrefsManager>();
        rb = GetComponent<Rigidbody>();        
    }
    private void Start()
    {
        rb.AddForce(Vector3.up * speed * 1000 * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform pos = other.transform.Find("MoneyPosition");
            transform.DOMove(pos.position, .5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerPrefsManager.MoneyValueIncrease();
            Destroy(gameObject);
        }
    }
}
