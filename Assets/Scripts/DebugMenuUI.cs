using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenuUI : MonoBehaviour
{

    private Vector2 playerPosition;
    private GameObject player;

    #region Editor

    private bool displayUI;
    public float maxTime;

    #endregion

    #region Internal

    private static Vector2 WINDOW_DIMENSION = new Vector2(256.0f, 135.0f);
    private static float BASE_PADDING = 8.0f;
    private Rect mScreenRect;
    private Rect mMainWindowRect;

    #endregion

    #region Interface

    #endregion

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        var mainCamera = FindObjectOfType<Camera>();
        mScreenRect = new Rect(
            mainCamera.rect.x * Screen.width,
            mainCamera.rect.y * Screen.height,
            mainCamera.rect.width * Screen.width,
            mainCamera.rect.height * Screen.height
        );

        mMainWindowRect = new Rect(
            mScreenRect.x + mScreenRect.width - WINDOW_DIMENSION.x, mScreenRect.y,
            WINDOW_DIMENSION.x, WINDOW_DIMENSION.y
        );
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            displayUI = true;
        }
        if (Input.GetKey(KeyCode.C))
        {
            displayUI = false;
        }
    }
    private void OnGUI()
    {
        if (displayUI)
        {
            mMainWindowRect = GUI.Window(0, mMainWindowRect, MainWindowUI, "DEBUG MENU");

            mMainWindowRect.x = Mathf.Clamp(
                mMainWindowRect.x, mScreenRect.x,
                mScreenRect.x + mScreenRect.width - WINDOW_DIMENSION.x
            );
            mMainWindowRect.y = Mathf.Clamp(
                mMainWindowRect.y, mScreenRect.y,
                mScreenRect.y + mScreenRect.height - WINDOW_DIMENSION.y
            );
        }
    }

    private void savePosition(){
        playerPosition = new Vector2(
            player.transform.position.x,
            player.transform.position.y
        );
    }

    private void loadPosition(){
        player.transform.position = new Vector3(
            playerPosition.x,
            playerPosition.y,
            0.0f
        );
    }
    private void MainWindowUI(int windowId)
    {

        GUILayout.BeginArea(new Rect(
            BASE_PADDING, 2.0f * BASE_PADDING,
            WINDOW_DIMENSION.x - 2.0f * BASE_PADDING,
            WINDOW_DIMENSION.y - 3.0f * BASE_PADDING
        ));
        { 

            GUILayout.BeginVertical();
            {
                var gm = FindObjectOfType<GameManager>();
                
                var freeze = gm.freezeTime;
                freeze = (bool)GUILayout.Toggle(freeze, "Freeze Time");
                if (GUI.changed)
                { gm.freezeTime = freeze; }
                
                GUILayout.Label("Time Left: ", GUILayout.Width(WINDOW_DIMENSION.x / 4.0f));
                var time = gm.timeLeft;
                time = (int)GUILayout.HorizontalSlider(time, 0.0f, maxTime, GUILayout.ExpandWidth(true));
                if (GUI.changed)
                {
                    gm.timeLeft = time;
                }

                if (GUILayout.Button("Save position")){
                    savePosition();
                }
                if (GUILayout.Button("Load position")){
                    loadPosition();
                }

            }
            GUILayout.EndVertical();


        }
        GUILayout.EndArea();

        GUI.DragWindow(new Rect(
            2.0f * BASE_PADDING, 0.0f,
            WINDOW_DIMENSION.x - 4.0f * BASE_PADDING,
            WINDOW_DIMENSION.y
        ));
    }
}
