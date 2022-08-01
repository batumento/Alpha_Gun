using System.Collections;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Camera camera;
    private void Awake()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back,
            camera.transform.rotation * Vector3.up);
    }
}
