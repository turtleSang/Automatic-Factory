using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject productPrefabs;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int poolSize = 20;

    public float delaytime = 5;
    private bool isSpawn = true;

    private Coroutine spawnCoroutine = null;

    private List<GameObject> poolProduct = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject product = Instantiate(productPrefabs);
            product.SetActive(false);
            poolProduct.Add(product);
        }
        StartSpawning();
    }


    IEnumerator DelaySpawn()
    {
        while (isSpawn)
        {
            SpawnProduct();
            yield return new WaitForSeconds(delaytime);
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
        spawnCoroutine = StartCoroutine(DelaySpawn());
    }

    public void StopSpawning()
    {
        if (!isSpawn && spawnCoroutine == null) return;
        isSpawn = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
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
        GameObject newProduct = Instantiate(productPrefabs);
        newProduct.SetActive(false);
        return newProduct;
    }
}
