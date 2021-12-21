using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMovement : MonoBehaviour
{
    [SerializeField] private Transform[] route;

    private GameManager timeLeftGM;
    private float timeLeft;
    private float time;
    private int routeToGo;

    private float tParam;

    private Vector2 moonPosition;

    private bool coroutineAllowed;

    void Start() {
        routeToGo = 0;
        tParam = 0;
        coroutineAllowed = true;
        time = GameObject.Find("GameManager").GetComponent<GameManager>().time;
        timeLeftGM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {

        timeLeft = timeLeftGM.timeLeft;

        if (coroutineAllowed) {
            if (routeToGo == 0) {
                StartCoroutine(GoByTheRoute(routeToGo));
            }
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber) {
        coroutineAllowed = false;

        Transform routeChild0 = route[routeNumber].GetChild(0);
        Transform routeChild1 = route[routeNumber].GetChild(1);
        Transform routeChild2 = route[routeNumber].GetChild(2);
        Transform routeChild3 = route[routeNumber].GetChild(3);

        while (tParam < 1) {

            Vector2 r0 = routeChild0.position;
            Vector2 r1 = routeChild1.position;
            Vector2 r2 = routeChild2.position;
            Vector2 r3 = routeChild3.position;

            tParam = 1 - timeLeft * (1/time);

            moonPosition =  Mathf.Pow(1 - tParam, 3) * r0 + 3 * 
                            Mathf.Pow(1 - tParam, 2) * tParam * r1 + 3 * (1 - tParam) * 
                            Mathf.Pow(tParam, 2) * r2 + Mathf.Pow(tParam, 3) * r3;

            transform.position = moonPosition;
            yield return null;
        }

        routeToGo += 1;
        tParam = 0;
        coroutineAllowed = true;
    }
}
