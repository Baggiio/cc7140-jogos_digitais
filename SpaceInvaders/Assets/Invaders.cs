using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 2.0f;
    private int state = 0;
    private float x;
    private float speed = 1.0f;
    public Sprite spriteImage;
    public float startOffset = 0.0f;
    private bool hasStartedMoving = false;
    
    // Variables for vertical movement
    private float verticalMoveTimer = 0.0f;
    private float verticalMoveInterval = 10.0f; // Move down every 10 seconds
    private float verticalMoveDistance = 0.815f; // Distance to move down
    // Bullet related variables
    public GameObject bulletPrefab;         // Prefab for the bullet
    public float bulletSpeed = 10.0f;       // Speed of the bullet
    public float shootCooldown = 0.3f;      // Time between shots
    private float shootTimer = 0.0f;
    private float shootInterval = 0.5f;
    private float startPositionY;
    private int level = 0;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {

        startPositionY = transform.position.y;

        source = GetComponent<AudioSource>();

        rb2d = GetComponent<Rigidbody2D>();  
        x = transform.position.x;

        // Initialize with zero velocity
        rb2d.velocity = Vector2.zero;
        
        // Start the delayed movement coroutine
        StartCoroutine(StartMovementAfterDelay());
    }

    // Coroutine to wait for startOffset before beginning movement
    IEnumerator StartMovementAfterDelay()
    {
        // Wait for the specified offset time
        yield return new WaitForSeconds(startOffset);
        
        // Start moving
        var vel = rb2d.velocity;
        vel.x = speed;
        rb2d.velocity = vel;
        hasStartedMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Only start the movement timers after the initial offset delay
        if (!hasStartedMoving)
            return;
            
        // Horizontal movement timer
        timer += Time.deltaTime;
        if (timer >= waitTime){
            ChangeState();
            timer = 0.0f;
        }

        // Vertical movement timer
        verticalMoveTimer += Time.deltaTime;
        if (verticalMoveTimer >= verticalMoveInterval){
            MoveDown();
            verticalMoveTimer = 0.0f;
        }

        // Shoot timer
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval){
            Shoot();
            shootTimer = 0.0f;
        }
        
    }

    void ChangeState(){
        increaseSpeed(level);
        var vel = rb2d.velocity;
        if (vel.x > 0)
            vel.x = speed;
        else
            vel.x = -speed;
        vel.x *= -1;
        rb2d.velocity = vel;
    }
    
    void MoveDown(){
        // Move the invader down by the specified distance
        Vector2 position = transform.position;
        position.y -= verticalMoveDistance;
        transform.position = position;
    }

    public void ExplodeAndDestroy() {
        source.Play();
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null) {
            Destroy(animator);
        }

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteImage;
        Destroy(gameObject, 0.5f);
    }

    void Shoot() {

        int random = Random.Range(0, 100);
        if (random > 5) // 5% chance of shooting
            return;

        // if there is already a bullet in the scene (InvaderBullet tag), don't shoot
        if (GameObject.FindWithTag("InvaderBullet") != null)
            return;

        // get gameObject name
        string name = gameObject.name;

        // gameObject name should be Linei-j
        // where i is the line number and j is the invader number

        // get the line number
        int line = int.Parse(name.Substring(4, 1));
        int column = int.Parse(name.Substring(6, 1));

        // only the last invader invader in the column shoots, so verify if gameObject Linei+1-j exists
        GameObject nextInvader = GameObject.Find("Line" + (line + 1) + "-" + column);
        if (nextInvader == null) {
            // Create bullet at invader position with slight Y offset
            Vector2 bulletPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
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
            
            // Set bullet velocity (downward)
            bulletRb.velocity = new Vector2(0, -bulletSpeed);
            
            // Destroy bullet after 3 seconds to prevent memory issues
            Destroy(bullet, 3f);
        }
    }

    void ResetPosition() {
        Vector2 position = transform.position;
        position.y = startPositionY;
        transform.position = position;

        // reset timers
        verticalMoveTimer = 0.0f;
        shootTimer = 0.0f;
    }

    void scheduleSpeedIncrease (int level) {

    }

    public void setSpeedLevel(int level) {
        this.level = level;
    }

    void increaseSpeed(int level) {

        if (level == 1) {
            speed = 1.5f;
            waitTime = 1.5f;
            verticalMoveInterval = 7.5f;
        } else if (level == 2) {
            speed = 2.0f;
            waitTime = 1f;
            verticalMoveInterval = 5.0f;
        } else {
            speed = 1.0f;
            waitTime = 2.0f;
            verticalMoveInterval = 10.0f;
        }
    }
}   
