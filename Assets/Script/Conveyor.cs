using UnityEditor.Callbacks;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public GameObject targetPoint;

    [SerializeField]
    private float speed = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            ProductController productController = other.gameObject.GetComponent<ProductController>();
            Vector3 dir = (targetPoint.transform.position - other.transform.position).normalized;
            productController.direction = dir;
            productController.speed = speed;

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Product"))
        {

            ProductController productController = collision.gameObject.GetComponent<ProductController>();
            productController.direction = Vector3.zero;
            productController.speed = 0;
            Vector3 dir = (targetPoint.transform.position - collision.transform.position).normalized;
            productController.direction = dir;
            productController.speed = speed;

        }
    }
}
