using System;
using System.Collections.Generic;
using UnityEngine;

namespace AABB
{
    public class BoundingBox : MonoBehaviour
    {
        private Vector3 origin;
        private Vector3 size;
        private Mesh mesh;

        public BoundingBox(Mesh mesh)
        {
            this.mesh = mesh;
        }

        public void MakeBox()
        {
            var meshKeyPoints = new Dictionary<string, Vector3> { { "Lowest", this.mesh.vertices[0] } } ;
            meshKeyPoints["Lowest"] = this.mesh.vertices[0];
            for (int i = 0; i < this.mesh.vertices.Length; i++)
            {
                //this is horrible, but it needs to check for all, all the time
                if (meshKeyPoints["Lowest"].y > this.mesh.vertices[i].y)
                {
                    meshKeyPoints["Lowest"] = this.mesh.vertices[i];
                }
                if(!meshKeyPoints.ContainsKey("Highest")|| meshKeyPoints["Highest"].y < this.mesh.vertices[i].y)
                {
                    meshKeyPoints["Highest"] = this.mesh.vertices[i];
                }
                if(!meshKeyPoints.ContainsKey("Leftest")|| meshKeyPoints["Leftest"].x > this.mesh.vertices[i].x)
                {
                    meshKeyPoints["Leftest"] = this.mesh.vertices[i];
                }
                if(!meshKeyPoints.ContainsKey("Rightest")|| meshKeyPoints["Rightest"].x < this.mesh.vertices[i].x)
                {
                    meshKeyPoints["Rightest"] = this.mesh.vertices[i];
                }
                if(!meshKeyPoints.ContainsKey("Closest")|| meshKeyPoints["Closest"].z > this.mesh.vertices[i].z)
                {
                    meshKeyPoints["Closest"] = this.mesh.vertices[i];
                }
                if(!meshKeyPoints.ContainsKey("Furthest")|| meshKeyPoints["Furthest"].z <this.mesh.vertices[i].z)
                {
                    meshKeyPoints["Furthest"] = this.mesh.vertices[i];
                }

            }

            this.origin = new Vector3(meshKeyPoints["Leftest"].x, meshKeyPoints["Lowest"].y, meshKeyPoints["Closest"].z);
            this.size = new Vector3(meshKeyPoints["Rightest"].x, meshKeyPoints["Highest"].y, meshKeyPoints["Furthest"].z);
        }

        public bool DoesCollideAABB(BoundingBox Other)
        {
            bool collidesD1 = false;
            bool collidesD2 = false;
            bool collidesD3 = false;
            //Separated by 2 dimensions
            //it then needs to collide in two dimension minimum to ensure is colliding
            // also, it can be done with less checks, but this is readable

            //XY
            //other is right
            if (this.origin.x < Other.origin.x)
            {
                //is above
                if (this.origin.y < Other.origin.y)
                {
                    collidesD1 = this.origin.x + this.size.x > Other.origin.x && this.origin.y + this.size.y > Other.origin.y ;
                }
                else
                {
                    collidesD1 = this.origin.x + this.size.x > Other.origin.x && this.origin.y < Other.origin.y + Other.size.y;
                }
            }
            else
            {
                //is above
                if (this.origin.y < Other.origin.y)
                {
                    //check
                }
                else
                {
                    //check
                }
            }

            //XZ
            //other is right
            if (this.origin.x < Other.origin.x)
            {
                //is further
                if (this.origin.z < Other.origin.z)
                {
                    //check
                }
                else
                {
                    //check
                }
            }
            else
            {
                //is further
                if (this.origin.z < Other.origin.z)
                {
                    //check
                }
                else
                {
                    //check
                }
            }

            //YZ
            //other is above
            if (this.origin.y < Other.origin.y)
            {
                //is further
                if (this.origin.z < Other.origin.z)
                {
                    //check
                }
                else
                {
                    //check
                }
            }
            else
            {
                //is further
                if (this.origin.z < Other.origin.z)
                {
                    //check
                }
                else
                {
                    //check
                }
            }

            return collidesD1 && collidesD3 || collidesD1 && collidesD2 || collidesD2 && collidesD3;
        }


        private void OnDrawGizmos()
        {
            //todo check where is origin
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(this.origin, this.size);
        }
    }
}
