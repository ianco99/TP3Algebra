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

    //public AABB(Mesh mesh)
    //{
    //    meshFilter.mesh = mesh;

    //    MakeBox();
    //}

    private void Start()
    {
        minV = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        maxV = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        meshFilter = GetComponentInChildren<MeshFilter>();

        vertices = meshFilter.mesh.vertices;
        SearchVertex();

        //MakeBox();
    }

    private void Update()
    {
        SearchVertex();
        //MakeBox();
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

    //public void MakeBox()
    //{
    //    var meshKeyPoints = new Dictionary<string, Vector3> { { "Lowest", meshFilter.mesh.vertices[0] } };
    //    meshKeyPoints["Lowest"] = meshFilter.mesh.vertices[0];
    //    for (int i = 0; i < meshFilter.mesh.vertices.Length; i++)
    //    {
    //        //this is horrible, but it needs to check for all, all the time
    //        if (meshKeyPoints["Lowest"].y > meshFilter.mesh.vertices[i].y)
    //        {
    //            meshKeyPoints["Lowest"] = meshFilter.mesh.vertices[i];
    //        }
    //        if (!meshKeyPoints.ContainsKey("Highest") || meshKeyPoints["Highest"].y < meshFilter.mesh.vertices[i].y)
    //        {
    //            meshKeyPoints["Highest"] = meshFilter.mesh.vertices[i];
    //        }
    //        if (!meshKeyPoints.ContainsKey("Leftest") || meshKeyPoints["Leftest"].x > meshFilter.mesh.vertices[i].x)
    //        {
    //            meshKeyPoints["Leftest"] = meshFilter.mesh.vertices[i];
    //        }
    //        if (!meshKeyPoints.ContainsKey("Rightest") || meshKeyPoints["Rightest"].x < meshFilter.mesh.vertices[i].x)
    //        {
    //            meshKeyPoints["Rightest"] = meshFilter.mesh.vertices[i];
    //        }
    //        if (!meshKeyPoints.ContainsKey("Closest") || meshKeyPoints["Closest"].z > meshFilter.mesh.vertices[i].z)
    //        {
    //            meshKeyPoints["Closest"] = meshFilter.mesh.vertices[i];
    //        }
    //        if (!meshKeyPoints.ContainsKey("Furthest") || meshKeyPoints["Furthest"].z < meshFilter.mesh.vertices[i].z)
    //        {
    //            meshKeyPoints["Furthest"] = meshFilter.mesh.vertices[i];
    //        }
    //    }

    //    Vector3 originR = new Vector3(meshKeyPoints["Leftest"].x, meshKeyPoints["Lowest"].y, meshKeyPoints["Closest"].z);

    //    // unity starts at center, but in here it starts at left-low corner
    //    this.size = new Vector3(meshKeyPoints["Rightest"].x, meshKeyPoints["Highest"].y, meshKeyPoints["Furthest"].z) * 2;
    //    this.origin = transform.TransformPoint(originR + (size / 2));
    //}

    //public bool DoesCollideAABB(AABB Other)
    //{
    //    bool collidesD1 = false;
    //    bool collidesD2 = false;
    //    bool collidesD3 = false;
    //    // Separated by axis
    //    // it then needs to collide in two dimensions minimum to ensure is colliding
    //    // also, it can be done with less checks, but this is readable

    //    //XY
    //    //other is right
    //    if (this.origin.x < Other.origin.x)
    //    {
    //        //is above
    //        if (this.origin.y < Other.origin.y)
    //        {
    //            collidesD1 = this.origin.x + this.size.x > Other.origin.x && this.origin.y + this.size.y > Other.origin.y;
    //        }
    //        else
    //        {
    //            collidesD1 = this.origin.x + this.size.x > Other.origin.x && this.origin.y < Other.origin.y + Other.size.y;
    //        }
    //    }
    //    else
    //    {
    //        //is above
    //        if (this.origin.y < Other.origin.y)
    //        {
    //            collidesD1 = this.origin.x + this.size.x > Other.origin.x + Other.size.x && this.origin.y + this.size.y > Other.origin.y;
    //        }
    //        else
    //        {
    //            collidesD1 = this.origin.x + this.size.x > Other.origin.x + Other.size.x && this.origin.y < Other.origin.y + Other.size.y;
    //        }
    //    }

    //    //XZ
    //    //other is right
    //    if (this.origin.x < Other.origin.x)
    //    {
    //        //is further
    //        if (this.origin.z < Other.origin.z)
    //        {
    //            //check
    //        }
    //        else
    //        {
    //            //check
    //        }
    //    }
    //    else
    //    {
    //        //is further
    //        if (this.origin.z < Other.origin.z)
    //        {
    //            //check
    //        }
    //        else
    //        {
    //            //check
    //        }
    //    }

    //    if (collidesD1 && collidesD2)
    //    {
    //        return true;
    //    }

    //    //YZ
    //    //other is above
    //    if (this.origin.y < Other.origin.y)
    //    {
    //        //is further
    //        if (this.origin.z < Other.origin.z)
    //        {
    //            //check
    //        }
    //        else
    //        {
    //            //check
    //        }
    //    }
    //    else
    //    {
    //        //is further
    //        if (this.origin.z < Other.origin.z)
    //        {
    //            //check
    //        }
    //        else
    //        {
    //            //check
    //        }
    //    }

    //    return collidesD1 && collidesD3 || collidesD2 && collidesD3;
    //}

    public bool IsColliding(AABB Aabb)
    {
        float halfWidthR1 = Aabb.GetSize().x;
        float halfHeightR1 = Aabb.GetSize().y;
        float halfProfR1 = Aabb.GetSize().z;

        float halfWidthR2 = GetSize().x;
        float halfHeightR2 = GetSize().y;
        float halfProfR2 = GetSize().z;

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

