using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private HashSet<string> _usedObjectIDs = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsObjectUsed(string objectID) => _usedObjectIDs.Contains(objectID);

    public void MarkObjectAsUsed(string objectID)
    {
        if (!string.IsNullOrEmpty(objectID))
            _usedObjectIDs.Add(objectID);
    }
}