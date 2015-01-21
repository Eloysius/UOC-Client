using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
    {
    public  GameObject                          prefabPlayerRift;
    public  GameObject                          prefabPlayerNoRift;
    public  GameObject                          prefabMouseCursorMovement;
    
    [HideInInspector] public  bool              VR_MODE;                        // Has the HMD been detected
    [HideInInspector] public  GameObject        playerGameObject;               // Player GameObject
    [HideInInspector] public  Transform         mainCameraAnchor;               // Main Camera
    [HideInInspector] public  Transform         topDownCameraTransform;         // Extra Camera(disabled) used for mouse tracking in VR mode

	private void Awake( )
        {
        if( OVRManager.display.isPresent )
            {
            VR_MODE                                 = true;
            playerGameObject                        = (GameObject)Instantiate( prefabPlayerRift, new Vector3( 1.0f, 0.0f, 1.0f ), Quaternion.identity );
            mainCameraAnchor                        = playerGameObject.transform.Find( "OVRCameraRig/CenterEyeAnchor" );
            topDownCameraTransform                  = playerGameObject.transform.Find( "TopDownCam" );
            topDownCameraTransform.camera.enabled   = false;
            }
        else
            {
            VR_MODE                                 = false;
            OVRManager OVRManagerScript             = GetComponent<OVRManager>( );
            OVRManagerScript.enabled                = false;
            playerGameObject                        = (GameObject)Instantiate( prefabPlayerNoRift, new Vector3( 1.0f, 0.0f, 1.0f ), Quaternion.identity );
            mainCameraAnchor                        = playerGameObject.transform.Find( "Camera" );
            }

        Screen.lockCursor = false;
        Screen.showCursor = false;
        }
    
    private void Start( )
        {
	    }
	
	void Update( )
        {
	    }
    }
