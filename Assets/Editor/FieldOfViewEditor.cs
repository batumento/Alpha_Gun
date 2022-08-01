using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FOV))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FOV _fov = (FOV)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(_fov.transform.position, Vector3.up, Vector3.forward, 360, _fov.viewRadius);
        Vector3 viewAngleA = _fov.DirFromAngle(-_fov.viewAngle / 2, false);
        Vector3 viewAngleB = _fov.DirFromAngle(_fov.viewAngle / 2, false);

        Handles.DrawLine(_fov.transform.position, _fov.transform.position + viewAngleA * _fov.viewRadius);
        Handles.DrawLine(_fov.transform.position, _fov.transform.position + viewAngleB * _fov.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in _fov.visibleTargets)
        {
            Handles.DrawLine(_fov.transform.position, visibleTarget.position);
        }
    }
}
