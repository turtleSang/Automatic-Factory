using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("Product")]
    [SerializeField]
    private GameObject productPrefabs;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int poolSize = 20;
    public float delayTime;

    [Header("Belt Matrial")]
    [SerializeField]
    private Material belt;

    [SerializeField]
    private Material beltReverse;

    [SerializeField]
    private float conveyorSpeed;

    [Header("UI")]
    [SerializeField]
    private Slider sliderDelayTime;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button stopButton;


    //State
    private bool isSpawn = true;

    private Coroutine spawnCoroutine = null;

    private List<GameObject> poolProduct = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDelayTime(5);
        belt.SetFloat("_Speed", 0);
        beltReverse.SetFloat("_Speed", 0);
        sliderDelayTime.interactable = false;
        startButton.interactable = true;
        stopButton.interactable = false;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject product = Instantiate(productPrefabs);
            product.SetActive(false);
            poolProduct.Add(product);
        }
    }


    IEnumerator DelaySpawn()
    {
        while (isSpawn)
        {
            SpawnProduct();
            yield return new WaitForSeconds(delayTime);
        }
    }

    private void SpawnProduct()
    {
        GameObject product = getProduct();
        ProductController productController = product.GetComponent<ProductController>();
        //reset Product
        productController.SetStage(ProductState.rawMaterial);
        productController.direction = Vector3.zero;
        productController.speed = 0;
        product.transform.position = spawnPoint.transform.position;
        product.transform.rotation = spawnPoint.transform.rotation;
        product.SetActive(true);
    }

    public void StartSpawning()
    {

        if (isSpawn && spawnCoroutine != null) return;
        isSpawn = true;
        startButton.interactable = false;
        stopButton.interactable = true;
        spawnCoroutine = StartCoroutine(DelaySpawn());
        belt.SetFloat("_Speed", conveyorSpeed);
        beltReverse.SetFloat("_Speed", -conveyorSpeed);
        sliderDelayTime.interactable = true;
    }

    public void StopSpawning()
    {
        if (!isSpawn && spawnCoroutine == null) return;
        startButton.interactable = true;
        stopButton.interactable = false;
        isSpawn = false;
        sliderDelayTime.interactable = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            belt.SetFloat("_Speed", 0);
            beltReverse.SetFloat("_Speed", 0);
        }
    }

    public GameObject getProduct()
    {
        foreach (GameObject product in poolProduct)
        {
            if (!product.activeInHierarchy)
            {
                return product;
            }
        }
        Debug.Log(123);
        GameObject newProduct = Instantiate(productPrefabs);
        newProduct.SetActive(false);
        poolProduct.Add(newProduct);
        return newProduct;
    }

    public void SetDelayTime(float newDelay)
    {
        delayTime = newDelay;
    }
}
