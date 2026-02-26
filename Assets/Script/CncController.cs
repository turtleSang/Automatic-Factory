using System.Collections;
using UnityEngine;

public class CncController : MonoBehaviour
{

    private Animator anim;

    public float deceleration = 3f;

    private float delayTime = 1f;

    [SerializeField]
    private GameObject nextTarget;

    [Range(0f, 1f)]
    public float failRate = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }



    void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Cut");
    }

    void OnTriggerStay(Collider product)
    {
        if (product.CompareTag("Product"))
        {
            ProductController productController = product.GetComponent<ProductController>();
            if (productController.currentState.Equals(ProductState.afterStamping))
            {
                productController.speed = Mathf.MoveTowards(productController.speed, 0, deceleration);
                StartCoroutine(DelayTransform(productController));
            }
            else
            {
                productController.speed = 1f;
                productController.direction = (nextTarget.transform.position - product.transform.position).normalized;
            }

        }
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
    }


}
