using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CellMovement))]
public class Player : MonoBehaviour
{

    private CellMovement cellMovement;

    // Start is called before the first frame update
    void Start()
    {
        cellMovement = GetComponent<CellMovement>();
        //en enemy
        if (this.ValidMovement(transform.position))
        {
            //pinto casilla en rojo
        }
        else
        {
            //no pinto nada
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(ValidMovement(mousePosition))
            {
                cellMovement.MoveTo(CellMovement.GetCurrentCell(mousePosition));
            }

           
        }
        
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
        if (desiredMovement.x < 2.5 || desiredMovement.x > 9.5 || desiredMovement.y > -3.5 || desiredMovement.y < -12.5)
        {
            return false;
        }
        if (personaje.x == destino.x || personaje.y == destino.y || difX == difY)
            return true;

        return false;
    }
}
