using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public bool isDefending;
    public int defendHP;

    Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
       
    }
    public void TakeDamage(int damage)
    {
        ani.SetTrigger("Hit");
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ani.SetTrigger("Death");
        //StartCoroutine(nameof(DieAniMation));
        //Debug.Log("Die");
    }
    IEnumerator DieAniMation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
