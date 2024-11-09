using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chunks;

public class Model : MonoBehaviour
{
    public Chunk chunk;

    [SerializeField] List<Triangle> triangles = new List<Triangle>();

    [SerializeField] MeshFilter meshFilter;

    [SerializeField] List<Plane> planes = new List<Plane>();

    public List<Triangle> Triangles { get { return triangles; } }
    public MeshFilter MeshFilter { get { return meshFilter; } }
    public Mesh Mesh { get { return meshFilter.mesh; } }

    private void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();

        if (meshFilter == null)
            return;

        Vector3[] vertices = meshFilter.mesh.vertices;

        for (int i = 0; i < meshFilter.mesh.triangles.Length; i += 3) 
        { 
            Triangle triangle = new Triangle();

            int vertIndex1 = meshFilter.mesh.triangles[i];
            int vertIndex2 = meshFilter.mesh.triangles[i + 1];
            int vertIndex3 = meshFilter.mesh.triangles[i + 2];

            triangle.vertices[0] = vertices[vertIndex1];
            triangle.vertices[1] = vertices[vertIndex2];
            triangle.vertices[2] = vertices[vertIndex3];

            triangles.Add(triangle);
        }

        for (int i = 0; i < triangles.Count; i++) 
        {
            Plane plane = new Plane(triangles[i].vertices[0], triangles[i].vertices[1], triangles[i].vertices[2]);
            planes.Add(plane);
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0;i < planes.Count;i++)
        {
            planes[i].DrawGizmo(transform);
        }
    }

    public bool ContainAPoint(Vector3 point)
    {
        bool isContain = true;

        for(int i = 0; i < planes.Count; i++)
        {
            Vector3 posP = planes[i].Center;
            Vector3 normal = planes[i].Normal;

            float dot = (point.x - posP.x) * normal.x + (point.y - posP.y) * normal.y + (point.z - posP.z) * normal.z;

            if(dot < 0)
            {
                isContain = false;
                break;
            }
        }

        return isContain;
    }
}
