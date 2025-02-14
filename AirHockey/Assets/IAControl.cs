using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAControl : MonoBehaviour
{
    public GameObject ball;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {   
        rb2d = GetComponent<Rigidbody2D>();
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        var ball_pos = ball.transform.position;
        Vector3 new_pos = new Vector3(ball_pos.x, transform.position.y, transform.position.z);
        transform.position = new_pos;
    }
}
