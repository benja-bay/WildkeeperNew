// #TEST
using UnityEngine;
using TMPro;

public class Hud : MonoBehaviour
{
    public TextMeshProUGUI Health;

    public void Update()
    {
        Health.text = GameManager.Instance.ActualHealth.ToString();
    }
    public void UpdateHealth(int actualHealth) 
    {
        Health.text = actualHealth.ToString();
    }
}
