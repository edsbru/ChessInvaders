using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : AIBase
{
    protected override bool AttackCondition()
    {
        List<Vector2> possiblePositions = new List<Vector2>() {
           (Vector2)transform.position+ Vector2.right * 2f + Vector2.up,
           (Vector2)transform.position+ Vector2.left * 2f + Vector2.up,
           (Vector2)transform.position+ Vector2.right * 2f + Vector2.down,
           (Vector2)transform.position+ Vector2.left * 2f + Vector2.down,
           (Vector2)transform.position+ Vector2.right + Vector2.up * 2f,
           (Vector2)transform.position+ Vector2.left + Vector2.up * 2f,
           (Vector2)transform.position+ Vector2.right + Vector2.down * 2f,
           (Vector2)transform.position+ Vector2.left + Vector2.down * 2f,
        };

        Vector2 playerPos = Player.instance.transform.position;

        foreach (var possiblePosition in possiblePositions)
        {

            if(Vector2.Distance(playerPos, possiblePosition) < 0.1f)
            {
                return true;
            }
        }

        return false;
    }
}
