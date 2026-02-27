using UnityEngine;

public class GarbageManager : MonoBehaviour
{

    void OnCollisionEnter(Collision product)
    {
        if (product.gameObject.CompareTag("Product"))
        {
            ProductController productController = product.gameObject.GetComponent<ProductController>();
            productController.direction = Vector3.zero;
            productController.speed = 0;
            product.gameObject.SetActive(false);

        }
    }
}
