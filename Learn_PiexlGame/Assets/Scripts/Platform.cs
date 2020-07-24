using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 moveMent;
    private GameObject TopLine;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        TopLine = GameObject.Find("TopLine");
    }

    // Update is called once per frame
    void Update()
    {
        moveMent.y = Speed*Time.deltaTime;
        MovePlatform();
          
    }

    private void MovePlatform()
    {
        transform.position += moveMent;
        if(transform.position.y >= TopLine.gameObject.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
