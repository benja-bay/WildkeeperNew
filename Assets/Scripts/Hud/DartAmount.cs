using UnityEngine;
using TMPro;
using Items;
using Player;

namespace HUD
{
    public class DartAmount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentAmmo;
        [SerializeField] private ItemSO _dartItem;

        private Inventory _inventory;

        private void Start()
        {
            var playerGO = GameObject.FindWithTag("Player");
            var _player = playerGO.GetComponent<Player.Player>();
            _inventory = _player.Inventory;

            UpdateDartText();
        }

        private void Update()
        {
            UpdateDartText();
        }

        private void UpdateDartText()
        {
            int dartCount = _inventory.GetItemCount(_dartItem);
            _currentAmmo.text = $"{dartCount}/{_dartItem.maxAmount}";
        }
    }
}