using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource keySound;

    [SerializeField]
    private float rotateForce;

    private void OnTriggerEnter(Collider other)
    {
        keySound.Play();
        EventManager.CallLevelCompleted();
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateForce * Time.deltaTime);
    }

}
