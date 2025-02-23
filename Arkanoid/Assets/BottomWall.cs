using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        if (hitInfo.tag == "Ball")
        {
            GameManager.LoseLife();
            hitInfo.gameObject.SendMessage("RestartPosition", null, SendMessageOptions.RequireReceiver);
        }

    }
}
