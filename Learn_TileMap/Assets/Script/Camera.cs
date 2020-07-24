using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//8.06 8.9 3.9 25.9
public class Camera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,new Vector3(target.position.x, target.position.y,transform.position.z) , moveSpeed*Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(target.position.x, -8.7f, 8.7f),
                                           Mathf.Clamp(target.position.y, -3.9f, 25.9f),
                                           transform.position.z);
    }
}
