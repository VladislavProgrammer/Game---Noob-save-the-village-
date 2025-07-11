using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Playgama;

public class AttackSword : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AudioSource swordSound;
    [SerializeField] CheckDevice checkDevice;

    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool canCombo2 = false;
    [HideInInspector] public bool canCombo3 = false;
    [HideInInspector] public float delayCombo2, delayCombo3;

    public float BaseDamage = 10f;


    void OnEnable()
    {
        BaseDamage = GameData.BaseDamage;
        if (BaseDamage == 0)
        {
            BaseDamage = 10f;
        }
        Debug.Log("Базовый урон: " + BaseDamage);
    }
    
    public void OnClickAttackSwordBTC()
    {
        PlayAnimAttackSword();
        EventManager.CallPlayerAttackEvent();
    }

    public void ChangeBaseDamage(float value)
    {
        BaseDamage += value;
        GameData.BaseDamage = BaseDamage;
        PlayerPrefs.SetFloat("BaseDamage", BaseDamage);
        Save("BaseDamage", BaseDamage);
    }
    
    void PlayAnimAttackSword()
    {

        if (!canCombo2 && !canCombo3)
        {
            anim.SetTrigger("attack1");
            StartCoroutine(ChangeBoolCombo2());

        }   
        else if(canCombo2)
        {
            anim.SetTrigger("attack2");
            StartCoroutine(ChangeBoolCombo3());

        }

        else if (canCombo3)
        {
            anim.SetTrigger("attack3");
            canCombo3 = false;
            
        }

        swordSound.Play();
        canAttack = true;
        StartCoroutine(DelayChangeCanAttack());
    }

    IEnumerator ChangeBoolCombo2()
    {
        canCombo2 = true;
        yield return new WaitForSeconds(delayCombo2);
        canCombo2 = false;
    }

    IEnumerator ChangeBoolCombo3()
    {
        canCombo3 = true;
        yield return new WaitForSeconds(delayCombo3);
        canCombo3 = false;
    }

    private void Update()
    {
        CheckClickKeyBoard();
    }

    void CheckClickKeyBoard()
    {
        if (!checkDevice.IsMobileDevice())
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClickAttackSwordBTC();
            }

            if (Input.GetMouseButtonUp(0))
            {
                UpClickaAttackSwordBTC();
            }

            
        }
    }

    

    void StopAnimAttackSword()

    {
        //anim.SetBool("attackSword", false);
        


    }

    IEnumerator DelayChangeCanAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = false;
    }

    public void UpClickaAttackSwordBTC()
    {
        StopAnimAttackSword();
    }
    
    
    void Save(string name, float value)
    {
        var key = new List<string> { name };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
    }
    
    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
}
