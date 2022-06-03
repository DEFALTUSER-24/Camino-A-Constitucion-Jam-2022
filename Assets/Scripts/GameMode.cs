using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode Game;

    // Start is called before the first frame update
    void Start()
    {
        Game = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPassengers()
    {
        Debug.Log("Passengers resetted");
    }
}
