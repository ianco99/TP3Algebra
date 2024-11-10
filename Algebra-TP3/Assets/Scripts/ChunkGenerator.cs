using System.Collections.Generic;
using UnityEngine;

namespace Chunks
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3 gridDimensions;
        [SerializeField] private Vector3 unitSize;
        [SerializeField] List<Chunk> chunks = new List<Chunk>();
        [SerializeField] int highlightChunkNumber = 0;

        [SerializeField] int pointsPerChunk = 30;

        [SerializeField] List<GameObject> objetos = new List<GameObject>();

        private void OnEnable()
        {
            CreateChunks();
        }

        [ContextMenu("GenerateChunks")]
        public void CreateChunks()
        {
            chunks.Clear();

            for (int i = 0; i < gridDimensions.z; i++)
            {
                for (int j = 0; j < gridDimensions.y; j++)
                {
                    for (int k = 0; k < gridDimensions.x; k++)
                    {
                        Vector3 gridOffset = new Vector3(transform.position.x + unitSize.x * k,
                                                         transform.position.y + unitSize.y * j,
                                                         transform.position.z + unitSize.z * i);
                        Chunk newChunk = new Chunk(gridOffset, unitSize, pointsPerChunk);

                        chunks.Add(newChunk);
                    }
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                for (int j = i + 1; j < objetos.Count; j++)
                {
                    AABB c1 = objetos[i].GetComponent<AABB>();
                    AABB c2 = objetos[j].GetComponent<AABB>();

                    if (c1 != null && c2 != null)
                    {
                        if (c1.IsColliding(c2))
                        {
                            print(i + " y " + j + " colision");
                            Model m1 = objetos[i].GetComponent<Model>();
                            Model m2 = objetos[j].GetComponent<Model>();

                            if(m1 != null && m2 != null)
                            {
                                for(int k = 0; k < chunks.Count; k++)
                                {
                                    for (int l = 0; l < chunks[k].Points.Count; l++)
                                    {
                                        if (m1.ContainAPoint(chunks[k].Points[l]) && m2.ContainAPoint(chunks[k].Points[l]))
                                        {
                                            print(i + " y " + j + " colision verdadera");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (chunks.Count > 0)
            {
                for (int i = 0; i < chunks.Count; i++)
                {
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawWireCube(chunks[i].GetOrigin() + chunks[i].GetSize() / 2.0f, chunks[i].GetSize());

                        for (int j = 0; j < chunks[i].Points.Count; j++)
                        {
                            Gizmos.color = Color.blue;
                            Gizmos.DrawSphere(chunks[i].Points[j], 0.01f);
                        }
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (chunks.Count > highlightChunkNumber && highlightChunkNumber >= 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(chunks[highlightChunkNumber].GetOrigin() + chunks[highlightChunkNumber].GetSize() / 2.0f, chunks[highlightChunkNumber].GetSize());
            }
        }

        //[ContextMenu("Iscontained")]
        //public void Test()
        //{
        //    Debug.Log(chunks[highlightChunkNumber].ContainsPoint(transform.position));
        //}
    }
}