using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float damage;
    Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Animator.SetTrigger("Attack01");
        }
    }


}
