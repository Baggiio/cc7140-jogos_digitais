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
    public static GameObject Blocks; // Referência aos blocos
    public int blocksToBeat = 0;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        RestartGame();
        DrawLifes();
    }

    public static void LoseLife() {
        lifes--;
        DrawLifes();
        thePlayer.SendMessage("RestartPosition", null, SendMessageOptions.RequireReceiver);

        if (lifes == 0) {
            FindAnyObjectByType<GameManager>().GameOver();
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
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Block");
        Debug.Log(gos.Length);
        Debug.Log(scene.name);
        if(gos.Length == blocksToBeat){
            if (scene.name == "Scene1"){
                SceneManager.LoadScene("Scene2");
            } else if (scene.name == "Scene2"){
                SceneManager.LoadScene("YouWin");
            } 
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

    public void AddPointPlayer1() {
        PlayerScore1++;
    }
    

}
