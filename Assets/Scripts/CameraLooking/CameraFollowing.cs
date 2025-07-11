using UnityEngine;

public class CameraFollowing : MonoBehaviour
{

    [SerializeField]
    Transform targetCamera;


    void LateUpdate()
    {
        Following();
        
    }


    void Following()
    {
        transform.position = targetCamera.position;
    }

}
