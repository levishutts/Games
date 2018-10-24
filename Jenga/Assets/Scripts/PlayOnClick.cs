using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnClick : MonoBehaviour {
    GameObject Player;
    Canvas StartMenu;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player.SetActive(false);
        StartMenu = GetComponentInParent<Canvas>();
    }
    public void StartGame()
    {
        Player.SetActive(true);
        
        StartMenu.enabled = false;
    }
}
