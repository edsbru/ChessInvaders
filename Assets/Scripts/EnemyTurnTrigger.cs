using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurnTrigger : MonoBehaviour
{
    public int points;

    public bool moving = false;

    public UnityEvent onStartMovingEvent = new UnityEvent();
    public UnityEvent onAttackPlayer = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.instance.OnEnemySpawned(this);  
    }

    // Esta funcion debe llamarse desde los scripts de la IA
    // una vez el movimineto se ha terminado para avisar el turn manager.
    public void NotifyMovementFinished()
    {
        moving = false;

    }

    public void OnEnemyMoveStarts()
    {
        moving = true;
        onStartMovingEvent.Invoke();
    }

}
