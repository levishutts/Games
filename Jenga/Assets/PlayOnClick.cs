using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnClick : MonoBehaviour {
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player.SetActive(false);
    }
    public void StartGame()
    {
        Player.SetActive(true);
    }
}
