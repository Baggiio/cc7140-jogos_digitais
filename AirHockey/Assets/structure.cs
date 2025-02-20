using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structure : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int health = 100;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        UpdateColor();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void UpdateColor()
    {
        float healthPercentage = (float)health / 100;
        spriteRenderer.color = new Color(1, healthPercentage, healthPercentage);
    }

    public void ResetStructure()
    {
        health = 100;
        UpdateColor();
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}