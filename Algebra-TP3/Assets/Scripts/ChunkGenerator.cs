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
        [SerializeField] List<Triangle> faces = new List<Triangle>();
        [SerializeField] Mesh mesh;

        [ContextMenu("GenerateChuncks")]
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
                        Chunk newChunk = new Chunk(gridOffset, unitSize);

                        chunks.Add(newChunk);

                        
                    }
                }
            }

            Triangle cacho = new Triangle(Vector3.zero, Vector3.zero, Vector3.zero);
            faces.Add(cacho);
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

        [ContextMenu("Iscontained")]
        public void Test()
        {
             Debug.Log(chunks[highlightChunkNumber].ContainsPoint(transform.position));
        }
    }
}