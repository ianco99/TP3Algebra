using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [ContextMenu("Waoskers")]
    void Start()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Debug.Log(plane.LiesOnPlane(new Vector3(1,0,1)));
    }
}
