using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-5.4  5.6  -5.2 4.2
public class CameControll : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Transform target;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5.4f, 5.6f), Mathf.Clamp(transform.position.y, -5.2f, 4.2f), transform.position.z);
        
    }

    public IEnumerator CameShaker(float maxTime,float _amount)
    {
        Vector3 orth_pos = gameObject.transform.localPosition;
        float shakeTime = 0.0f;
        while(shakeTime < maxTime)
        {
            float x = Random.Range(-1, 1)*_amount;
            float y = Random.Range(-1, 1)*_amount;
            transform.localPosition = new Vector3(orth_pos.x+x, orth_pos.y+y, orth_pos.z);
            shakeTime += Time.deltaTime;
            yield return null;
        }
            
    }
}
