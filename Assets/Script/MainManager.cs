using System;
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
    }

    public void BackToMainMenu()
    {
        cameraController.ChangeState(CameraState.MAIN);
        uiManager.ChangeState(UIState.MAINMENU);
    }

}
