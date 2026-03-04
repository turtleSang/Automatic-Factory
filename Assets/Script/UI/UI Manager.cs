using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject factoryUI;
    [SerializeField]
    private GameObject spawnerUI;
    [SerializeField]
    private GameObject cncUI;
    [SerializeField]
    private GameObject defaultUI;
    private UIState currentUIState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void ChangeState(UIState uIState)
    {
        mainUI.gameObject.SetActive(false);
        factoryUI.gameObject.SetActive(false);
        spawnerUI.gameObject.SetActive(false);
        cncUI.gameObject.SetActive(false);
        defaultUI.gameObject.SetActive(false);
        currentUIState = uIState;
        switch (currentUIState)
        {
            case UIState.MAINMENU:
                mainUI.gameObject.SetActive(true);
                break;
            case UIState.FACTORYMENU:
                factoryUI.gameObject.SetActive(true);
                break;
            case UIState.SPAWNERMENU:
                spawnerUI.gameObject.SetActive(true);
                break;
            case UIState.CNCMACHINEMENU:
                cncUI.gameObject.SetActive(true);
                break;
            default:
                defaultUI.gameObject.SetActive(true);
                break;
        }
    }
}
