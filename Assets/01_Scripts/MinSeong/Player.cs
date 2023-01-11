using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform attackTransform;
    [SerializeField] Vector2 boxSize;
    [SerializeField] Transform dashStopTransform;
    [SerializeField] Vector2 dashStopBoxSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackTransform.position, boxSize);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(dashStopTransform.position, dashStopBoxSize);
    }

    public int hp;

    public int damage;
    public float attackCoolTime;

    public bool canDash = true;
    public bool isDashing;
    public bool isAttacking;
    public bool canDefend;
    public bool isDefending;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    bool shouldDash;
    bool isGround;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Collider2D[] collider2Dstop = Physics2D.OverlapBoxAll(dashStopTransform.position, dashStopBoxSize, 0);
        foreach (Collider2D collider in collider2Dstop)
        {
            if (collider.tag == "Enemy")
            {
                shouldDash = false;
            }
            else
            {
                shouldDash = true;
            }
        }

        LookAt();
        Attack();

        if (canDefend)
        {
            if (Input.GetMouseButtonDown(2))
            {
                //StartCoroutine(Defend());
            }
            else if (Input.GetMouseButtonUp(2))
            {
                //StopCoroutine(Defend());
            }
        }


        if (shouldDash && isDashing)
        {
            transform.position += new Vector3(transform.localScale.x * dashingPower * Time.deltaTime, 0);
        }
    }

    //IEnumerator Defend()
    //{
        
    //}

    float time = 0;

    private void Attack()
    {
        if (time <= 0)
        {
            Debug.Log("readyforattack;");
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("����");
                if (!shouldDash)
                {
                    DefAttack();
                }
                else
                {
                    StartCoroutine(DashAttack());
                }


                time = attackCoolTime;
            }
        }
        else
            time -= Time.deltaTime;

    }
    void DefAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackTransform.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        //target���� �����
        Debug.Log("rmswjq");
    }

    IEnumerator DashAttack()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashingTime);
        Debug.Log("dd");
        isDashing = false;

    }
    private void LookAt()
    {
        GameObject[] en = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < en.Length; i++)
        {
            if (transform.position.x > en[i].transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < en[i].transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }









    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Debug.Log("PlayerDie");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
