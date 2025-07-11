using UnityEngine;

public class TnTTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource takeSound;

    [SerializeField]
    private float rotateForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            takeSound.Play();
            EventManager.CallLevelCompleted();
        }
        
    }

    private void Update()
    {
        transform.Rotate(0, rotateForce * Time.deltaTime, 0);
    }
}
