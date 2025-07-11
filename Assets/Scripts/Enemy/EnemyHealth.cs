using System.Collections;
using UnityEngine.AI;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;

    private float _currentHealth;

    [SerializeField] private RagdollLogic rl;
    [SerializeField] HealthBarView healthBarView;
    [SerializeField] EnemyMove enemyMove;
    

    [SerializeField] GameObject diamondObj;
    [SerializeField] ParticleSystem deathEffect;

    public int TakeDamageInSparkTrap = 1;

    [SerializeField] AudioSource _hitSound;
    [SerializeField] AudioSource _deathSound;
    
    private float explodeForce = 1000f;
    private float explodeRadius = 50f;

    private void Awake() => _currentHealth = _maxHealth;
    private void OnEnable()
    {
        diamondObj.SetActive(false);
        
    }

   

    public void ChangeHealth(float value)
    {
        int soundIndex = PlayerPrefs.GetInt("soundIndex");

        _currentHealth -= value;

        if(_currentHealth <= 0)
        {
           EnemyDeath();
        }

        else
        {
            float _currentHealthPercantage = (float)_currentHealth / _maxHealth;
            healthBarView.ChangeHealthView(_currentHealthPercantage);
            if(soundIndex != 1) PlayHitSound();
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "SpikeTrap")
        {
            ChangeHealth(TakeDamageInSparkTrap);
        }

    }
    

    void PlayHitSound() => _hitSound.Play();
    void PlayDeathSound() => _deathSound.Play();    
    


    public void EnemyDeath()
    {
        int soundIndex = PlayerPrefs.GetInt("soundIndex");

        EventManager.CallEnemyDeath();
        if(soundIndex != 1) PlayDeathSound();
        deathEffect.Play();
        deathEffect.transform.SetParent(null);
        diamondObj.SetActive(true);
        diamondObj.transform.SetParent(null);
        rl.ActivateRagdoll();
        DeactiveComponents();
        Explode();
        StartCoroutine(DelayAfterDeactive());
        
        
    }

    void Explode()
    {
        Debug.Log("Бабах и разлетелся на части!!!");
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, explodeRadius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(explodeForce, transform.position, explodeRadius);

            }
        }
    }

    void DeactiveComponents()
    {
       if(enemyMove) enemyMove.enabled = false;
       if(enemyMove) enemyMove.CanAttack = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }
    
    IEnumerator DelayAfterDeactive()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }



}





















//makhonin