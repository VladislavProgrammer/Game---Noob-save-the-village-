using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ID;
    [SerializeField] protected GameObject swordHitEffect;
    [SerializeField] protected float damageMultipler;
    [SerializeField] protected AttackSword attackSword;
    [HideInInspector] public bool CanDamage;

    public abstract void DamageOther(Collider other);
    

}
