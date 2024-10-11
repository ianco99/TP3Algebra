using System.Collections.Generic;
using UnityEngine;

namespace Chunks
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3 gridSize;
        [SerializeField] List<Chunk> chunks = new List<Chunk>();
        [SerializeField] int highlightChunkNumber = 0;

        [ContextMenu("GenerateChuncks")]
        public void CreateChunks()
        {
            chunks.Clear();

            for (int i = 0; i < gridSize.z; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    for (int k = 0; k < gridSize.x; k++)
                    {
                        Vector3 gridOffset = new Vector3(transform.position.x + gridSize.x * k,
                                                         transform.position.y + gridSize.y * j,
                                                         transform.position.z + gridSize.z * i);
                        Chunk newChunk = new Chunk(gridOffset, Vector3.one * 5);

                        chunks.Add(newChunk);
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
    }
}