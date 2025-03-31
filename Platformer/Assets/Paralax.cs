using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    public GameObject followCamera;
    public Vector2 camera_start_position;
    public Vector2 background_start_position;

    // Start is called before the first frame update
    void Start()
    {
        followCamera = GameObject.Find("Follow Camera");
        camera_start_position = followCamera.transform.position;
        background_start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // increase background position X to half of camera position X delta

        Vector2 camera_position = followCamera.transform.position;
        Vector2 background_position = transform.position;
        Vector2 camera_delta = camera_position - camera_start_position;
        Vector2 background_delta = new Vector2(camera_delta.x / 2, camera_delta.y / 2);
        transform.position = new Vector2(background_start_position.x + background_delta.x, background_start_position.y);
    }
}
