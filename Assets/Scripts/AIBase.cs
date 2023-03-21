using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTurnTrigger))]
[RequireComponent(typeof(CellMovement))]
public abstract class AIBase : MonoBehaviour
{
    EnemyTurnTrigger turnTrigger;
    protected CellMovement cellMovement;
    public GameObject emptyRedSignal;

    // Start is called before the first frame update
    void Start()
    {
        cellMovement = GetComponent<CellMovement>();
        turnTrigger = GetComponent<EnemyTurnTrigger>();
        turnTrigger.onStartMovingEvent.AddListener(OnMove);
        cellMovement.movementCompletedEvent.AddListener(OnMoveEnds);

    }

    protected abstract bool AttackCondition();

    protected Vector2 GenerateDestination()
    {
        Vector2 currentPosition = transform.position;

        if (AttackCondition())
        {
            Vector2 playerPosition = Player.instance.transform.position;
            Vector2 vectorToPlayer = playerPosition - (Vector2)transform.position;
            Attack();
            return CellMovement.GetCurrentCell(playerPosition);
        }


        return CellMovement.GetCurrentCell(currentPosition + Vector2.down * CellMovement.METERS_PER_CELL);
    }

    void OnMove()
    {
        emptyRedSignal.SetActive(false);
        Vector2 destination = GenerateDestination();
        cellMovement.MoveTo(destination);
    }

    void OnMoveEnds() {
        if (attacking)
        {
            attacking= false;
            turnTrigger.onAttackPlayer.Invoke();
        }
        turnTrigger.NotifyMovementFinished();


        if (Player.instance.ValidMovement(transform.position))
        {
            emptyRedSignal.SetActive(true);
        }
    }

    bool attacking = false;

    protected void Attack()
    {
        attacking = true;
    }


}
