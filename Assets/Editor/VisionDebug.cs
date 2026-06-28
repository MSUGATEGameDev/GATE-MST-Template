//Based Off 3D Enemy FOV Tut https://www.youtube.com/watch?v=j1-OyLo77ss

using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

[CustomEditor(typeof(Vision))]
public class VisionDebug : Editor
{
    private void OnSceneGUI()
    {
        Vision fov = (Vision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        if (fov.canSeeTarget && fov.targetObject != null)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.targetObject.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}