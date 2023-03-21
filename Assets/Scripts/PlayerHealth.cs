using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;

    public UnityEvent playerDiesEvent = new UnityEvent();

    public void RecieveDamage()
    {
        health--;
        if (health <= 0)
        {
            playerDiesEvent.Invoke();
        }
    }
}
