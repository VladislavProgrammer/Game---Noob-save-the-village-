using Playgama;
using Playgama.Modules;
using TMPro;
using UnityEngine;

public class CheckDevice : MonoBehaviour
{

    [SerializeField]
    GameObject mobileInput, buttonAttackPC;

    [SerializeField]
    GameObject iconButtonE, iconButtonR, iconButtonF, iconButtonP;

    [SerializeField]
    GameObject iconButtonG, wasdIcon;

    [SerializeField]
    GameObject canvasPanelMobile;

    [SerializeField]
    GameObject tutorialIconPC, tutorialIconMobile;

    private void Awake()
    {

        

        if (IsMobileDevice())
        {
            LookMobile();
        }

        else
        {
            LookPc();

        }


    }

    public bool IsMobileDevice()
    {
        //string deviceUser = YandexGame.EnvironmentData.deviceType;

        string deviceUser = Bridge.device.type.ToString();
        //Debug.Log("����������: " + deviceUser);

        {
            if (deviceUser == "Mobile" || deviceUser == "Tablet")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }

    public bool IsRussianLanguage()
    {
        if (Bridge.platform.language == "ru")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void LookPc()
    {
       if(mobileInput) mobileInput.SetActive(false);
       if(buttonAttackPC) buttonAttackPC.SetActive(true);
       if(canvasPanelMobile) canvasPanelMobile.SetActive(false);

        if (iconButtonE) iconButtonE.SetActive(true);
        if (iconButtonR) iconButtonR.SetActive(true);
        if (iconButtonF) iconButtonF.SetActive(true);
        if (iconButtonP) iconButtonP.SetActive(true);
        if (iconButtonG) iconButtonG.SetActive(true);
        if (wasdIcon) wasdIcon.SetActive(true);
        if (tutorialIconPC) tutorialIconPC.SetActive(true);
        if (tutorialIconMobile) tutorialIconMobile.SetActive(false);


    }


    void LookMobile()
    {
       if(mobileInput) mobileInput.SetActive(true);
       if(buttonAttackPC) buttonAttackPC.SetActive(false);
       if(canvasPanelMobile) canvasPanelMobile.SetActive(true);

        if (iconButtonE) iconButtonE.SetActive(false);
        if (iconButtonR) iconButtonR.SetActive(false);
        if (iconButtonF) iconButtonF.SetActive(false);
        if (iconButtonP) iconButtonP.SetActive(false);
        if (iconButtonG) iconButtonG.SetActive(false);
        if (wasdIcon) wasdIcon.SetActive(false);
        if (tutorialIconPC) tutorialIconPC.SetActive(false);
        if (tutorialIconMobile) tutorialIconMobile.SetActive(true);

    }




}
