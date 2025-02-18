using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d; 
    void GoBall(){                      
        float rand = Random.Range(0, 2);
        if(rand < 1){
            rb2d.AddForce(new Vector2(-15, -20));
        } else {
            rb2d.AddForce(new Vector2(-15, 20));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        Invoke("GoBall", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rb2d.velocity.magnitude > 5)
        {
            rb2d.velocity *= 0.999f; // Reduce velocity by 1% every frame
        }
        else if (rb2d.velocity.magnitude < 5)
        {
            rb2d.velocity = rb2d.velocity.normalized * 5; // Set velocity magnitude to 10
        }
        
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")){
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);

            if (vel.magnitude > 30)
            {
                vel = vel.normalized * 30;
            }

            rb2d.velocity = vel;
        }
    }

    void ResetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }


}
