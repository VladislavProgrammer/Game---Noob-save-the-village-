using UnityEngine;
using UnityEngine.EventSystems; 


public class CameraMove : MonoBehaviour
{

    public bool clickToMoveCamera = false;
    public bool canZoom = true;
    public float sensitivity = 0.5f;
  
    public Vector2 cameraLimit = new Vector2(-45, 40);



    float mouseX;
    float mouseY;


    public Vector3 lookInput;

    bool isMobile;

    [SerializeField]
    CheckDevice checkDevice;

   


    void Awake()
    {
        if (checkDevice.IsMobileDevice())
        {
            isMobile = true;
        }

    }



    void Update()
    {

        if (isMobile)
        {
           LookMobile();
           
        }

        else
        {
            LookPC();
            
        }

    }

    public void ButtonTest () {
        Debug.Log("Есть нажатие");
    }
 
    void LookPC(){
        
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        

        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);


        lookInput = new Vector3(-mouseY, mouseX, 0);

        transform.rotation = Quaternion.Euler(lookInput);
    }


    void LookMobile(){
        
        mouseX += SimpleInput.GetAxis("PanelX") * sensitivity;
        mouseY += SimpleInput.GetAxis("PanelY") * sensitivity;
        

        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);


        lookInput = new Vector3(-mouseY, mouseX, 0);

        transform.rotation = Quaternion.Euler(lookInput);
    }

}
