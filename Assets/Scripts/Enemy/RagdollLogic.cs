using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollLogic : MonoBehaviour
{

    private List<Rigidbody> rigidbodies;

    private Animator anim;




    private void OnEnable()
    {
        Init();
        DisableRagdoll();
    }
    
    

    void Init()
    {
        rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        anim = GetComponent<Animator>();
    }

    


    void DisableRagdoll()
    {
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        anim.enabled = true;
    }

    IEnumerator DelayOffRagdoll()
    {
        yield return new WaitForSeconds(1);
        DisableRagdoll();
    }

    public void ActivateRagdoll()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        anim.enabled = false;

        DesemberedAllRigidbodies();
       
    }

    public void Desembered()
    {
        Rigidbody randomRb = rigidbodies[Random.Range(0, rigidbodies.Count - 1)];
        CharacterJoint cj = randomRb.GetComponent<CharacterJoint>();
        Destroy(cj);
        
    }

    void DesemberedAllRigidbodies()
    {
        foreach (var rb in rigidbodies)
        {
            CharacterJoint cj = rb.GetComponent<CharacterJoint>();
            if (cj) Destroy(cj);
        }
    }
}
