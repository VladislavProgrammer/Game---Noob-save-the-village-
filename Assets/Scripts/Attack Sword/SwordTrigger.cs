using UnityEngine;
using System.Collections;

public class SwordTrigger : MonoBehaviour
{

    public int damage;

    [SerializeField] private AttackSword attackSword;

    [SerializeField]
    AudioSource armorGolemHitSound;

    [SerializeField] GameObject armorWarningBG;

    [SerializeField] private GameObject swordHitEffect;

    public int force = 50;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (attackSword.canAttack)
            {
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                EnemyMove enemyMove = other.gameObject.GetComponent<EnemyMove>();

                if (enemyHealth)
                {
                    enemyHealth.ChangeHealth(damage);
                    enemyMove.Stun();
                    Instantiate(swordHitEffect, transform.position, Quaternion.identity);

                }


                other.gameObject.transform.position -= other.gameObject.transform.forward * force * Time.deltaTime;

            }
            
            
        }

        if(other.gameObject.tag == "BossHead")
        {
            if (attackSword.canAttack)
            {
                other.gameObject.GetComponent<HeadAttack>().DeactiveHead();
                
            }
        }

        if(other.gameObject.tag == "Golem")
        {
            if (attackSword.canAttack)
            {
                ShowArmorWarningBG();
            }
        }
    }


    void ShowArmorWarningBG()
    {
        Debug.Log("����� ������������� !");
        armorGolemHitSound.Play();
        armorWarningBG.SetActive(true);
        StartCoroutine(DelayArmorWarningText());
    }

    IEnumerator DelayArmorWarningText()
    {
        yield return new WaitForSeconds(2);
        armorWarningBG.SetActive(false);

    }

    
    
}
