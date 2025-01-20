using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitSplinePoint : MonoBehaviour
{
    [SerializeField] private float _tangentStrength;

    public Vector3 GetTangent()
    {
        return this.transform.rotation.eulerAngles * _tangentStrength;
    }
}
