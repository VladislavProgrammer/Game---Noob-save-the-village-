using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform mainCamera;


    private void LateUpdate()
    {
        transform.LookAt(mainCamera);
    }
}
