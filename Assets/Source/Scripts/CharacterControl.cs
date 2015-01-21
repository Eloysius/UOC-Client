using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
    {
    private GameManager     _gameManager;
    private GameObject      _mouseCursor;
	private Animator 		_animator;

	public float movementSpeed = 0.5f;
	public float curMovementSpeed = 0.0f;
	public float rotationSpeed = 75.0f;
	public float strafeSpeed   = 0.25f;

	private GameObject player;
    
	//My line is better!

	private void Start( )
        {
        GameObject CLIENT = GameObject.Find( "CLIENT" );
	    _gameManager = CLIENT.GetComponent<GameManager>( );
        _mouseCursor = null;
        _mouseCursor = (GameObject)Instantiate( _gameManager.prefabMouseCursorMovement );
		_animator = GetComponent<Animator>();
	    }

	private void Update( )
        {
        int layerMask = 7 << 8; //layerMask = ~layerMask; // To Invert layer

        Ray ray = new Ray( );
        if( _gameManager.VR_MODE )
            {
            ray = _gameManager.topDownCameraTransform.camera.ScreenPointToRay( Input.mousePosition );
            Debug.Log( _gameManager.topDownCameraTransform.position +""+ Input.mousePosition);
            }
        else
            {
            ray = _gameManager.mainCameraAnchor.camera.ScreenPointToRay( Input.mousePosition );
            }

        RaycastHit raycastHit;
        if( Physics.Raycast( ray, out raycastHit, 3f, layerMask ) ) 
            {
            if( _mouseCursor == null )
                {
                _mouseCursor = (GameObject)Instantiate( _gameManager.prefabMouseCursorMovement );
                }
                
            Vector3 mousePosition                = new Vector3( raycastHit.point.x, 0.0067f, raycastHit.point.z );
            _mouseCursor.transform.localPosition = mousePosition;
            _mouseCursor.transform.localRotation = Quaternion.Euler( new Vector3( 0.0f, 0.0f, 90.0f ) );


			player = GameObject.FindGameObjectWithTag("Player");

			if( Input.GetMouseButton( 1 ) ) 
                {
                	float distance = Vector3.Distance( transform.position, _mouseCursor.transform.position );
                	Vector3 direction = new Vector3( raycastHit.point.x, 0.0f, raycastHit.point.z );
                	transform.position = Vector3.MoveTowards( transform.position, raycastHit.point, movementSpeed * Time.deltaTime * distance );
                	transform.position = new Vector3( transform.position.x, 0.0f, transform.position.z );
					player.transform.LookAt(direction);
					
					curMovementSpeed = movementSpeed * distance;
					if (curMovementSpeed <= 0.0f)
						curMovementSpeed = 0.0f;

					_animator.SetFloat ("moveSpeed", curMovementSpeed);
				}
				else
				{
					_animator.SetFloat ("moveSpeed", 0.0f);
				}
        	}
            

        float mouseWheelInput = Input.GetAxis( "Mouse ScrollWheel" );
        if( mouseWheelInput != 0 )
            {
            transform.Rotate( Vector3.down * mouseWheelInput * rotationSpeed );
            }
	    }

}