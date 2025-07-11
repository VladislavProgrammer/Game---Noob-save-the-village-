using UnityEngine;

public class UpSpeedBuyTrigger : BuyTrigger
{

    [SerializeField] private ThirdPersonController thirdPersonController;
    [SerializeField] private float _upSpeed;
    [SerializeField] private ParticleSystem _upSpeedEffect;
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
            UpSpeed();
            EventManager.CallChangeScore(_price);
        }

        else
        {
            _nopeSound.Play();
        }
    }

    void UpSpeed()
    {
        thirdPersonController.ChangeSpeed(_upSpeed);
        _upSpeedEffect.Play();
    }
}
