using System.Collections.Generic;
using UnityEngine;

namespace Chunks
{
    public class ChunkGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3 gridSize;
        List<Chunk> chunks = new List<Chunk>();

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

        private void OnDrawGizmosSelected()
        {
            if (chunks.Count > 0)
            {
                for (int i = 0; i < chunks.Count; i++)
                {
                    Gizmos.DrawWireCube(chunks[i].GetOrigin() + chunks[i].GetSize() / 2.0f, chunks[i].GetSize());
                }
            }
        }
    }
}