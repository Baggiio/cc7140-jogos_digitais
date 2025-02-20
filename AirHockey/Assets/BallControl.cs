using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d; 
    public AudioSource source;
    public static int nextSpawn = -1;

    public void setNextSpawn(int spawn){
        nextSpawn = spawn;
    }

    void GoBall(){

        if (nextSpawn == -1) {
            float rand = Random.Range(0, 2);
            if(rand < 1){
                rb2d.AddForce(new Vector2(-15, -20));
            } else {
                rb2d.AddForce(new Vector2(-15, 20));
            }
        }
    }

    void Shoot(){
        rb2d.AddForce(new Vector2(-15, -20));
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        source = GetComponent<AudioSource>();
        Invoke("GoBall", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rb2d.velocity.magnitude > 5)
        {
            rb2d.velocity *= (1 - 0.5f * Time.deltaTime); // Reduce velocity by 0.1% per second
        }
        else if (rb2d.velocity.magnitude < 5)
        {
            rb2d.velocity = rb2d.velocity.normalized * 5; // Set velocity magnitude to 10
        }

        if (rb2d.velocity.x == 0 && rb2d.velocity.y > 0) {
            rb2d.AddForce(new Vector2(1, 0));
        } else if (rb2d.velocity.x == 0 && rb2d.velocity.y < 0) {
            rb2d.AddForce(new Vector2(1, 0));
        }
        
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player") || coll.collider.CompareTag("IA")) {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);

            if (vel.magnitude > 15)
            {
                vel = vel.normalized * 15;
            }

            rb2d.velocity = vel;
        }

        source.Play();
    }

    void ResetBall()
    {
        Vector2 spawnPos;

        if (nextSpawn == 0) {
            spawnPos = new Vector2(0, -2);
        } else if (nextSpawn == 1) {
            spawnPos = new Vector2(0, 2);
            Invoke("Shoot", 0.5f);
        } else {
            spawnPos = new Vector2(0, 0);
        }

        rb2d.velocity = Vector2.zero;
        transform.position = spawnPos;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }

    void StopGame(){
        nextSpawn = -1;
        ResetBall();
    }

    void ResetGame(){
        nextSpawn = -1;
        ResetBall();
        Invoke("GoBall", 1);
    }
}
