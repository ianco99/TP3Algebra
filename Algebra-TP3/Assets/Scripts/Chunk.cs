using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chunks
{
    [Serializable]
    public class Chunk
    {
        private Vector3 origin;
        private Vector3 size;

        public Chunk(Vector3 origin, Vector3 size)
        {
            this.origin = origin;
            this.size = size;
        }

        public Vector3 GetOrigin() => origin;
        public Vector3 GetSize() => size;
    }
}
