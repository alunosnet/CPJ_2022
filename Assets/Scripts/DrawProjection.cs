using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    arco _arco;
    LineRenderer lineRenderer;

    // Number of points on the line
    public int MaxDistance = 50;

    // distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;
    void Start()
    {
        _arco = GetComponent<arco>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (_arco != null && _arco.Carregar)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = (int)MaxDistance;
            List<Vector3> points = new List<Vector3>();
            Vector3 startingPosition = _arco.transform.position;
            Vector3 startingVelocity = _arco.transform.forward * _arco.Forca;
            for (float t = 0; t < MaxDistance; t += timeBetweenPoints)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint,0.5f, CollidableLayers).Length > 0)
                {
                    //Debug.Log("Colidi " + points.Count);
                    lineRenderer.positionCount = points.Count;
                    break;
                }
            }

            lineRenderer.SetPositions(points.ToArray());
        }
        else
        {
            lineRenderer.enabled= false;
        }
    }
}
