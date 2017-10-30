using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public static GameState _instance = null;

    public PlayerStats stats;

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        else if (_instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        stats = gameObject.AddComponent<PlayerStats>();
    }
}
