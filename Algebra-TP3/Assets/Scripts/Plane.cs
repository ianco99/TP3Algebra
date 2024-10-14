using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane
{
    private Vector3 normal;
    private Vector3 point;

    public Plane(Vector3 normal, Vector3 point)
    {
        this.normal = normal;
        this.point = point;

        //PlaneDefinition();
    }

    /*private void PlaneDefinition()
    {
    }*/

    public bool LiesOnPlane(Vector3 pointToCheck)
    {
        return normal.x * (pointToCheck.x - point.x) +
        normal.y * (pointToCheck.y - point.y) +
        normal.z * (pointToCheck.z - point.z) == 0;
    }

    public bool IsPositiveToThePlane(Vector3 pointToCheck)
    {
        //Preguntar a lean que deber�a hacer si el punto est� exactamente en el plano
        return normal.x * (pointToCheck.x - point.x) +
        normal.y * (pointToCheck.y - point.y) +
        normal.z * (pointToCheck.z - point.z) > 0;
    }
}
