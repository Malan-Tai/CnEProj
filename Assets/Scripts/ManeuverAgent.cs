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
        //_end.gameObject.SetActive(false);
        if (_start == null)
        {
            _start = this.transform;
        }
        else
        {
            //_start.gameObject.SetActive(false);
        }
    }

    public void SetUpAtEndOf(ManeuverAgent other)
    {
        /*Vector3 positionDiff = other._end.position - _start!.position;
        other._end.rotation.ToAngleAxis(out float otherAngle, out Vector3 _1);
        _start.rotation.ToAngleAxis(out float startAngle, out Vector3 _2);

        transform.Translate(positionDiff);
        transform.RotateAround(_start!.position, Vector3.forward, otherAngle - startAngle);*/

        transform.rotation = other._end.rotation;
        transform.position = other._end.position + transform.position - _start.position;
    }
}
