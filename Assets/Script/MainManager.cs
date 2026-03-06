using System;
using UnityEditor;
using UnityEngine;

public class MainManager : MonoBehaviour
{


    [SerializeField]
    private SpawnManager spawnManager;

    //Camera
    [SerializeField]
    private CameraController cameraController;

    //UI 
    [SerializeField]
    private UIManager uiManager;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager.ChangeState(UIState.MAINMENU);
        cameraController.ChangeState(CameraState.MAIN);
        //Delete test
    }

    public void EnterFactory()
    {
        cameraController.ChangeState(CameraState.FACTORY);
        uiManager.ChangeState(UIState.FACTORYMENU);
    }

    public void FocusSpawner()
    {
        cameraController.ChangeState(CameraState.SPAWNERS);
        uiManager.ChangeState(UIState.SPAWNERMENU);
    }

    public void FocusPresser()
    {
        cameraController.ChangeState(CameraState.PRESSMACHINE);
        uiManager.ChangeState(UIState.DEFAULT);
    }

    public void FocusCNC()
    {
        cameraController.ChangeState(CameraState.CNCMACHINE);
        uiManager.ChangeState(UIState.CNCMACHINEMENU);

    }

    public void FocusArm01()
    {
        cameraController.ChangeState(CameraState.HAND01);
        uiManager.ChangeState(UIState.DEFAULT);
    }

    public void FocusArm02()
    {
        cameraController.ChangeState(CameraState.HAND02);
        uiManager.ChangeState(UIState.DEFAULT);

    }

    public void FocusScanner01()
    {
        cameraController.ChangeState(CameraState.SCANNER01);
        uiManager.ChangeState(UIState.DEFAULT);

    }

    public void FocusScanner02()
    {
        cameraController.ChangeState(CameraState.SCANNER02);
        uiManager.ChangeState(UIState.DEFAULT);

    }

    public void BackToMainMenu()
    {
        cameraController.ChangeState(CameraState.MAIN);
        uiManager.ChangeState(UIState.MAINMENU);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
