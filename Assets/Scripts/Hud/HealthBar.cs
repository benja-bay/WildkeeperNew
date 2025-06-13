using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Image healthBarFill;
        [SerializeField] private TextMeshProUGUI  healthBarText;

        private void Update()
        {
            healthBarFill.fillAmount = (float) health.CurrentHealth / health.MaxHealth;
            healthBarText.text = health.CurrentHealth.ToString();
        }
    }
}
