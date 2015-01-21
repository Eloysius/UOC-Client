using UnityEngine;
using System.Collections;

public class OVR_Custom : MonoBehaviour
    {
	void Start( )
        {
	    }
	
	void Update( ) 
        {
        if( Input.GetKeyDown( KeyCode.R ) )
            {
            OVRManager.display.RecenterPose();
            }
	    }
    }
