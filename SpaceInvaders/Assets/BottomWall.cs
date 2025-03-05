using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWall : MonoBehaviour
{

    public GameObject gameManager;
    private bool verifyCollision = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Display");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {

        if (verifyCollision == false) {
            return;
        }

        if (hitInfo.tag == "Invader3" || hitInfo.tag == "Invader2" || hitInfo.tag == "Invader1")
        {
            gameManager.GetComponent<GameManager>().LoseLife();
            // set verifyCollision to false to avoid multiple collisions
            verifyCollision = false;
            
            // wait 1 second before setting verifyCollision to true
            StartCoroutine(WaitForCollision());
        }

    }

    IEnumerator WaitForCollision() {
        yield return new WaitForSeconds(1);
        verifyCollision = true;
    }
}
