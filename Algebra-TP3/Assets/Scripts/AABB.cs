using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour
{
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 size;
    [SerializeField] private MeshFilter meshFilter;

    [SerializeField] private Vector3[] vertices;
    [SerializeField] Vector3 minV;
    [SerializeField] Vector3 maxV;

    private void Start()
    {
        minV = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        maxV = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        meshFilter = GetComponentInChildren<MeshFilter>();

        vertices = meshFilter.mesh.vertices;
        SearchVertex();
    }

    private void Update()
    {
        SearchVertex();
    }

    private void SearchVertex()
    {
        minV = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        maxV = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        for (int i = 0; i < vertices.Length; i++)
        {
            minV = Vector3.Min(minV, transform.TransformPoint(vertices[i]));
            maxV = Vector3.Max(maxV, transform.TransformPoint(vertices[i]));
        }
    }

    public bool IsColliding(AABB Aabb)
    {
        float halfWidthR1 = Aabb.GetSize().x / 2;
        float halfHeightR1 = Aabb.GetSize().y / 2;
        float halfProfR1 = Aabb.GetSize().z / 2;

        float halfWidthR2 = GetSize().x / 2;
        float halfHeightR2 = GetSize().y / 2;
        float halfProfR2 = GetSize().z / 2;

        float distanceX = Aabb.GetCenter().x - GetCenter().x;
        float distanceY = Aabb.GetCenter().y - GetCenter().y;
        float distanceZ = Aabb.GetCenter().z - GetCenter().z;

        distanceX = Mathf.Abs(distanceX);
        distanceY = Mathf.Abs(distanceY);
        distanceZ = Mathf.Abs(distanceZ);

        distanceX -= halfWidthR1 + halfWidthR2;
        distanceY -= halfHeightR1 + halfHeightR2;
        distanceZ -= halfProfR1 + halfProfR2;

        return (distanceX < 0 && distanceY < 0 && distanceZ < 0);
    }

    public Vector3 GetCenter()
    {
        return (minV + maxV) / 2;
    }

    public Vector3 GetSize()
    {
        return maxV - minV;
    }

    private void OnDrawGizmos()
    {
        //todo check where is origin
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(this.origin, this.size);
        Gizmos.DrawWireCube(GetCenter(), GetSize());
    }
}

