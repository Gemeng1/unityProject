using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
           
           
        }
    }
    void Attack()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float roateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, roateZ);
        transform.GetChild(0).gameObject.SetActive(true);
    }

  
}

