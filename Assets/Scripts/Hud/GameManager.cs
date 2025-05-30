// #TEST
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int ActualHealth {  get; private set; }
    public Hud Hud;

    void Awake() 
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Debug.LogWarning("Mas de un GameManager en la escena!!");
        }
    }

    public void ShowHealth(int health)
    {
        ActualHealth = health;
        Hud.UpdateHealth(ActualHealth);
    }
}
