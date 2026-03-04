using System.Collections;
using UnityEngine;

public class PresserController : MonoBehaviour
{
    private Animator anim;
    public float deceleration = 5f;

    [SerializeField]
    private GameObject targetAfterStamp;

    [SerializeField]
    private ParticleSystem smokeParticle;

    private float speed = 1f;

    private bool isProcessing = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        smokeParticle.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            anim.SetTrigger("Press");
            smokeParticle.Play();
        }
    }

    private void OnTriggerStay(Collider product)
    {
        if (product.CompareTag("Product"))
        {

            ProductController productController = product.gameObject.GetComponent<ProductController>();
            if (productController.currentState.Equals(ProductState.rawMaterial))
            {
                productController.speed = Mathf.MoveTowards(productController.speed, 0f, deceleration * Time.deltaTime);
                if (!isProcessing)
                {
                    isProcessing = true;
                    StartCoroutine(DelayTransform(productController));
                }
            }
            else
            {
                productController.direction = (targetAfterStamp.transform.position - product.transform.position).normalized;
                productController.speed = speed;
            }

        }
    }

    private IEnumerator DelayTransform(ProductController productController)
    {
        yield return new WaitForSeconds(1f);
        productController.SetStage(ProductState.afterStamping);
        isProcessing = false;
    }

}
