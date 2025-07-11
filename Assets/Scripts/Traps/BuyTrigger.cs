using UnityEngine;
using TMPro;

public abstract class BuyTrigger : MonoBehaviour
{
    [SerializeField] protected int _price;
    [SerializeField] protected TextMeshPro _priceText;
    [SerializeField] protected ScoreLogic _scoreLogic;
    [SerializeField] protected AudioSource _buySound;
    [SerializeField] protected AudioSource _nopeSound;
    
    private void Awake()
    {
        _priceText.text = _price.ToString();
    }

}
