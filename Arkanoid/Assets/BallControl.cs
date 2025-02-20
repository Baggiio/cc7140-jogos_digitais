using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public KeyCode launchBall = KeyCode.Space;
    private Rigidbody2D rb2d; 
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(launchBall)) {             // Velocidade da Raquete para ir para cima
            float rand = Random.Range(0, 2);
            if(rand < 1){
                rb2d.AddForce(new Vector2(15, 15));
            } else {
                rb2d.AddForce(new Vector2(-15, 15));
            }
        }
    }
}
