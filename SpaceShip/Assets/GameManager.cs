using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static int PlayerScore1 = 0; // Pontuação do player 1
    public static int PlayerScore2 = 0; // Pontuação do player 2

    public GUISkin layout;              // Fonte do placar

    public static int lifes = 3; // Vidas do jogador
    public static GameObject thePlayer; // Referência ao objeto jogador
    private float shipInterval;
    public static float shipSpeed = -2.0f;
    public GameObject ship1Prefab;
    public GameObject ship2Prefab;
    public GameObject ship3Prefab;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        RestartGame();
        DrawLifes();

        shipInterval = Random.Range(1f, 2f);
        source = GetComponent<AudioSource>();
    }

    public void LoseLife() {
        lifes--;
        DrawLifes();
        thePlayer.SendMessage("RestartPosition", null, SendMessageOptions.RequireReceiver);

        source.Play();

        if (lifes == 0) {
            GameOver();
            thePlayer.SendMessage("Die", null, SendMessageOptions.RequireReceiver);
        }

        // find all gameObjects with tag Invader3, Invader2, Invader1 and call ResetPosition() from each of them
        GameObject[] invader3 = GameObject.FindGameObjectsWithTag("Invader3");
        foreach (GameObject invader in invader3) {
            Destroy(invader);
        }

        GameObject[] invader2 = GameObject.FindGameObjectsWithTag("Invader2");
        foreach (GameObject invader in invader2) {
            Destroy(invader);
        }

        GameObject[] invader1 = GameObject.FindGameObjectsWithTag("Invader1");
        foreach (GameObject invader in invader1) {
            Destroy(invader);
        }

    }

    public static void DrawLifes() {
        if (lifes == 2) {
            GameObject heart0 = GameObject.Find("Heart0");
            heart0.GetComponent<SpriteRenderer>().enabled = false;
        } else if (lifes == 1) {
            GameObject heart1 = GameObject.Find("Heart1");
            heart1.GetComponent<SpriteRenderer>().enabled = false;
        } else if (lifes == 0) {
            GameObject heart2 = GameObject.Find("Heart2");
            heart2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // GameObject[] invaders3 = GameObject.FindGameObjectsWithTag("Invader3");
        // GameObject[] invaders2 = GameObject.FindGameObjectsWithTag("Invader2");
        // GameObject[] invaders1 = GameObject.FindGameObjectsWithTag("Invader1");

        // int len = invaders3.Length + invaders2.Length + invaders1.Length;

        // if(len == 25) {
        //     foreach (GameObject invader in invaders3) {
        //         invader.GetComponent<Invaders>().setSpeedLevel(1);
        //     }
            
        //     foreach (GameObject invader in invaders2) {
        //         invader.GetComponent<Invaders>().setSpeedLevel(1);
        //     }

        //     foreach (GameObject invader in invaders1) {
        //         invader.GetComponent<Invaders>().setSpeedLevel(1);
        //     }
        // } else if (len == 10) {
        //     foreach (GameObject invader in invaders3) {
        //         invader.GetComponent<Invaders>().setSpeedLevel(2);
        //     }
            
        //     foreach (GameObject invader in invaders2) {
        //         invader.GetComponent<Invaders>().setSpeedLevel(2);
        //     }

        //     foreach (GameObject invader in invaders1) {
        //         invader.GetComponent<Invaders>().setSpeedLevel(2);
        //     }
        // } else if (len == 0){
        //     SceneManager.LoadScene("YouWin");
        // }

        shipInterval -= Time.deltaTime;
        if (shipInterval <= 0) {
            shipInterval = Random.Range(1.0f, 2.0f);
            SpawnShip();
        }
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI () {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 13, 2, 200, 200), "" + PlayerScore1);
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame() {
        lifes = 3;
        PlayerScore1 = 0;
    }

    public void Add10PointPlayer1() {
        PlayerScore1 += 10;
    }

    public void Add20PointPlayer1() {
        PlayerScore1 += 20;
    }

    public void Add30PointPlayer1() {
        PlayerScore1 += 30;
    }

    public void Add50PointPlayer1() {
        PlayerScore1 += 50;
    }

    void SpawnShip() {
        int random = Random.Range(0, 3);
        GameObject ship;

        if (random == 0) {
            ship = Instantiate(ship1Prefab) as GameObject;
        } else if (random == 1) {
            ship = Instantiate(ship2Prefab) as GameObject;
        } else {
            ship = Instantiate(ship3Prefab) as GameObject;
        }

        Rigidbody2D rb2d = ship.GetComponent<Rigidbody2D>();
        if (rb2d == null) {
            rb2d = ship.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
        }

        // random Y between -3.3 and 3.3
        float randomY = Random.Range(-2.5f, 2.5f);
        ship.transform.position = new Vector2(8f, randomY);
        rb2d.velocity = new Vector2(shipSpeed, 0);

    }
    

}
