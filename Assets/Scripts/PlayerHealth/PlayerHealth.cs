using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _health = 100;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private AudioSource _takeDamageSound;

    private void OnEnable()
    {
        EventManager.GameOver += Death;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= Death;

    }

    

    public void Heal(float healthPoints)
    {
        if(_health < 100)
        {
            _health += healthPoints;
            playerHealthBar.fillAmount = _health / 100;
        }
    }

    public void RemoveHealth(float damage)
    {
        if(_health >= 0)
        {
            
            _health -= damage;
            playerHealthBar.fillAmount = _health / 100;
        }
        else
        {
            Debug.Log("Игрок погиб");
            EventManager.CallGameOver();
        }

        _takeDamageSound.Play();

    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
