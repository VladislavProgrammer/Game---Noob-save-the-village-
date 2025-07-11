using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class BossEffects : MonoBehaviour
{
    public AudioSource bossDamagableSound;
    public RagdollLogic rg;
    public BoxCollider boxCollider;
    public Animator bossAnim;
    public GameObject boss, bossHPBG, fragCounterBG;
    [SerializeField] private float health = 100;
    [SerializeField] private Image bossHealthBar;
    [SerializeField] private float takeDamage;
    [SerializeField] AudioSource deathSound;

    private void Awake()
    {
        bossHPBG.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.BossTakeDamage += ChangeHealth;
        EventManager.StartBossFight += ActivateBoss;
    }


    private void OnDisable()
    {
        EventManager.BossTakeDamage -= ChangeHealth;
        EventManager.StartBossFight -= ActivateBoss;

    }


    public void ChangeHealth()
    {
        bossDamagableSound.Play();

        if (health > 0)
        {

            health -= takeDamage;
            bossHealthBar.fillAmount = health / 100;
        }
        else
        {
            Debug.Log("Босс погиб");
            BossDeath();
            
        }

    }


    void BossDeath()
    {
        if(deathSound) deathSound.Play();
        if(bossAnim) bossAnim.enabled = false;
        boxCollider.enabled = false;
        if(rg) rg.ActivateRagdoll();
        StartCoroutine(DelayDeactive());
        EventManager.CallEndBossFight();
    }

    IEnumerator DelayDeactive()
    {
        yield return new WaitForSeconds(3);
        bossHPBG.SetActive(false);
        boss.SetActive(false);
        
    }


    void ActivateBoss()
    {
        boss.SetActive(true);
        bossHPBG.SetActive(true);
        fragCounterBG.SetActive(false);
    }
}
