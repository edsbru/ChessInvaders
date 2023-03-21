using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : AIBase
{
    protected override bool AttackCondition()
    {
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 vectorToPlayer = playerPosition - (Vector2)transform.position;

        float dotVertical = Mathf.Abs(Vector2.Dot(Vector2.up, vectorToPlayer.normalized));
        float dotHorizontal = Mathf.Abs(Vector2.Dot(Vector2.right, vectorToPlayer.normalized));

        const float threshold = 0.9999f;
        if (dotVertical >= threshold || dotHorizontal >= threshold)
        {
            return true;
        }

        return false;
    }
}
