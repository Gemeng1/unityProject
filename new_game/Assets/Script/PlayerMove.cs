using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float MoveH, MoveV;
    [SerializeField] private float MoveSpeed;
    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveH = Input.GetAxisRaw("Horizontal")*MoveSpeed;
        MoveV = Input.GetAxisRaw("Vertical")*MoveSpeed;
        Flip();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(MoveH, MoveV);
    }

    void Flip()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.eulerAngles = new Vector3(0, transform.position.x < pos.x ? 0 : 180, 0);
    }
}
