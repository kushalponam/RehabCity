using UnityEngine;
using System.Collections;
/*Attach this class to player object in any scene where 2d slection is required
 * 
*/
public class Player_SM : MonoBehaviour {
   // the slected object is stored in this vaiable
    private SelectObjects objselection;
    private GameManager GM;
    private CalibrationManager CM;
    private Vector2 DirectionVector;
    private AnalogStick analogstick;
    // Sensitivity of the cursor movement on screen
    public float Sensitivity;

	void Start () {
        objselection = GetComponent<SelectObjects>();
        GM = GameManager.Instance;
        CM = CalibrationManager.Instance;
	    analogstick = AnalogStick.Instance;
       // Debug.Log(Screen.height + " " + Screen.width);
#if UNITY_WEBPLAYER
        AdjustCameraOnWebBuild();
#endif
#if UNITY_ANDROID
        AdjustCameraOnWebBuild();
#endif
	}
	void Update () {
        // Get the direction vector from the joystick
	    if (GM.IsAndroid && !GM.IsCalibrationRequired)
	    {
	        DirectionVector.x = analogstick.GetAxis().x;
	        DirectionVector.y = analogstick.GetAxis().y * -1;
	    }
	    else if (GM.IsCalibrationRequired)
        {
             DirectionVector = new Vector2(CM.GetDirection(0), CM.GetDirection(1));
        }
        else if(!GM.IsAndroid)
        {
             DirectionVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
       // if the cursor is within the screen coordinates
        if (IsWithinScreen())
        {
            objselection.left += (DirectionVector.x) * Sensitivity;
            objselection.top += (DirectionVector.y) * Sensitivity;
            //Debug.Log(DirectionVector+"  "+objselection.left + " " +objselection.top);
        }
        CheckBorders();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("NewCity");
        }
	}

    // we are setting the camera's orthographic size 
    // because of web build . 
    void AdjustCameraOnWebBuild()
    {
        Camera.main.orthographicSize = 66;
    }

    bool IsWithinScreen()
    {
        if ((objselection.left >= 0 && objselection.top >= 0) && (objselection.left <= Screen.width && objselection.top <= Screen.height)) return true;
        return false;
    }
    void CheckBorders()
    {
        if (objselection.left < 0) objselection.left = 0;
        if (objselection.top < 0) objselection.top = 0;
        if (objselection.left > Screen.width) objselection.left = Screen.width;
        if (objselection.top > Screen.height) objselection.top = Screen.height;
    }
   
    
}
