using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rig;
    private void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveh = Input.GetAxisRaw("Horizontal")*moveSpeed;
        float movev = Input.GetAxisRaw("Vertical")*moveSpeed;
        rig.velocity = new Vector2(moveh, movev);
    }
}
