using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_CameraFollower : MonoBehaviour
{
    public Camera cam;

    public GameObject player;

    public float camera_y_offset;

    void Update() {
        // Camera follows the player
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+camera_y_offset, cam.transform.position.z);
    }
}
