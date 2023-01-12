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

    public bool isDashing;
    public bool canDefend;
    public bool isDefending;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public int defendHP;
    public int defendCooldown;
    public bool isStun;
    float curStunTime;
    public float stunTime;

    public bool mouseInput = true;

    public string[] attacknames;

    Animator ani;

    int attackIndex = 0;

    bool shouldDash;
    bool isGround;
    bool timerOn = false;

    public float curDefendTime;

    float dfTrigger;
    bool dfbool;
    float dfTime;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        attackIndex = 0;
        curDefendTime = -1;
        curDefendHP = defendHP;
        originSpeed = attackCoolTime;
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
        if (!isDefending && !isStun && !defAttackStun)
            Attack();
        Defend();


        if (timerOn)
        {
            canDefend = false;
            curDefendTime += Time.deltaTime;
            if (curDefendTime >= defendCooldown && mouseInput)
            {
                canDefend = true;
                timerOn = false;
                curDefendTime = 0;
            }
        }

        if (dfbool)
        {
            dfTime = Random.Range(2, 5);
            if (dfTime >= 0)
            {
                isDefending = true;
                dfTime -= Time.deltaTime;
            }
            else
                dfTrigger = Random.Range(6, 10);
        }
        if (dfTrigger <= 0 && canDefend)
        {
            dfbool = true;
        }
        else
            dfTrigger -= Time.deltaTime;

        if (shouldDash && isDashing)
        {
            transform.position += new Vector3(transform.localScale.x * dashingPower * Time.deltaTime, 0);
        }

        //Debug.Log(curDefendTime);
    }

    private void Defend()
    {
        if (curDefendHP <= 0)
        {
            isStun = true;
            isDefending = false;
            ani.SetBool("IdleBlock", false);
            curStunTime += Time.deltaTime;
        }
        if (curStunTime >= stunTime)
        {
            isStun = false;
            ani.SetBool("IdleBlock", false);
            curStunTime = 0;
            curDefendHP = defendHP;

        }
        if (!isStun && !defAttackStun)
        {
            if (mouseInput && canDefend)
            {
                isDefending = true;
                ani.SetBool("IdleBlock", true);
            }
            else if (!mouseInput)
            {
                isDefending = false;
                ani.SetBool("IdleBlock", false);
                canDefend = true;
                timerOn = true;
                curDefendHP = defendHP;
            }

        }
    }

    public int curDefendHP;





    float time = 0;

    float originSpeed;

    void NB(GameObject target)
    {
        target.GetComponent<Rigidbody2D>().AddForce(new Vector2(10000 * -target.transform.localScale.x, 10000f));
        Debug.Log("BN:");
    }
    float attackIndexResetTime;

    private void Attack()
    {
        if (time <= 0)
        {


            Debug.Log("공격");
            if (attackIndex < 2)
                attackCoolTime = 0.2f;
            else
            {
                attackCoolTime = originSpeed;
            }

            if (!shouldDash)
            {
                //DefAttack();
                ani.SetTrigger(attacknames[attackIndex]);
                if (isDashing)
                {
                    DefAttack();
                    ani.SetTrigger(attacknames[attackIndex]);
                }
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
            {
                StartCoroutine(DashAttack());
            }

            time = attackCoolTime;


        }
        else
            time -= Time.deltaTime;
    }

    public void DefAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackTransform.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
                if (attackIndex == 0)
                    NB(collider.gameObject);
            }
        }
        //target에게 대미지
    }

    IEnumerator DashAttack()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashingTime);
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
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < en[i].transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }







    bool defAttackStun;
    public void DefAttackStunFalse()
    {
        defAttackStun = false;
    }
    public void TakeDamage(int damage)
    {
        if (!isDefending)
        {
            ani.SetTrigger("Hurt");
            hp -= damage;
            defAttackStun = true;
        }
        else
        {
            ani.SetTrigger("Block");
            curDefendHP -= damage;
        }

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ani.SetTrigger("Death");
        Debug.Log("PlayerDie");
    }
}
