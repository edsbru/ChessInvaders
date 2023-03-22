using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;
    Animator animator;

    public UnityEvent playerDiesEvent = new UnityEvent();

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RecieveDamage()
    {
        health--;
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            playerDiesEvent.Invoke();
        }
    }
}
