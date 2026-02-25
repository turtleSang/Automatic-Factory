using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject productPrefabs;
    [SerializeField]
    private GameObject spawnPoint;

    public float delaytime = 5;
    private bool isSpawn = true;

    private Coroutine spawnCoroutine = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Instantiate(productPrefabs, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    public void StartSpawning()
    {
        if (isSpawn && spawnCoroutine != null) return;
        isSpawn = true;
        Debug.Log(123);
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
}
