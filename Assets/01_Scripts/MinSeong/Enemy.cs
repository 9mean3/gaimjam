using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform attackTransform;
    [SerializeField] Vector2 boxSize;

    public int hp;
    public float moveSpeed;
    public float jumpPower;
    public int damage;
    public float attackCoolTime;

    Rigidbody2D rb;

    bool isGround;

    float time = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (time <= 0)
        {

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackTransform.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().TakeDamage(damage);
                }
            }
            time = attackCoolTime;

        }
        else
            time -= Time.deltaTime;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            rb.velocity = Vector2.up * jumpPower;
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);

        if (x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Died"); Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackTransform.position, boxSize);
    }
}
