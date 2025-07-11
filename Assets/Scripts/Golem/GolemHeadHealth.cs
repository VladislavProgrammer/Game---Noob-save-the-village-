using UnityEngine;

public class GolemHeadHealth : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            EventManager.CallBossTakeDamage();
        }
    }
}
