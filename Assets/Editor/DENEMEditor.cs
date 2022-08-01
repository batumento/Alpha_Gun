using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DENEME))]
public class DENEMEditor : Editor
{
    private void OnSceneGUI()
    {
        DENEME deneme = (DENEME)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(deneme.transform.position, Vector3.up, Vector3.forward, 360, deneme.radius);
    }
}
