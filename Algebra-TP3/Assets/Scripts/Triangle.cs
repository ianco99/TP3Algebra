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
}
