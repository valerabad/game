using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class testspainting : MonoBehaviour {

	private string ourString; 
	private LineRenderer line;


	// Use this for initialization
	void Start () {
		// Create line renderer component and set its property
		line = gameObject.AddComponent<LineRenderer>();
		//line.material =  new Material(Shader.Find("Particles/Additive")); //Particles/AlphaBlended Additive
		
		line.SetWidth(0.05f,0.05f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;
		Debug.Log ("hello!");
		StreamReader sr = new StreamReader("C:\\test\\Code_test.txt", Encoding.Default);
		ourString = sr.ReadToEnd ();
		sr.Close ();
		Debug.Log (ourString);
		line.SetVertexCount (5);
	//	line.SetWidth(0.1f,0.1f);// Set the number of line segments.
		line.SetPosition (0,new Vector3(0.0f,0.0f,0.0f));
		line.SetPosition (1,new Vector3(1.0f,0.0f,0.0f));
		line.SetPosition (2,new Vector3(1.0f,1.0f,0.0f));
		line.SetPosition (3,new Vector3(0.0f,1.0f,0.0f));
		line.SetPosition (4,new Vector3(0.0f,0.0f,0.0f));
		//line.SetWidth(0.1f,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		//line.SetVertexCount(pointsList.Count);

		//line.SetVertexCount (1);
	}
	void Awake()  //событие выпоняется перед Start()
	{
			

	}
}
