
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Properties")]
    public Vector3 targetPosition = Vector3.zero;

    public Vector3 targetRotation = Vector3.zero;

    public float speed = 0.01f;

    public float rotateSpeed = 10f;

    public bool isMoving = false;

    public bool isRotate = false;

    [Header("Target")]
    [SerializeField]
    private Transform targetHome;
    [SerializeField]
    private Transform focusHome;

    [SerializeField]
    private Transform targetFactory;

    [SerializeField]
    private Transform focusFactory;

    [SerializeField]
    private Transform targetSpawner;

    [SerializeField]
    private Transform focusSpawner;

    [SerializeField]
    private Transform targetPresser;

    [SerializeField]
    private Transform focusPresser;

    [SerializeField]
    private Transform targetCNC;

    [SerializeField]
    private Transform focusCNC;

    [SerializeField]
    private Transform targetHand01;
    [SerializeField]
    private Transform focusHand01;
    [SerializeField]
    private Transform targetHand02;
    [SerializeField]
    private Transform focusHand02;
    [SerializeField]
    private Transform targetScanner01;
    [SerializeField]
    private Transform focusScanner01;
    [SerializeField]
    private Transform targetScanner02;
    [SerializeField]
    private Transform focusScanner02;



    [Header("CameraState")]
    [SerializeField]
    private CameraState cameraCurrentState;


    // Update is called once per frame
    private void LateUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, targetPosition) < 0.05)
            {
                isMoving = false;
            }
        }

        if (isRotate)
        {
            Vector3 lookDirection = targetRotation - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            float angle = Quaternion.Angle(transform.rotation, lookRotation);
            if (angle < 0.5f)
            {
                transform.rotation = lookRotation;
                isRotate = false;
            }
        }

    }

    public void ChangeState(CameraState cameraState)
    {
        cameraCurrentState = cameraState;
        switch (cameraCurrentState)
        {
            case CameraState.MAIN:
                targetPosition = targetHome.position;
                targetRotation = focusHome.position;
                break;
            case CameraState.FACTORY:
                targetPosition = targetFactory.position;
                targetRotation = focusFactory.position;
                break;
            case CameraState.SPAWNERS:
                targetPosition = targetSpawner.position;
                targetRotation = focusSpawner.position;
                break;
            case CameraState.PRESSMACHINE:
                targetPosition = targetPresser.position;
                targetRotation = focusPresser.position;
                break;
            case CameraState.CNCMACHINE:
                targetPosition = targetCNC.position;
                targetRotation = focusCNC.position;
                break;
            case CameraState.HAND01:
                targetPosition = targetHand01.position;
                targetRotation = focusHand01.position;
                break;
            case CameraState.HAND02:
                targetPosition = targetHand02.position;
                targetRotation = focusHand02.position;
                break;
            case CameraState.SCANNER01:
                targetPosition = targetScanner01.position;
                targetRotation = focusScanner01.position;
                break;
            case CameraState.SCANNER02:
                targetPosition = targetScanner02.position;
                targetRotation = focusScanner02.position;
                break;

        }
        isMoving = true;
        isRotate = true;
    }

}
