using UnityEngine;
using System.Collections;

public class HeadAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float speed;
    [SerializeField] private int _damage;
    public ParticleSystem destroyEffect;
   

    private void OnEnable()
    {
        SetTargetPoint();
    }

    private void Update()
    {
        AttackTarget();
    }

    void SetTargetPoint()
    {
        target = GameObject.FindGameObjectWithTag("TargetPoint").GetComponent<Transform>();

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth) playerHealth.RemoveHealth(_damage);
            DamagePlayer();
        }

        if (other.gameObject.tag == "Sword")
        {
            DeactiveHead();
        }
    }

    void DamagePlayer()
    {
        gameObject.SetActive(false);
    }

    public void DeactiveHead()
    {
        DestroyEffect();
        gameObject.SetActive(false);
        EventManager.CallBossTakeDamage();
    }
  
    void DestroyEffect()
    {
        destroyEffect.transform.SetParent(null);
        destroyEffect.Play();
        StartCoroutine(DelayDeactiveEffect());

    }

    IEnumerator DelayDeactiveEffect()
    {
        yield return new WaitForSeconds(3);
        destroyEffect.gameObject.SetActive(false);
    }

    

    void AttackTarget()
    {
        transform.LookAt(target);
        transform.position += transform.forward * speed * Time.deltaTime;

    }
}
