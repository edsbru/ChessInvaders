using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    public static TurnManager instance;

    private List<EnemyTurnTrigger> enemies = new List<EnemyTurnTrigger>();

    public UnityEvent allEnemyMovementFinishedEvent;

    bool playerTurn = true;

    public void OnEnemySpawned(EnemyTurnTrigger enemy)
    {
        enemy.onAttackPlayer.AddListener(Player.instance.OnAttacked);
        enemies.Add(enemy);
    }

    private void Awake()
    {
        allEnemyMovementFinishedEvent = new UnityEvent();
        instance = this;
    }

    void OnPlayerMoved()
    {   
        SpawnEnemies();
        playerTurn = false;
        foreach (var e in enemies)
        {
            e.OnEnemyMoveStarts();
        }
    }

    bool EnemyCloseToOther(GameObject enemyCreated, List<GameObject> createdEnemies)
    {
        foreach (var enemy in createdEnemies)
        {
            if (Vector2.Distance(enemyCreated.transform.position, enemy.transform.position) < CellMovement.HALF_CELL_SIZE)
            {
                return true;
            }
        }
        return false;
    }

    void SpawnEnemies() {

        int minEnemies = 0;
        int maxEnemies = 4;
        if(enemies.Count == 0)
        {
            minEnemies = 1;
        }

        List<GameObject> createdEnemies = new List<GameObject>();
        int enemiesToSpawn = Random.Range(minEnemies, maxEnemies+1);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Count);

            var enemyCreated = Instantiate(enemyPrefabs[enemyIndex], new Vector3(Random.Range(2.36f, 9.45f), 4f, -0.42f), Quaternion.identity);
            while(EnemyCloseToOther(enemyCreated, createdEnemies))
            {
                enemyCreated.transform.position = new Vector3(Random.Range(2.36f, 9.45f), transform.position.y, transform.position.z);
            }

            createdEnemies.Add(enemyCreated);
        }


    }

    void OnAllEnemieMovementsFinished()
    {
        playerTurn = true;
        allEnemyMovementFinishedEvent.Invoke();

    }

    // Start is called before the first frame update
    void Start()
    {
        Player.instance.cellMovement.movementCompletedEvent.AddListener(OnPlayerMoved);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTurn)
        {
            bool enemiesFinishedMovement = true;
            foreach (var e in enemies)
            {
                if (e.moving)
                {
                    enemiesFinishedMovement = false;
                    break;
                }
            }

            if (enemiesFinishedMovement)
            {
                OnAllEnemieMovementsFinished();
            }
        }
    }
}
