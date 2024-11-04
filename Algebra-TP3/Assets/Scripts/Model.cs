using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chunks;

public class Model : MonoBehaviour
{
    public Chunk chunk;

    [SerializeField] List<Triangle> triangles = new List<Triangle>();

    [SerializeField] MeshFilter meshFilter;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
