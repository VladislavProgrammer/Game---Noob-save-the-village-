using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class NoobGirlHealth : MonoBehaviour
{
    public float Health;
    public Image GirlHelathBar;
    public float Damage = 10;

    public GameObject GirlAvatarWarning;


    private void OnEnable()
    {
        EventManager.GirlDamage += DamageEffectWarning;
        EventManager.StopGirlDamage += HideDamageEffect;
    }

    private void OnDisable()
    {
        EventManager.GirlDamage -= DamageEffectWarning;
        EventManager.StopGirlDamage -= HideDamageEffect;


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            ChangeHealth(Damage);
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GirlAvatarWarning.SetActive(false);
            
        }
    }

    void DamageEffectWarning()
    {
        GirlAvatarWarning.SetActive(true);
        

    }

    void HideDamageEffect()
    {
        GirlAvatarWarning.SetActive(false);

    }

    void ChangeHealth(float damage)
    {
        if (Health >= 0)
        {

            Health -= damage;
            GirlHelathBar.fillAmount = Health / 100;
        }
        else
        {
            Debug.Log("Вы погибли");
            EventManager.CallGameOver();
        }

    }

   
    


}
