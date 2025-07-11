using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpikeTrapTrigger : MonoBehaviour
{

    //[SerializeField] private int damage;
    public bool canAdd = true;
    [SerializeField] int delayLifeTime = 5;
    [SerializeField] Button spawnSpikeBtc;

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.tag == "Enemy")
        //{
            //other.gameObject.GetComponent<EnemyHealth>().ChangeHealth(damage);

       // }
    }

    private void OnEnable()
    {
        StartCoroutine(DelayDeactive());
    }

    IEnumerator DelayDeactive()
    {

        canAdd = false;
        spawnSpikeBtc.interactable = false;
        yield return new WaitForSeconds(delayLifeTime);
        canAdd = true;
        gameObject.SetActive(false);
        spawnSpikeBtc.interactable = true;
    }

     
}
