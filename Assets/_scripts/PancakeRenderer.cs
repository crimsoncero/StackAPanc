using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PancakeRenderer : MonoBehaviour
{

    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private List<Transform> _points;


    private void Awake()
    {
        _renderer.positionCount = _points.Count;
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    private void OnDrawGizmos()
    {
        _renderer.positionCount = _points.Count;
        DrawLine();
    }

    private void DrawLine()
    {
        _renderer.SetPositions(_points.Select(p => p.position).ToArray());
    }

   

}
