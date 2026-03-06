using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CncController : MonoBehaviour
{

    private Animator anim;

    public float deceleration = 3f;

    private float delayTime = 1f;

    [SerializeField]
    private float delayVfx = 0.3f;

    [SerializeField]
    private GameObject nextTarget;

    [SerializeField]
    private GameObject lightVfx;

    private float failRate = 0;

    private bool isProcessing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }



    void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Cut");
        StartCoroutine(DelayVfx());
    }

    void OnTriggerStay(Collider product)
    {
        if (product.CompareTag("Product"))
        {
            ProductController productController = product.GetComponent<ProductController>();
            if (productController.currentState.Equals(ProductState.afterStamping))
            {
                productController.speed = Mathf.MoveTowards(productController.speed, 0, deceleration);
                if (!isProcessing)
                {
                    isProcessing = true;
                    StartCoroutine(DelayTransform(productController));
                }
            }
            else
            {
                productController.speed = 1f;
                productController.direction = (nextTarget.transform.position - product.transform.position).normalized;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        lightVfx.SetActive(false);
    }

    IEnumerator DelayTransform(ProductController productController)
    {
        yield return new WaitForSeconds(delayTime);
        if (Random.value < failRate)
        {
            productController.SetStage(ProductState.brokenProduct);
        }
        else
        {
            productController.SetStage(ProductState.finalProduct);
        }
        isProcessing = false;

    }

    IEnumerator DelayVfx()
    {
        yield return new WaitForSeconds(delayVfx);
        lightVfx.SetActive(true);

    }


    public void SetFailRate(float newFailRate)
    {

        failRate = newFailRate;
    }
}
