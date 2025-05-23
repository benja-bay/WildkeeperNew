using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Este script ajusta dinámicamente el orden de renderizado (sorting order) del SpriteRenderer
// para que el jugador aparezca delante o detrás de tiles (por ejemplo, troncos, hojas) según su posición.

// Asegura que el GameObject tenga un componente SpriteRenderer
[RequireComponent(typeof(SpriteRenderer))]
public class DynamicSorting : MonoBehaviour
{
    [Header("Sorting Layer a escanear")]
    [Tooltip("Nombre de la Sorting Layer donde están los Tilemaps interactivos (por ejemplo 'World').")]
    [SerializeField]
    private string targetSortingLayer = "World";
    // Nombre de la capa de orden de renderizado que queremos inspeccionar.

    [Header("Parámetros de búsqueda")]
    [Tooltip("Radio en celdas para buscar tiles alrededor del jugador.")]
    [SerializeField]
    private int searchRadius = 3;
    // Distancia, en número de celdas, que buscamos alrededor de la posición del jugador.

    [Header("Orden de renderizado")]
    [SerializeField]
    private int orderInFront = 10;   // Cuando el jugador debe ir delante del tile (por ejemplo, delante del tronco).
    [SerializeField]
    private int orderBehind = 0;     // Cuando el jugador debe ir detrás del tile (por ejemplo, bajo las hojas).

    // Referencia al componente SpriteRenderer del jugador
    private SpriteRenderer sr;
    // Lista para almacenar los Tilemaps encontrados en la escena
    private List<Tilemap> tilemaps = new List<Tilemap>();

    // Awake se ejecuta al inicializar el objeto, antes de Start.
    void Awake()
    {
        // Obtener y cachear el SpriteRenderer del mismo GameObject
        sr = GetComponent<SpriteRenderer>();

        // Encontrar todos los TilemapRenderer en la escena
        var renderers = FindObjectsOfType<TilemapRenderer>();
        foreach (var rend in renderers)
        {
            // Filtrar solo los que pertenecen a la capa de sorting que nos interesa
            if (rend.sortingLayerName == targetSortingLayer)
            {
                // Obtener el componente Tilemap asociado al renderer
                Tilemap tm = rend.GetComponent<Tilemap>();
                if (tm != null)
                    tilemaps.Add(tm);  // Agregar a la lista para futuras comprobaciones
            }
        }
    }

    // LateUpdate se llama después de Update en cada frame
    // Ideal para ajustes de cámara y orden de renderizado que dependen de la posición final.
    void LateUpdate()
    {
        // Posición actual del jugador en el mundo
        Vector3 playerPos = transform.position;

        // Variables para determinar el tile más cercano en Y
        float nearestDistY = float.MaxValue;  // Distancia vertical más pequeña encontrada
        float nearestTileY = 0f;              // Posición Y del tile más cercano
        bool found = false;                   // Marca si encontramos al menos un tile

        // Recorrer cada Tilemap almacenado
        foreach (var tm in tilemaps)
        {
            // Convertir la posición del jugador a coordenadas de celda en ese Tilemap
            Vector3Int centerCell = tm.WorldToCell(playerPos);

            // Explorar en un área cuadrada de lado (2*searchRadius+1)
            for (int dx = -searchRadius; dx <= searchRadius; dx++)
            {
                for (int dy = -searchRadius; dy <= searchRadius; dy++)
                {
                    // Cálculo de la celda actual a verificar
                    Vector3Int cell = new Vector3Int(
                        centerCell.x + dx,
                        centerCell.y + dy,
                        centerCell.z
                    );

                    // Verificar si existe un tile en esa celda
                    if (tm.HasTile(cell))
                    {
                        // Obtener la posición central del tile en el mundo (solo la Y importa)
                        float tileY = tm.GetCellCenterWorld(cell).y;
                        // Calcular distancia vertical absoluta entre jugador y tile
                        float distY = Mathf.Abs(playerPos.y - tileY);

                        // Si esta distancia es menor que la mejor encontrada hasta ahora,
                        // actualizar los valores
                        if (distY < nearestDistY)
                        {
                            nearestDistY = distY;
                            nearestTileY = tileY;
                            found = true;
                        }
                    }
                }
            }
        }

        // Si hemos encontrado al menos un tile:
        if (found)
        {
            // Comparar la posición Y del jugador con la del tile más cercano
            // Si el jugador está debajo (playerPos.y < nearestTileY), aparece delante (orderInFront)
            // Si está encima, aparece detrás (orderBehind)
            sr.sortingOrder = (playerPos.y < nearestTileY)
                ? orderInFront
                : orderBehind;
        }
        // Si no se detecta ningún tile, no modificamos el sortingOrder,
        // manteniéndose el valor configurado en el editor o por otro sistema.
    }
}
