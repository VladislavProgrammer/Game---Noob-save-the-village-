using UnityEngine.UI;
using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;


    
    public void ChangeHealthView(float value)
    {
        //Debug.Log(value);
        _healthBarFilling.fillAmount = value;
    }

}
