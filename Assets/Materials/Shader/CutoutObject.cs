using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;
    private Vector3 targetRightObject;
    [SerializeField]
    private LayerMask wallMask;
    public Texture2D buildMat;
    private Camera maincamera;
    void Awake()
    {
        maincamera = GetComponent<Camera>();
        targetRightObject = new Vector3(targetObject.position.x, targetObject.position.y + 1, targetObject.position.z);
    }

    void Update()
    {
        
        Vector2 cutoutPos = maincamera.WorldToViewportPoint(targetRightObject);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetRightObject - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; i++)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                Debug.Log("calisti");
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", .1f);
                materials[m].SetFloat("_FalloffSize", .05f);
            }
        }
    }
}
