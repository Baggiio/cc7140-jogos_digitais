using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin layout;              // Fonte do placar
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI() {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 300, 190, 700, 100), "É um clássico jogo onde você controla nave para rebater a bola e destruir todos os blocos na tela.");
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
