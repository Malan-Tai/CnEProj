using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Based on code from https://en.wikibooks.org/wiki/Cg_Programming/Unity/Hermite_Curves
 */

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class HermiteSpline : MonoBehaviour
{
	[SerializeField] private List<HermitSplinePoint> _controlPoints = new();
	[SerializeField] private Color _color = Color.white;
	[SerializeField] private float _width = 0.2f;
	[SerializeField] private int _numberOfPoints = 20;
	LineRenderer _lineRenderer;	

	void Start () 
	{
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.useWorldSpace = true;
	}
	
	void Update () 
	{
		if (null == _lineRenderer || _controlPoints == null 
			|| _controlPoints.Count < 2)
   		{
      			return; // not enough points specified
   		}

		// update line renderer
		_lineRenderer.startColor = _color;
		_lineRenderer.endColor = _color;
   		_lineRenderer.startWidth = _width;
		_lineRenderer.endWidth = _width;
		if (_numberOfPoints < 2)
   		{
      			_numberOfPoints = 2;
   		}
		_lineRenderer.positionCount = _numberOfPoints * (_controlPoints.Count - 1);

		// loop over segments of spline
		Vector3 p0, p1, m0, m1;

		for(int j = 0; j < _controlPoints.Count - 1; j++)
		{
			// check control points
			if (_controlPoints[j] == null || 
				_controlPoints[j + 1] == null ||
				(j > 0 && _controlPoints[j - 1] == null) ||
				(j < _controlPoints.Count - 2 && _controlPoints[j + 2] == null))
			{
				return;  
			}
			// determine control points of segment
			p0 = _controlPoints[j].transform.position;
			p1 = _controlPoints[j + 1].transform.position;

			m0 = _controlPoints[j].GetTangent();
			m1 = _controlPoints[j + 1].GetTangent();

			// set points of Hermite curve
			Vector3 position;
			float t;
			float pointStep = 1.0f / _numberOfPoints;

			if (j == _controlPoints.Count - 2)
			{
				pointStep = 1.0f / (_numberOfPoints - 1.0f);
				// last point of last segment should reach p1
			}  
			for(int i = 0; i < _numberOfPoints; i++) 
			{
				t = i * pointStep;
				position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 
					+ (t * t * t - 2.0f * t * t + t) * m0 
					+ (-2.0f * t * t * t + 3.0f * t * t) * p1 
					+ (t * t * t - t * t) * m1;
				_lineRenderer.SetPosition(i + j * _numberOfPoints, 
					position);
			}
		}
	}
}