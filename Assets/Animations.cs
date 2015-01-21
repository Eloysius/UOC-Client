using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

	private Animator _animator;
	public float v;
	public float h;
	public float run;

	// Use this for initialization
	void Start () 
	{
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			v = Input.mousePosition.x; 
			h = Input.mousePosition.y;
		} 
		else
		{
			v = 0;
			h = 0;
		}
		Running ();
	}

	void FixedUpdate()
	{
		_animator.SetFloat ("Walk", v);
		_animator.SetFloat ("Turn", h);
		_animator.SetFloat ("Run", run);

	}

	void Running() 
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			run = 0.2f;
		}
		else
		{
			run = 0.0f;
		}
	}


}
