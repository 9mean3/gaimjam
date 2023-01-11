using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public bool isDefending;
    public int defendHP;
    private void Update()
    {
       
    }
    public void TakeDamage(int damage)
    {

        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die");
    }
}
