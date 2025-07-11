using UnityEngine;

public class CursorLocker : MonoBehaviour
{

    [SerializeField] CheckDevice checkDevice;
    [SerializeField] CameraMove cameraMove;
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] AttackSword attackSword;

    private void OnEnable()
    {
        EventManager.LockCursor += HideCursor;
        EventManager.UnLockCursor += ShowCursor;
        EventManager.LockControls += LockPlayersInputs;
        EventManager.UnLockControls += UnLockPlayerInputs;
    }


    private void OnDisable()
    {

        EventManager.LockCursor -= HideCursor;
        EventManager.UnLockCursor -= ShowCursor;
        EventManager.LockControls -= LockPlayersInputs;
        EventManager.UnLockControls -= UnLockPlayerInputs;


    }

    void ShowCursor()
    {
        if (!checkDevice.IsMobileDevice())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("����� ������");
        }
        else Debug.Log("���� ��� �������");
       
    }


    void HideCursor()
    {
        if (!checkDevice.IsMobileDevice())
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("�� ����� ������");
        }
        else Debug.Log("���� ��� �������");
    }


    void LockPlayersInputs()
    {
        if (attackSword)
        {
            attackSword.enabled = false;

        }
        cameraMove.enabled = false;
        thirdPersonController.enabled = false;
        
    }

    
    void UnLockPlayerInputs()
    {
        if (attackSword)
        {
            attackSword.enabled = true;
        }
        cameraMove.enabled = true;
        thirdPersonController.enabled = true;
    }
}
