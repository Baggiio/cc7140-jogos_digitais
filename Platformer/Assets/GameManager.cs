using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0; // Pontuação do player 1
    public GUISkin layout;              // Fonte do placar
    public static int lifes = 3; // Vidas do jogador
    public static GameObject thePlayer; // Referência ao objeto jogador
    private AudioSource source;
    private int lastScore = 0;
    public GameObject followCamera;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        RestartGame();
        DrawLifes();
        //source = GetComponent<AudioSource>();
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
        if (PlayerScore1 >= 1000){
            SceneManager.LoadScene("YouWin");
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
}
