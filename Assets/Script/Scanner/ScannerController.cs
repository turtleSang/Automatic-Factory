using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ScannerController : MonoBehaviour
{
    //UI Element
    [SerializeField]
    private GameObject waitingUI;
    [SerializeField]
    private GameObject scanningUI;
    [SerializeField]
    private GameObject checkedUI;
    [SerializeField]
    private GameObject failUI;

    //Light Element
    [SerializeField]
    private GameObject scanLight;

    //delay Time
    [SerializeField]
    private float delayScan = 1.5f;
    private float delayReset = 1f;

    private ScreenType currentScreenType;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetScreen(ScreenType.WAIT);
        scanLight.SetActive(false);
    }

    void OnTriggerEnter(Collider product)
    {
        if (product.CompareTag("Product"))
        {
            SetScreen(ScreenType.SCAN);
            scanLight.SetActive(true);
            ProductController productController = product.GetComponent<ProductController>();
            StartCoroutine(DelayScan(productController));
            product.gameObject.SetActive(false);
            productController.direction = Vector3.zero;
            productController.speed = 0;

        }
    }

    IEnumerator DelayScan(ProductController productController)
    {
        yield return new WaitForSeconds(delayScan);
        if (productController.currentState.Equals(ProductState.finalProduct))
        {
            SetScreen(ScreenType.CHECKED);
        }
        else
        {
            SetScreen(ScreenType.FAIL);
        }
        scanLight.SetActive(false);
        StartCoroutine(DelayReset());
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(delayReset);
        SetScreen(ScreenType.WAIT);
    }

    void SetScreen(ScreenType type)
    {
        currentScreenType = type;
        waitingUI.SetActive(false);
        scanningUI.SetActive(false);
        failUI.SetActive(false);
        checkedUI.SetActive(false);
        switch (currentScreenType)
        {
            case ScreenType.WAIT:
                waitingUI.SetActive(true);

                break;
            case ScreenType.SCAN:
                scanningUI.SetActive(true);
                break;
            case ScreenType.CHECKED:
                checkedUI.SetActive(true);
                break;
            default:
                failUI.SetActive(true);
                break;
        }
    }

}
