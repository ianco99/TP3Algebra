using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chunks
{
    [Serializable]
    public class Chunk
    {
        private List<Plane> planes;

        enum Axis { x, y, z };

        public Vector3 origin;
        public Vector3 size;


        public Chunk(Vector3 origin, Vector3 size)
        {
            this.origin = origin;
            this.size = size;

            GenerateContainerPlanes();
        }

        private void GenerateContainerPlanes()
        {
            planes = new List<Plane>();

            for (int i = 0; i < 6; i++)
            {
                Plane newPlane;

                Vector3 newNormal = Vector3.zero;
                Vector3 newPoint = origin;

                float multiplier = 1.0f;

                if (i < 3)
                {
                    multiplier = -1.0f;
                    newPoint = origin + size;
                }

                Axis axis = (Axis)(i % 3);
                switch (axis)
                {
                    case Axis.x:
                        newNormal = Vector3.right * multiplier;
                        break;
                    case Axis.y:
                        newNormal = Vector3.up * multiplier;
                        break;
                    case Axis.z:
                        newNormal = Vector3.forward * multiplier;
                        break;
                    default:
                        Debug.LogError("CRITIC FAILURE");
                        break;
                }

                newPlane = new Plane(newNormal, newPoint);
                planes.Add(newPlane);
            }
        }

        public Vector3 GetOrigin() => origin;
        public Vector3 GetSize() => size;

        public bool ContainsPoint(Vector3 givenPoint)
        {
            bool pointIsContained = true;

            for (int i = 0; i < planes.Count; i++)
            {
                if (!planes[i].IsPositiveToThePlane(givenPoint))
                {
                    pointIsContained = false;
                }
            }

            return pointIsContained;
        }
    }
}
