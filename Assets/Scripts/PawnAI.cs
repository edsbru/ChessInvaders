using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAI : AIBase
{
    protected override bool AttackCondition()
    {
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 vectorToPlayer = playerPosition - (Vector2)transform.position;
        float distance = vectorToPlayer.magnitude;
        const float cellSize = CellMovement.METERS_PER_CELL;

        float cellDiagnoal = Mathf.Sqrt(cellSize * cellSize + cellSize * cellSize);

        float dotOfDiagonal = Vector2.Dot(Vector2.up.normalized, Vector2.one.normalized);


        if (distance <= cellDiagnoal + 0.01f)
        {
            float dot = Vector2.Dot(Vector2.down, vectorToPlayer.normalized);
            if (dot >= dotOfDiagonal + 0.01f)
            {
                return true;
            }
        }

        return false;
    }
}
