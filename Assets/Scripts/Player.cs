using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CellMovement))]
public class Player : MonoBehaviour
{

    private CellMovement cellMovement;

    // Start is called before the first frame update
    void Start()
    {
        cellMovement = GetComponent<CellMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cellMovement.MoveTo(CellMovement.GetCurrentCell(mousePosition));
        }
        
    }

    void enemyKill()
    {
        
    }
}
