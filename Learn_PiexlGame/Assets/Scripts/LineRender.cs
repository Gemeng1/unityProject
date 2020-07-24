using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    private LineRenderer render;
    public Transform starPoint;
    public Transform endPoint;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        render.SetPosition(0, starPoint.transform.position);
        render.SetPosition(1, endPoint.transform.position);

    }
}
