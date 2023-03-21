using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopAI : AIBase
{
    protected override bool AttackCondition()
    {
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 vectorToPlayer = playerPosition - (Vector2)transform.position;

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
