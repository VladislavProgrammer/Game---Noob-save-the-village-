using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TNT : MonoBehaviour
{

    [SerializeField] AudioSource bombSound;
    [SerializeField] ParticleSystem bombEffect;
    public float Force;
    public float Radius;
    public int delay;

    public Button TNTButton;
    public bool canExplode = true;


    private void OnEnable()
    {
        if (canExplode)
        {
            StartCoroutine(DelayDetonatnion());
            canExplode = false;
            TNTButton.interactable = false;
        }
        
    }


    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, Radius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                if (rigidbody.gameObject.GetComponent<EnemyHealth>())
                {
                    rigidbody.gameObject.GetComponent<EnemyHealth>().EnemyDeath();
                }

                rigidbody.AddExplosionForce(Force, transform.position, Radius);
                
            }
        }

        
        bombEffect.Play();
    }

    IEnumerator DelayDetonatnion()
    {
        bombSound.Play();
        yield return new WaitForSeconds(delay);
        Explode();
        gameObject.SetActive(false);
        canExplode = true;
        TNTButton.interactable = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
