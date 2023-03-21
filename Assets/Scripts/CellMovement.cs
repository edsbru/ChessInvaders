using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellMovement : MonoBehaviour
{
    //constantes necesarias para las mates, no tocar x favor.
    public const float METERS_PER_CELL = 1f;
    public const float HALF_CELL_SIZE = METERS_PER_CELL * 0.5f;
    public const float MOVEMENT_DURATION = 0.7f;
    public const float MOVING_SCALE = 0.2f;

    

    public AnimationCurve curvaAnimacionMovimiento;
    public AnimationCurve efecto3DAnimacionMovimiento;

    bool isMoving = false;

    public UnityEvent movementCompletedEvent;

    private void Awake()
    {
        movementCompletedEvent = new UnityEvent();
        transform.position = GetCurrentCell();

    }

    Vector2 GetCurrentCell()
    {
        return GetCurrentCell(transform.position);
    }

    /**
     Convierte unas coordenadas en espacio de mundo a espacio de celda.
     */
    public static Vector2 GetCurrentCell(Vector2 worldPosition)
    {

        // A continuación:
        // 1 - Le quitamos el decimal y lo multiplicamos por el numero
        // de metros por celda para pasar a espacio de celda
        // 2 - Lo centramos en la celda sumandole la mitad del tamaño de la celda
        // ya que el punto origen en unity esta por defecto en el centro.
        worldPosition.y = ((int)worldPosition.y) * METERS_PER_CELL + HALF_CELL_SIZE - METERS_PER_CELL;
        worldPosition.x = ((int)worldPosition.x) * METERS_PER_CELL + HALF_CELL_SIZE;


        return worldPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void MoveTo(Vector2 cellCoord)
    {
        if (isMoving == false)
        {
            StartCoroutine(MovementRoutine(transform.position, cellCoord));
        }
    }
    /**
     * Rutina que mueve la ficha de un punto a otro.
     */
    IEnumerator MovementRoutine(Vector2 origin, Vector2 destiny)
    {
        isMoving = true;
        // Conforme la ficha se mueve su escala se hace mas grande
        // para simular que "sube" como si fuera 3D.
        // para poder hacer esto nos guardamos los valores originales de la escala.
        Vector3 orignalScale = transform.localScale;
        Vector3 upScale = transform.localScale + Vector3.one * MOVING_SCALE;


        // La animacion de movimiento tiene una duración
        // por cada frame, mientras el tiempo transcurrido sea menor
        // a la duración, actualizamos la posicion
        float timeCount = 0f;
        while (timeCount < MOVEMENT_DURATION)
        {
            // actualizar el contador de tiempo
            timeCount += Time.deltaTime;

            // en funcion del contador del tiempo calculamos el porcentaje de progreso.
            // EJEMPLO: si estamos en la mitad del tiempo, progress = 0.5 (50%).
            // Si estamos en 3 cuartos de la duracion, progress = 0.75 (75%)
            float timeProgress = timeCount / MOVEMENT_DURATION;

            // transformar el progrso en el tiempo en progreso del movimiento
            // en función del animation curve configurado (curvaAnimacionMovimiento)
            float movementProgress = curvaAnimacionMovimiento.Evaluate(timeProgress);

            // Formula de interpolación lineal:
            // EstadoActual = B * PorcentajeProgreso + A * (100% - PorcentajeProgreso)
            
            // Pasar de progreso de movimiento a posicion
            Vector3 newPos = destiny * movementProgress + origin * (1f - movementProgress);
            // Seteamos la nueva posicion
            transform.position = newPos;

            // Hacemos lo mismo con la escala de la ficha para que parezca que se levanta:
            float zProgress = efecto3DAnimacionMovimiento.Evaluate(timeProgress);
            transform.localScale = zProgress * upScale + orignalScale * (1f - zProgress);

            yield return null;
        }

        // Cuando termina la animacion nos aseguramos de que los valores son correctos.
        transform.position = new Vector3(destiny.x, destiny.y, -0.1f);
        transform.localScale = orignalScale;
        isMoving= false;

        movementCompletedEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) {
            MoveTo(GetCurrentCell()+Vector2.up*2f + Vector2.right);
        }
    }
}
