using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [ContextMenu("Waoskers")]
    void Start()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        //Debug.Log("Does the point lie on the plane?: " + plane.LiesOnPlane(new Vector3(1,0,1)));
        Debug.Log("Does the point lie positive to the plane: " + plane.IsPositiveToThePlane(new Vector3(5,0.2f,10)));
    }
}
