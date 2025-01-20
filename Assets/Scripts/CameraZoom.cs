using UnityEngine;
using Unity.Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CinemachineCamera virtualCamera;
    [SerializeField] float sensitivity = 10f;
    CinemachineComponentBase componentBase;
    float cameraDistance;

    private void Update(){
        if(componentBase == null){
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0){
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            if(componentBase is CinemachinePositionComposer){
                (componentBase as CinemachinePositionComposer).CameraDistance -= cameraDistance;
            }
        }
    }
}
