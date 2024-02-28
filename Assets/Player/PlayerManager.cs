using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    class Player
    {
        public Rigidbody2D rb;
        public SpriteRenderer renderer;
    }
    public GameObject playerPrefab;
    Player player;
    void Start()
    {
        GameObject playerObject = Instantiate(playerPrefab);
        player = new Player();
        player.rb = playerObject.GetComponent<Rigidbody2D>();
        player.renderer = playerObject.GetComponent<SpriteRenderer>();
    }
}
