using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpriteRenderer))]
public class DynamicSorting : MonoBehaviour
{
    [Header("Sorting Layer a escanear")]
    [Tooltip("Nombre de la Sorting Layer donde est�n los Tilemaps interactivos (por ejemplo 'World').")]
    public string targetSortingLayer = "World";

    [Header("Par�metros de b�squeda")]
    [Tooltip("Radio en celdas para buscar tiles alrededor del jugador.")]
    public int searchRadius = 3;

    [Header("Orden de renderizado")]
    public int orderInFront = 10;   // Cuando est� debajo del tile (delante del tronco/objeto)
    public int orderBehind = 0;     // Cuando est� encima del tile (detr�s de hojas/objetos)

    private SpriteRenderer sr;
    private List<Tilemap> tilemaps = new List<Tilemap>();

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // Encontrar todos los TilemapRenderer en la escena
        var renderers = FindObjectsOfType<TilemapRenderer>();
        foreach (var rend in renderers)
        {
            // Filtrar por Sorting Layer
            if (rend.sortingLayerName == targetSortingLayer)
            {
                Tilemap tm = rend.GetComponent<Tilemap>();
                if (tm != null)
                    tilemaps.Add(tm);
            }
        }
    }

    void LateUpdate()
    {
        Vector3 playerPos = transform.position;
        float nearestDistY = float.MaxValue;
        float nearestTileY = 0f;
        bool found = false;

        // Recorrer cada Tilemap agregado autom�ticamente
        foreach (var tm in tilemaps)
        {
            Vector3Int centerCell = tm.WorldToCell(playerPos);

            // Buscar en el �rea de radio
            for (int dx = -searchRadius; dx <= searchRadius; dx++)
            {
                for (int dy = -searchRadius; dy <= searchRadius; dy++)
                {
                    Vector3Int cell = new Vector3Int(centerCell.x + dx, centerCell.y + dy, centerCell.z);
                    if (tm.HasTile(cell))
                    {
                        float tileY = tm.GetCellCenterWorld(cell).y;
                        float distY = Mathf.Abs(playerPos.y - tileY);

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

        if (found)
        {
            // Ajustar sorting seg�n el tile m�s cercano
            sr.sortingOrder = (playerPos.y < nearestTileY) ? orderInFront : orderBehind;
        }
        // Si no se detecta ning�n tile, no modificamos el sortingOrder
    }
}