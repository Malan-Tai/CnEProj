using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ManeuverAgent : MonoBehaviour
{
    [SerializeField][CanBeNull] private Transform _start;
    [SerializeField] private Transform _end;

    // Start is called before the first frame update
    void Start()
    {
        _end.gameObject.SetActive(false);
        if (_start == null) // start = null means start is the origin of the object
        {
            _start = this.transform;
        }
        else
        {
            _start.gameObject.SetActive(false);
        }
    }

    // Position this object's start point at the end of the other's end point
    public void SetUpAtEndOf(ManeuverAgent other)
    {
        transform.rotation = other._end.rotation;
        transform.position = other._end.position + transform.position - _start.position;
    }
}
