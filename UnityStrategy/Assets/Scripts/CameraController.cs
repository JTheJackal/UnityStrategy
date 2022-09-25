using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    private const float zoomAmount          = 1f;
    private const float zoomSpeed           = 2f;

    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start(){

        cinemachineTransposer   = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset      = cinemachineTransposer.m_FollowOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    // Handle camera movement
    private void HandleMovement(){

        Vector3 inputMoveDir    = new Vector3(0, 0, 0);
        float moveSpeed     = 10f;

        if(Input.GetKey(KeyCode.W)){

            inputMoveDir.z  = +1f;
        }

        if(Input.GetKey(KeyCode.S)){

            inputMoveDir.z  = -1f;
        }

        if(Input.GetKey(KeyCode.A)){

            inputMoveDir.x  = -1f;
        }

        if(Input.GetKey(KeyCode.D)){

            inputMoveDir.x  = +1f;
        }

        // Prepare camera controller movement based on it's current rotation
        Vector3 moveVector  = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;

        // Move the camera controller
        transform.position  += moveVector * moveSpeed * Time.deltaTime;
    }

    // Handle rotation of the camera
    private void HandleRotation(){

        Vector3 rotationVector  = new Vector3(0, 0, 0);
        float rotationSpeed = 50f;

        if(Input.GetKey(KeyCode.Q)){

            rotationVector.y  = +1f;
        }

        if(Input.GetKey(KeyCode.E)){

            rotationVector.y  = -1f;
        }

        transform.eulerAngles   += rotationVector * rotationSpeed * Time.deltaTime;
    }

    // Handle zooming of the camera
    private void HandleZoom(){

        // Check for zooming on the mouse wheel
        if(Input.mouseScrollDelta.y > 0){
            
            targetFollowOffset.y      -= zoomAmount;
        }else if(Input.mouseScrollDelta.y < 0){

            targetFollowOffset.y      += zoomAmount;
        }

        // Enforce min and max zoom
        targetFollowOffset.y          = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        // Apply the zoom
        cinemachineTransposer.m_FollowOffset    = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
