using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [SerializeField] GameObject _fireEffect;
    [SerializeField] int _delayAfter = 3, _activeTime = 3;


    private void OnEnable()
    {
        StartCoroutine(StartFire()); 
    }

    IEnumerator StartFire()
    {
        yield return new WaitForSeconds(_delayAfter);
        _fireEffect.SetActive(true);
        yield return new WaitForSeconds(_activeTime);
        _fireEffect.SetActive(false);
        gameObject.SetActive(false);


    }

}
