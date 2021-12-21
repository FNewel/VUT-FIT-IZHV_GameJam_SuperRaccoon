using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public GameObject player;

    public float camBorder;

    private Vector2 player_prev_position;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player_prev_position = player.transform.position;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        Physics2D.IgnoreLayerCollision(9, 12);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > camBorder)
         {
             Vector2 delta_position;
             delta_position = (Vector2) player.transform.position - player_prev_position;
             transform.Translate(delta_position);
         }
         player_prev_position = player.transform.position;
    }
}
