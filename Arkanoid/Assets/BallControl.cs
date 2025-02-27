using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public KeyCode launchBall = KeyCode.Space;
    private Rigidbody2D rb2d; 
    private bool isLaunched = false;
    public float ballSpeed = 10.0f;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(launchBall) && !isLaunched) {             // Velocidade da Raquete para ir para cima
            isLaunched = true;
            float rand = Random.Range(0, 2);
            if(rand < 1){
                rb2d.AddForce(new Vector2(15, 15));
            } else {
                rb2d.AddForce(new Vector2(-15, 15));
            }
        }

        if (isLaunched) {
            // normaliza a velocidade
            Vector2 vel = rb2d.velocity;
            vel.Normalize();
            vel *= ballSpeed;
            rb2d.velocity = vel;
        } else {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.3f);
        }
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")) {
            Vector2 vel;
            vel.x = rb2d.velocity.x / 2 + coll.collider.attachedRigidbody.velocity.x / 3;
            vel.y = rb2d.velocity.y;

            rb2d.velocity = vel;
        } else if (coll.collider.CompareTag("Block")) {
            coll.gameObject.GetComponent<Block>().TakeDamage(100);
        }

        source.Play();
    }

    void RestartPosition() {
        isLaunched = false;
        rb2d.velocity = Vector2.zero;
    }
}
