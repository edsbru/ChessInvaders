using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CellMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    // Gracias a "static" la variable se crea en el scope global y
    // así podremos acceder al player desde los scripts de enemigos.
    public static Player instance;

    public GameObject possibleMovementsPrevisualization;

    public CellMovement cellMovement;
    public PlayerHealth playerHealth;

    private Vector2 spawnPosition;


    public void OnAttacked()
    {
        transform.position = spawnPosition;
        playerHealth.RecieveDamage();
    }

    private void Awake()
    {
        instance = this;
        cellMovement = GetComponent<CellMovement>();
        playerHealth = GetComponent<PlayerHealth>();

    }

    // Start is called before the first frame update
    void Start()
    {   
        possibleMovementsPrevisualization.SetActive(true);
        TurnManager.instance.allEnemyMovementFinishedEvent.AddListener(OnEnemyMovementCompleted);

        //en enemy
        if (this.ValidMovement(transform.position))
        {
            //pinto casilla en rojo
        }
        else
        {
            //no pinto nada
        }

        spawnPosition = transform.position;

    }

    void OnEnemyMovementCompleted()
    {
        possibleMovementsPrevisualization.SetActive(true);
    }

    void OnMovementCompleted()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(ValidMovement(mousePosition))
            {
                StartMovement(CellMovement.GetCurrentCell(mousePosition));
            }
        }
        
    }

    private void StartMovement(Vector2 destination)
    {
        possibleMovementsPrevisualization.SetActive(false);
        cellMovement.MoveTo(destination);
    }

    public bool ValidMovement(Vector2 desiredMovement)
    {
        Vector2 personaje = CellMovement.GetCurrentCell(transform.position);
        Vector2 destino = CellMovement.GetCurrentCell(desiredMovement);

        float difX = personaje.x - destino.x;
        float difY = personaje.y - destino.y;
        difX = Mathf.Abs(difX);
        difY = Mathf.Abs(difY);

        if (personaje == destino)
        {
            return false;
        }
        if (desiredMovement.x < 2.5 || desiredMovement.x > 9.5 || desiredMovement.y > 3f || desiredMovement.y < -12.5)
        {
            return false;
        }
        
        if(DiagonalCheckValidMovement(destino) || HorVerCheckValidMovement(destino)){
            return true;
        }

        return false;
    }

    private bool HorVerCheckValidMovement(Vector2 targetPos)
    {
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 vectorToPlayer = playerPosition - targetPos;

        float dotVertical = Mathf.Abs(Vector2.Dot(Vector2.up, vectorToPlayer.normalized));
        float dotHorizontal = Mathf.Abs(Vector2.Dot(Vector2.right, vectorToPlayer.normalized));

        const float threshold = 0.9999f;
        if (dotVertical >= threshold || dotHorizontal >= threshold)
        {
            return true;
        }

        return false;
    }

    private bool DiagonalCheckValidMovement(Vector2 targetPos)

    {
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 vectorToPlayer = playerPosition - targetPos;

        Vector2 diag1 = Vector2.one.normalized;
        Vector2 diag2 = new Vector2(1f, -1f).normalized;

        float dot1 = Mathf.Abs(Vector2.Dot(diag1, vectorToPlayer.normalized));
        float dot2 = Mathf.Abs(Vector2.Dot(diag2, vectorToPlayer.normalized));

        if (dot1 >= 0.999f || dot2 >= 0.999f)
        {

            return true;
        }

        return false;
    }


}
