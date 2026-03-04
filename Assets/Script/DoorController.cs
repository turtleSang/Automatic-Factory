using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float delayCloseDoor = 5f;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void OpenDoor()
    {
        StartCoroutine(DoorCoordinate());
    }

    IEnumerator DoorCoordinate()
    {
        anim.SetBool("IsOpen", true);
        yield return new WaitForSeconds(delayCloseDoor);
        anim.SetBool("IsOpen", false);

    }
}
