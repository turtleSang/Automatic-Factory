using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField]
    private float delayGrab = 1.1f;

    [SerializeField]
    private float delayRelease = 5.04f;

    [SerializeField]
    private float delayReset = 7f;


    [SerializeField]
    private ArmTriggerType armTrigger;

    [SerializeField]
    private GameObject grabPoint;

    [SerializeField]
    private ProductState typeProduct = ProductState.rawMaterial;

    private GameObject currentObj;
    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider product)
    {
        ProductController productController = product.GetComponent<ProductController>();
        if (product.CompareTag("Product") && productController.currentState.Equals(typeProduct) && currentObj == null)
        {
            currentObj = product.gameObject;
            switch (armTrigger)
            {
                case ArmTriggerType.TURNBACK:
                    anim.SetTrigger("TurnBack");
                    break;
                case ArmTriggerType.TURNLEFT:
                    anim.SetTrigger("TurnLeft");
                    break;
                default:
                    anim.SetTrigger("TurnRight");
                    break;
            }
            StartCoroutine(DelayGrab());
            StartCoroutine(DelayRelease());
            StartCoroutine(DelayReset());
        }

    }

    private void Grab()
    {
        Rigidbody rig = currentObj.GetComponent<Rigidbody>();
        rig.isKinematic = true;
        ProductController productController = currentObj.GetComponent<ProductController>();
        productController.speed = 0;
        productController.direction = Vector3.zero;

        currentObj.transform.SetParent(grabPoint.transform);
        currentObj.transform.localPosition = Vector3.zero;
        currentObj.transform.rotation = Quaternion.identity;

    }
    IEnumerator DelayGrab()
    {
        yield return new WaitForSeconds(delayGrab);
        Grab();
    }

    private void Release()
    {
        Rigidbody rig = currentObj.GetComponent<Rigidbody>();
        rig.isKinematic = false;
        currentObj.transform.SetParent(null);
    }

    IEnumerator DelayRelease()
    {
        yield return new WaitForSeconds(delayRelease);
        Release();
    }

    private void ResetArm()
    {
        currentObj = null;
    }

    private IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(delayReset);
        ResetArm();
    }

}
