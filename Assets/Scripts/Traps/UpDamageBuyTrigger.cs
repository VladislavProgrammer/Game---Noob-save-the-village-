using UnityEngine;


public class UpDamageBuyTrigger : BuyTrigger
{
    [SerializeField] private AttackSword attackSword;
    [SerializeField] private float _upDamageValue;
    [SerializeField] private ParticleSystem _upDamageEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TryBuy();
        }
    }

    void TryBuy()
    {
        if (_scoreLogic.score >= _price)
        {
            _buySound.Play();
            ChangeDamage();
            EventManager.CallChangeScore(-_price);
        }

        else
        {
            _nopeSound.Play();
        }
        
        
    }

    void ChangeDamage()
    {
        attackSword.ChangeBaseDamage(_upDamageValue);
        _upDamageEffect.Play();
    }
}
