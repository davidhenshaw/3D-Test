using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float distanceFromCamera = 10f;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float panSpeed = 25f;
    [SerializeField] Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FollowPlayer();
        LockToPlayerAxis();
        FacePlayer();
    }

    private void FollowPlayer()
    {
        Vector3 playerToCamera = transform.position - playerTransform.position;
        Vector3 target = playerTransform.position + Vector3.Normalize(playerToCamera) * distanceFromCamera;
        Vector3 nextPosition = target;

        transform.position = nextPosition;
    }

    private void FacePlayer()
    {
        var cameraToPlayerVector = playerTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(cameraToPlayerVector, Vector3.up);
    }

    private void LockToPlayerAxis()
    {
        

        Vector3 playerBackwardVector = Vector3.Normalize(playerTransform.forward * -1);  
        Vector3 targetPosition = playerBackwardVector * distanceFromCamera;

        transform.position = playerTransform.position + targetPosition;
        transform.position += cameraOffset;
    }

}
