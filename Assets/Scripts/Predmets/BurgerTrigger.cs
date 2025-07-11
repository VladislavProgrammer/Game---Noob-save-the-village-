using UnityEngine;

public class BurgerTrigger : MonoBehaviour
{
    [SerializeField]
    private float healthPoints;

    [SerializeField]
    private float rotateForce;

    [SerializeField]
    private AudioSource eatSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().Heal(healthPoints);
            eatSound.Play();
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Rotate(0, rotateForce * Time.deltaTime, 0);
    }
}
