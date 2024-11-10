using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[Serializable]
public class Plane
{
    [SerializeField] private Vector3 normal;
    [SerializeField] private Vector3 point;
    [SerializeField] private float distance;

    [SerializeField] private Vector3 center;

    [SerializeField] private Vector3[] vertices;

    public Vector3 Normal { get { return normal; } }
    public Vector3 Point { get { return point; } }
    public Vector3 Center { get { return center; } }
    public float Distance { get { return distance; } }

    public Plane(Vector3 normal, Vector3 point)
    {
        this.normal = normal.normalized;
        this.point = point;

        distance = Vector3.Dot(this.normal, point);
    }

    public Plane(Vector3 vect1, Vector3 vect2, Vector3 vect3)
    {
        Vector3 vert1 = vect2 - vect1;
        Vector3 vert2 = vect3 - vect1;

        normal = GetCrossProduct(vert2, vert1).normalized;

        point = vect1;

        vertices = new Vector3[3];
        vertices[0] = vect1;
        vertices[1] = vect2;
        vertices[2] = vect3;

        distance = Vector3.Dot(-normal, point);
    }

    public void DrawGizmo(Transform transform)
    {
        Vector3 transformedPoint = point;
        Vector3 transformedNormal = normal;

        Vector3 v1 = vertices[0];
        Vector3 v2 = vertices[1];
        Vector3 v3 = vertices[2];

        Gizmos.color = Color.green;
        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v3, v1);

        Gizmos.color = Color.red;
        center = (v1 + v2 + v3) / 3;
        Gizmos.DrawLine(center, center + transformedNormal * 0.2f);
    }

    public void SetNewPoints(Vector3 vect1, Vector3 vect2, Vector3 vect3)
    {
        Vector3 vert1 = vect2 - vect1;
        Vector3 vert2 = vect3 - vect1;

        normal = GetCrossProduct(vert2, vert1).normalized;

        point = vect1;

        vertices = new Vector3[3];
        vertices[0] = vect1;
        vertices[1] = vect2;
        vertices[2] = vect3;

        distance = Vector3.Dot(normal, point);
    }

    Vector3 GetCrossProduct(Vector3 rotationA, Vector3 rotationB)
    {
        Vector3 rotation;

        rotation.x = rotationA.y * rotationB.z - rotationA.z * rotationB.y;
        rotation.y = rotationA.z * rotationB.x - rotationA.x * rotationB.z;
        rotation.z = rotationA.x * rotationB.y - rotationA.y * rotationB.x;

        return rotation;
    }

    public bool GetSide(Vector3 pointToCheck)
    {
        float dot = (pointToCheck.x - point.x) * normal.x + (pointToCheck.y - point.y) * normal.y + (pointToCheck.z - point.z) * normal.z;
        return dot > 0;
    }
}
