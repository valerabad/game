using UnityEngine;
using System.Collections;

public class MoveSphere : MonoBehaviour {
	private Vector3 pos;
	private Vector3 tmpMousePosition;
	// Use this for initialization

	void Start () {

		//transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = ne;

		//if ((Camera.main.ScreenToWorldPoint(tmpMousePosition) != Camera.main.ScreenToWorldPoint(Input.mousePosition)) ) //&&((Input.GetMouseButtonUp(0))
		{
			pos=(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));//Camera.main.ScreenToWorldPoint
			//transform.Translate(pos*Time.deltaTime);//new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f)
			pos = Camera.main.ScreenToWorldPoint(pos);
			pos.z = 0;
			transform.position = pos;
			//Debug.Log(transform.position.x);
			//transform.TransformVector(pos);

			//(tmpMousePosition) = (Input.mousePosition);
		}
	}
}
