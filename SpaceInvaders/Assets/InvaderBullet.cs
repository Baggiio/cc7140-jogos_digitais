using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBullet : MonoBehaviour
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
        if (coll.gameObject.tag == "Player"){
            Destroy(this.gameObject);
            gameManager.GetComponent<GameManager>().LoseLife();
        } else {
            Destroy(this.gameObject); 
        }

    }

}
