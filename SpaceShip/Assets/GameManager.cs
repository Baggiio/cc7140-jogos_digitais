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
    private float mothershipInterval;
    public float mothershipSpeed = 5.0f;
    public GameObject mothershipPrefab;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        RestartGame();
        DrawLifes();

        mothershipInterval = Random.Range(10.0f, 20.0f);
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
            invader.SendMessage("ResetPosition", null, SendMessageOptions.RequireReceiver);
        }

        GameObject[] invader2 = GameObject.FindGameObjectsWithTag("Invader2");
        foreach (GameObject invader in invader2) {
            invader.SendMessage("ResetPosition", null, SendMessageOptions.RequireReceiver);
        }

        GameObject[] invader1 = GameObject.FindGameObjectsWithTag("Invader1");
        foreach (GameObject invader in invader1) {
            invader.SendMessage("ResetPosition", null, SendMessageOptions.RequireReceiver);
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
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] invaders3 = GameObject.FindGameObjectsWithTag("Invader3");
        GameObject[] invaders2 = GameObject.FindGameObjectsWithTag("Invader2");
        GameObject[] invaders1 = GameObject.FindGameObjectsWithTag("Invader1");

        int len = invaders3.Length + invaders2.Length + invaders1.Length;

        if(len == 25) {
            foreach (GameObject invader in invaders3) {
                invader.GetComponent<Invaders>().setSpeedLevel(1);
            }
            
            foreach (GameObject invader in invaders2) {
                invader.GetComponent<Invaders>().setSpeedLevel(1);
            }

            foreach (GameObject invader in invaders1) {
                invader.GetComponent<Invaders>().setSpeedLevel(1);
            }
        } else if (len == 10) {
            foreach (GameObject invader in invaders3) {
                invader.GetComponent<Invaders>().setSpeedLevel(2);
            }
            
            foreach (GameObject invader in invaders2) {
                invader.GetComponent<Invaders>().setSpeedLevel(2);
            }

            foreach (GameObject invader in invaders1) {
                invader.GetComponent<Invaders>().setSpeedLevel(2);
            }
        } else if (len == 0){
            SceneManager.LoadScene("YouWin");
        }

        mothershipInterval -= Time.deltaTime;
        if (mothershipInterval <= 0) {
            mothershipInterval = Random.Range(10.0f, 20.0f);
            SpawnMotherShip();
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

    void SpawnMotherShip() {
        int random = Random.Range(0, 100);
        GameObject motherShip = Instantiate(mothershipPrefab) as GameObject;

        Rigidbody2D rb2d = motherShip.GetComponent<Rigidbody2D>();
        if (rb2d == null) {
            rb2d = motherShip.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
        }

        // if random is less than 50, spawn from left, else spawn from right

        if (random < 50) {
            motherShip.transform.position = new Vector2(-7f, 3.5f);
            rb2d.velocity = new Vector2(mothershipSpeed, 0);
        } else {
            motherShip.transform.position = new Vector2(7f, 3.5f);
            rb2d.velocity = new Vector2(-mothershipSpeed, 0);
        }

        Destroy(motherShip, 5.0f);

    }
    

}
