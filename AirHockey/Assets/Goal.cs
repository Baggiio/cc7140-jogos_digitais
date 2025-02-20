using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameObject structureEsquerda;
    GameObject structureDireita;
    // Start is called before the first frame update
    void Start()
    {
        structureEsquerda = GameObject.FindGameObjectWithTag("StructureEsquerda");
        structureDireita = GameObject.FindGameObjectWithTag("StructureDireita");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        if (hitInfo.tag == "Ball")
        {
            string goalName = transform.name;

            GameManager.Score(goalName);
            hitInfo.gameObject.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
            structureDireita.SetActive(true);
            structureEsquerda.SetActive(true);
            structureDireita.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
            structureEsquerda.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
    }

}
