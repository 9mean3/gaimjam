using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    public int defendHP;

    public string[] attacknames;

    Animator ani;

    int attackIndex = 0;

    bool shouldDash;
    bool isGround;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        attackIndex = 0;
    }

    void Update()
    {
        Collider2D[] collider2Dstop = Physics2D.OverlapBoxAll(dashStopTransform.position, dashStopBoxSize, 0);
        foreach (Collider2D collider in collider2Dstop)
        {
            if (collider.tag == "Player")
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

        /*        if (canDefend)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        ani.SetBool("BlockIdle", true);
                        isDefending = true;
                    }
                    else if (Input.GetMouseButtonUp(1))
                    {
                        ani.SetBool("BlockIdle", false);
                        isDefending = false;
                    }
                }*/

        if (shouldDash && isDashing)
        {
            transform.position -= new Vector3(transform.localScale.x * dashingPower * Time.deltaTime, 0);
        }
    }

    void Defend(int damage)
    {
        int curDefendHP = defendHP;
        curDefendHP -= damage;
    }

    float time = 0;

    private void Attack()
    {
        if (time <= 0)
        {
            Debug.Log("readyforattack;");

            Debug.Log("공격");
            if (!shouldDash)
            {
                DefAttack();

                ani.SetTrigger(attacknames[attackIndex]);
            }
            else
            {
                StartCoroutine(DashAttack());
            }

            time = attackCoolTime;
            if (attackIndex >= 2)
            {
                attackIndex = 0;
            }
            else
            {
                attackIndex++;
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
            if (collider.tag == "Player")
            {
                collider.GetComponent<Player>().TakeDamage(damage);
            }
        }
        //target에게 대미지
        Debug.Log("rmswjq");
    }

    IEnumerator DashAttack()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashingTime);
        Debug.Log("dd");
        isDashing = false;
    }

    //상대 쳐다보기
    private void LookAt()
    {
        GameObject[] en = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < en.Length; i++)
        {
            if (transform.position.x > en[i].transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x < en[i].transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        ani.SetTrigger("Hit");
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0.2f;
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
