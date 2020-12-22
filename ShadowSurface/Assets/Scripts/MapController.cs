using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    
    public static MapController game_controller;
    public GameObject player;
       
    // Start is called before the first frame update
    void Start()
    {
        game_controller = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move_player(Vector2 move_loc)
    {
        player.transform.position = move_loc;
    }
}
