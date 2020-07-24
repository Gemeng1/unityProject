using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Attacked,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private float move_speed;
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtTime;
    [SerializeField] private float curHurtTime;
    public GameObject effect;
    public float hp;
    private Transform target;
    private SpriteRenderer renderer;
    public EnemyState state;
    public GameObject canvas;
    void Start()
    {
        hp = maxHp;
        state = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, move_speed * Time.deltaTime);
        if(curHurtTime <= 0)
        {
            setFlashAmount(0);
        }
        else
        {
            curHurtTime -= Time.deltaTime;
        }
    }

    public void danmager(float times)
    {
        state = EnemyState.Attacked;
        StartCoroutine("changeState");
        hp -= times;
        //Vector2 difference = transform.position - target.position;
        Instantiate(canvas, transform.position, Quaternion.identity).GetComponent<ShowText>().showText( Mathf.Ceil(times));
        StartCoroutine(FindObjectOfType<CameControll>().CameShaker(0.2f, 0.1f));
        setFlashAmount(1);
        curHurtTime = hurtTime;
        
        if (hp <= 0)
        {
            Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject, 0.02f);
        }
        
    }

    private void setFlashAmount(float amount)
    {
        renderer.material.SetFloat("_FlashAmount", amount);
    }

    IEnumerator changeState()
    {
        yield return null;
        state = EnemyState.Idle;
    }
}
