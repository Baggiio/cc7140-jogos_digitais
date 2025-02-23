using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int PlayerScore1 = 0; // Pontuação do player 1
    public static int PlayerScore2 = 0; // Pontuação do player 2

    public GUISkin layout;              // Fonte do placar

    public static int lifes = 3; // Vidas do jogador
    public static GameObject thePlayer; // Referência ao objeto jogador

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
    }

    public static void LoseLife() {
        lifes--;
        DrawLifes();
        thePlayer.SendMessage("RestartPosition", null, SendMessageOptions.RequireReceiver);
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
        
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI () {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 + 380 - 12, 10, 100, 100), "" + PlayerScore1);
    }


}
