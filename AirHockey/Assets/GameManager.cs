using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int PlayerScore1 = 0; // Pontuação do player 1
    public static int PlayerScore2 = 0; // Pontuação do player 2

    public GUISkin layout;              // Fonte do placar
    GameObject theBall;              // Referência ao objeto bola
    GameObject structureEsquerda;
    GameObject structureDireita;
    

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball"); // Busca a referência da bola
        structureEsquerda = GameObject.FindGameObjectWithTag("StructureEsquerda");
        structureDireita = GameObject.FindGameObjectWithTag("StructureDireita");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // incrementa a potuação
    public static void Score (string goalID) {
        if (goalID == "GolBaixo")
        {
            PlayerScore1++;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<BallControl>().setNextSpawn(1);
        } else
        {
            PlayerScore2++;
            GameObject.FindGameObjectWithTag("Ball").GetComponent<BallControl>().setNextSpawn(0);
        }
    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI () {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 + 380 - 12, 10, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 380 - 12, Screen.height - 70, 100, 100), "" + PlayerScore2);

        if (GUI.Button(new Rect(Screen.width / 2 + 380, Screen.height / 2 - 22, 120, 53), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            theBall.SendMessage("StopGame", null, SendMessageOptions.RequireReceiver);
            structureDireita.SetActive(true);
            structureEsquerda.SetActive(true);
            structureDireita.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
            structureEsquerda.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
        if (PlayerScore1 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 + 380, 200, 2000, 1000), "PLAYER ONE WINS");
            theBall.SendMessage("StopGame", null, SendMessageOptions.RequireReceiver);
            structureDireita.SetActive(true);
            structureEsquerda.SetActive(true);
            structureDireita.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
            structureEsquerda.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        } else if (PlayerScore2 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 + 380, 200, 2000, 1000), "PLAYER TWO WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            structureDireita.SetActive(true);
            structureEsquerda.SetActive(true);
            structureDireita.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
            structureEsquerda.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
    }


}
