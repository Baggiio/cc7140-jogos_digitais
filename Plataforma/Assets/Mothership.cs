using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Sprite spriteImage;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    void Update()
    {
        
    }

    public void ExplodeAndDestroy() {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteImage;
        Destroy(gameObject, 0.5f);
    }
}
