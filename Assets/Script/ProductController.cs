using System;
using UnityEngine;

public class ProductController : MonoBehaviour
{

    public float speed = 0f;

    float rotationSpeed = 1f;

    public Vector3 direction = Vector3.zero;

    public ProductState currentState;


    private Rigidbody rb;

    [SerializeField]
    private GameObject rawMaterial;


    [SerializeField]
    private GameObject afterStamping;


    [SerializeField]
    private GameObject brokenProduct;

    [SerializeField]
    private GameObject finalProduct;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        SetStage(ProductState.rawMaterial);
    }

    // Update is called once per frame
    void Update()
    {



    }

    void FixedUpdate()
    {
        if (speed > 0 && direction != Vector3.zero)
        {
            rb.linearVelocity = direction * speed;
            Quaternion targetRotate = Quaternion.LookRotation(direction);
            Quaternion quaternion = Quaternion.Slerp(transform.rotation, targetRotate, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(quaternion);
        }
    }
    public void SetStage(ProductState newState)
    {
        currentState = newState;
        rawMaterial.SetActive(false);
        afterStamping.SetActive(false);
        brokenProduct.SetActive(false);
        finalProduct.SetActive(false);
        switch (currentState)
        {
            case ProductState.rawMaterial:
                rawMaterial.SetActive(true);
                break;
            case ProductState.afterStamping:
                afterStamping.SetActive(true);
                break;
            case ProductState.brokenProduct:
                brokenProduct.SetActive(true);
                break;
            default:
                finalProduct.SetActive(true);
                break;

        }
    }


}
