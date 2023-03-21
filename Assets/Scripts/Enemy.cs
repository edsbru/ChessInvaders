using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject effect;
    [SerializeField] private float pointsAmount;
    [SerializeField] private Score pointsNumber;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        //que el enemigo compureve si el player esta al alcance y sino que baje una casilla hacia abajo

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pointsNumber.GetPoints(pointsAmount);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
