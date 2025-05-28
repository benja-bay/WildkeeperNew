using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private Transform _player; // Reference to the player's transform
    [SerializeField] private float _distance; // Distance from the player to place the hitbox

    private void Update()
    {
        UpdatePositionAndRotation();
    }
   
    public void UpdatePositionAndRotation()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3 (0, 0, angle);

        Vector3 playerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _player.position;
        playerToMouseDir.z = 0;
        transform.position = _player.position + (_distance * playerToMouseDir.normalized);

        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else 
        {
            localScale.y = 1f;
        }
        transform.localScale = localScale;
    }
}
