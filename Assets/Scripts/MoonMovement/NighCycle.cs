using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class NighCycle : MonoBehaviour
{
    public GameObject GlobalVolume;
    public float timeSpeed = 5f;
    private Volume globalVolume;
    private float time;
    private GameManager timeLeftGM;
    private float timeLeft;


    void Start() {
        globalVolume = GlobalVolume.GetComponent<Volume>();
        time = GameObject.Find("GameManager").GetComponent<GameManager>().time;
        timeLeftGM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        timeLeft = timeLeftGM.timeLeft;

        if (timeLeft > 0) {
            globalVolume.weight = 1 - Mathf.Pow(Mathf.Lerp(1, 0, timeLeft/time), timeSpeed);
        }
    }
}
