using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health = 100;
    public GameObject gameManager;
    public Sprite spriteImage;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Display");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            gameManager.GetComponent<GameManager>().AddPointPlayer1();
        }
        // change sprite when health is less than 50
        else if (health <= 50)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteImage;
        }
    }
}
