using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float boundY_cima = 0;
    public float boundY_baixo = -4.3f;
    public float boundX = 2.4f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var pos = transform.position;

        if (mousePos.y > boundY_cima) {                  
            pos.y = boundY_cima;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        } else if (mousePos.y < boundY_baixo) {
            pos.y = boundY_baixo;               // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        } else {
            pos.y = mousePos.y;
        }

        if (mousePos.x > boundX) {                  
            pos.x = boundX;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (mousePos.x < -boundX) {
            pos.x = -boundX;               // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        } else {
            pos.x = mousePos.x;
        }

        Vector2 targetPosition = new Vector2(pos.x, pos.y);
        rb2d.MovePosition(targetPosition);
    }
}
