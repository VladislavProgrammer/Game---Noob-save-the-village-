using DG.Tweening;
using UnityEngine;

public class SellerTrigger : MonoBehaviour
{

    public GameObject ShopBG;
    public AudioSource sellerTriggerSound;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ShowShopBG();
            sellerTriggerSound.Play();
        }
                
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CloseShopBG();
        }
    }

    void ShowShopBG()
    {
        ShopBG.transform.DOScale(1, 1);
        EventManager.CallUnlockCursor();
        EventManager.CallLockControls();
    }

    public void CloseShopBG()
    {
        ShopBG.transform.DOScale(0, 1);
        EventManager.CallLockCursor();
        EventManager.CallUnlockControls();
    }

}
