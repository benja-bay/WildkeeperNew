using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _distance;
    [SerializeField] private Camera _camera;

    // Booleans para cada dirección
    public bool isRight;
    public bool isLeft;
    public bool isUp;
    public bool isDown;

    private void Update()
    {
        Follow();
        DetectDirection();
    }

    private void Follow()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);      //Obtiene la posición del mouse en el mundo 2D/3D de Unity, no en la pantalla
        mousePos.z = 0f;                                                        //Se asegura de que el objeto se mantenga en el plano 2D
        Vector3 direction = (mousePos - _player.position).normalized;            //Calcula un vector unitario que apunta desde el jugador hacia el mouse
        transform.position = _player.position + direction * _distance;           //Posiciona el objeto a cierta distancia desde el jugador en dirección al mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    //Calcula el ángulo de rotación que debe tener el objeto para mirar hacia el mouse
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void DetectDirection()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - _player.position).normalized;

        // Reiniciar todos los booleanos
        isRight = false;
        isLeft = false;
        isUp = false;
        isDown = false;

        // Determinar cuál componente es dominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                isRight = true;
                Debug.Log("El cursor se encuentra: derecha");
            }
            else
            {
                isLeft = true;
                Debug.Log("El cursor se encuentra: izquierda");
            }
        }
        else
        {
            if (direction.y > 0)
            {
                isUp = true;
                Debug.Log("El cursor se encuentra: arriba");
            }
            else
            {
                isDown = true;
                Debug.Log("El cursor se encuentra: abajo");
            }
        }
    }

}
