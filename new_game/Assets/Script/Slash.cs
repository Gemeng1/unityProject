using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private float hurtValue;
    private float hurtMax = 30, hurtMin = 1;
    public void EndAttack()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy.state == EnemyState.Idle && enemy.tag == "Enemy")
        { 
            hurtValue = Random.Range(hurtMin, hurtMax);
            collision.gameObject.GetComponent<Enemy>().danmager(hurtValue);
            Vector2 difference = collision.transform.position - gameObject.transform.position;
            collision.transform.position = new Vector2(collision.transform.position.x + difference.x/2,
                                                       collision.transform.position.y + difference.y/2);
        }
    }

}
