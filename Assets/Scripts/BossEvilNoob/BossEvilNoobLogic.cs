using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvilNoobLogic : MonoBehaviour
{
    [SerializeField] GameObject[] _spellsZone;
    [SerializeField] int _delayCastSpell= 7;
   
    


    private void OnEnable()
    {
        foreach (GameObject spellObj in _spellsZone)
        {
            spellObj.SetActive(false); // выключаем все заклинания
        }

        StartCoroutine(CastSpell());
        
    }


    


    
    IEnumerator CastSpell()
    {
        int randomIndex = Random.Range(0, _spellsZone.Length);
        _spellsZone[randomIndex].SetActive(true);
        yield return new WaitForSeconds(_delayCastSpell);
        _spellsZone[randomIndex].SetActive(false);
        StartCoroutine(CastSpell());
    }


   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            EventManager.CallBossTakeDamage();

        }
    }

}
