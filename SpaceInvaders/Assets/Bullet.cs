using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Display");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.tag == "Invader3"){
            coll.gameObject.GetComponent<Invaders>().ExplodeAndDestroy();
            Destroy(this.gameObject); 
            gameManager.GetComponent<GameManager>().Add10PointPlayer1();
        }

        if (coll.gameObject.tag == "Invader2"){
            coll.gameObject.GetComponent<Invaders>().ExplodeAndDestroy();
            Destroy(this.gameObject); 
            gameManager.GetComponent<GameManager>().Add20PointPlayer1();
        }

        if (coll.gameObject.tag == "Invader1"){
            coll.gameObject.GetComponent<Invaders>().ExplodeAndDestroy();
            Destroy(this.gameObject); 
            gameManager.GetComponent<GameManager>().Add30PointPlayer1();
        }

        if (coll.gameObject.tag == "Mothership"){
            coll.gameObject.GetComponent<Mothership>().ExplodeAndDestroy();
            Destroy(this.gameObject); 
            gameManager.GetComponent<GameManager>().Add100PointPlayer1();
        }

        if (coll.gameObject.tag == "Wall"){
            Destroy(this.gameObject); 
        }
    }

}
