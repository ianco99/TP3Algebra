using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chunks
{
    [Serializable]
    public class Chunk
    {
        private List<Plane> planes;
        private List<Vector3> points;

        private int maxPoints;
        private float pSpacing;
        private float margin = 1f;

        enum Axis { x, y, z };

        public Vector3 origin;
        public Vector3 size;

        public List<Vector3> Points { get { return points; } }

        public Chunk(Vector3 origin, Vector3 size, int maxPoints)
        {
            this.origin = origin;
            this.size = size;

            this.maxPoints = maxPoints;

            points = new List<Vector3>();

            GenerateContainerPlanes();
            GeneratePoints();
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

        private void GeneratePoints()
        {
            points.Clear();

            float spacing = Mathf.Min(size.x, size.y, size.z) / (maxPoints / 3);

            int pointsPerAxisX = Mathf.FloorToInt((size.x - margin) / spacing) + 1;
            int pointsPerAxisY = Mathf.FloorToInt((size.y - margin) / spacing) + 1;
            int pointsPerAxisZ = Mathf.FloorToInt((size.z - margin) / spacing) + 1;

            for (int i = 0; i < pointsPerAxisX; i++)
            {
                for (int j = 0; j < pointsPerAxisY; j++)
                {
                    for (int k = 0; k < pointsPerAxisZ; k++)
                    {
                        float x = origin.x + (margin / 2) + i * spacing;
                        float y = origin.y + (margin / 2) + j * spacing;
                        float z = origin.z + (margin / 2) + k * spacing;

                        points.Add(new Vector3(x, y, z));
                    }
                }
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
