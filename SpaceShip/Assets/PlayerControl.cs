using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;      // Move a raquete para cima
    public KeyCode moveRight = KeyCode.D;    // Move a raquete para baixo
    public KeyCode moveUp = KeyCode.W;        // Move a raquete para a esquerda
    public KeyCode moveDown = KeyCode.S;      // Move a raquete para a direita
    public float speed = 10.0f;             // Define a velocidade da bola
    public float boundX_r = 4.0f;            // Define os limites em Y
    public float boundX_l = 4.0f;            // Define os limites em Y
    public float boundY = 2.0f;            // Define os limites em X
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete
    private bool isDead = false;            // Define se o jogador morreu
    
    // Bullet related variables
    public GameObject bulletPrefab;         // Prefab for the bullet
    public float bulletSpeed = 10.0f;       // Speed of the bullet
    public float shootCooldown = 0.3f;      // Time between shots
    private float lastShootTime = 0;        // Time of last shot

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa a raquete
    }

    // Update is called once per frame
    void Update()
    {
        // Check for shoot input
        if (Input.GetKeyDown(KeyCode.Space) && !isDead)
        {
            Shoot();
        }

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

        if (Input.GetKey(moveUp)) {       // Velocidade da Raquete para ir para a esquerda
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown)) {     // Velocidade da Raquete para ir para a direita
            vel.y = -speed;
        }
        else {
            vel.y = 0;
        }

        rb2d.velocity = vel;                    // Atualizada a velocidade da raquete

        var pos = transform.position;           // Acessa a Posição da raquete
        if (pos.x > boundX_r) {                  
            pos.x = boundX_r;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.x < boundX_l) {
            pos.x = boundX_l;                    // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }

        if (pos.y > boundY) {
            pos.y = boundY;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.y < -boundY) {
            pos.y = -boundY;                    // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        transform.position = pos;               // Atualiza a posição da raquete
    }

    void RestartPosition() {
        transform.position = new Vector2(0, -4.0f); // Reposiciona a raquete
    }

    void Die() {
        isDead = true; // Define que o jogador morreu
    }

    void Shoot()
    {
        // Check if enough time has passed since last shot
        if (Time.time - lastShootTime < shootCooldown)
            return;
            
        // Update last shoot time
        lastShootTime = Time.time;
        
        // Create bullet at player position with slight Y offset
        Vector2 bulletPosition = new Vector2(transform.position.x + 0.5f, transform.position.y);
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        
        // If the bullet doesn't have a Rigidbody2D, add one
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb == null)
        {
            bulletRb = bullet.AddComponent<Rigidbody2D>();
            bulletRb.gravityScale = 0; // No gravity for the bullet
        }
        
        // If the bullet doesn't have a BoxCollider2D, add one
        if (bullet.GetComponent<BoxCollider2D>() == null)
        {
            bullet.AddComponent<BoxCollider2D>();
        }
        
        // Set bullet velocity (upward)
        bulletRb.velocity = new Vector2(bulletSpeed, 0);
        
        // Destroy bullet after 3 seconds to prevent memory issues
        Destroy(bullet, 3f);
    }
}
