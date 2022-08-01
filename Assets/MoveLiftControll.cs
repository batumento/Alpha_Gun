using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MoveLiftControll : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (this.tag == "levelLift")
            {
                byte sceneIndex = (byte)SceneManager.GetActiveScene().buildIndex;
                transform.parent.transform.DOMoveY(10, 5).OnComplete(() => SceneManager.LoadScene(sceneIndex + 1));
            }
            transform.parent.transform.DOMoveY(10, 5).OnComplete(()=> door.transform.DOMoveY(transform.position.y + 5.19f,2));
        }
    }
}
