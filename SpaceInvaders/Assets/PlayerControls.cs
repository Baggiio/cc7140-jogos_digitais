using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode shoot = KeyCode.Space;
    public float speed = 20.0f;
    public float boundX = 4.0f;
    private Rigidbody2D rb2d;
    private bool isDead = false;
 
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = rb2d.velocity;                // Acessa a velocidade da raquete
        if (isDead) {
            vel = Vector2.zero;                 // Define a velocidade da raquete como zero
            return;
        }
        if (Input.GetKey(moveLeft)) {             // Velocidade da Raquete para ir para cima
            vel.x = -speed;
        }
        else if (Input.GetKey(moveRight)) {      // Velocidade da Raquete para ir para cima
            vel.x = speed;                    
        }
        else {
            vel.x = 0;                          // Velociade para manter a raquete parada
        }
        rb2d.velocity = vel;                    // Atualizada a velocidade da raquete

        var pos = transform.position;           // Acessa a Posição da raquete
        if (pos.x > boundX) {                  
            pos.x = boundX;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.x < -boundX) {
            pos.x = -boundX;                    // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        transform.position = pos;               // Atualiza a posição da raquete

    }

    void RestartPosition() {
        transform.position = new Vector2(0, -4.5f); // Reposiciona a raquete
    }

    void Die() {
        isDead = true; // Define que o jogador morreu
    }
}
