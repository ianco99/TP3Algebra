using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[Serializable]
public class Triangle
{
    [SerializeField] Vector3[] vertexs;

    public Vector3[] vertices => vertexs;

    public Triangle()
    {
        vertexs = new Vector3[3];
        vertexs[0] = Vector3.zero;
        vertexs[1] = Vector3.zero;
        vertexs[2] = Vector3.zero;
    }

    public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        vertexs[0] = vertex1;
        vertexs[1] = vertex2;
        vertexs[2] = vertex3;
    }

    public void SetVertexs(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        vertexs[0] = vertex1;
        vertexs[1] = vertex2;
        vertexs[2] = vertex3;
    }
}
