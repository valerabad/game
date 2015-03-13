using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PaintSqare : MonoBehaviour {
	
	private string ourString; 
	private LineRenderer line;
	private List<Vector3> pointsList;
	private List<Vector3> pointsList2;
	private Vector3 semileVector;


	
	// Use this for initialization
	void Start () {
		pointsList = new List<Vector3>();
		pointsList2 = new List<Vector3>();
		semileVector = new Vector3 ();
		Vector3 Vec1 = new Vector3();
		Vector3 Vec2 = new Vector3();

		line = gameObject.AddComponent<LineRenderer>();
		//line.material =  new Material(Shader.Find("Particles/Additive")); //Particles/AlphaBlended Additive
		
		line.SetWidth(0.1f,0.1f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;


		StreamReader sr = new StreamReader("C:\\test\\Code_test.txt", Encoding.Default);
		ourString = sr.ReadToEnd ();
		sr.Close ();
		//Debug.Log (ourString);
		line.SetVertexCount (5);

		line.SetPosition (0,new Vector3(0.0f,0.0f,0.0f));
		line.SetPosition (1,new Vector3(1.0f,0.0f,0.0f));
		line.SetPosition (2,new Vector3(1.0f,1.0f,0.0f));
		line.SetPosition (3,new Vector3(0.0f,1.0f,0.0f));
		line.SetPosition (4,new Vector3(0.0f,0.0f,0.0f));



		pointsList.Add (new Vector3(0.0f,0.0f,0.0f));
		pointsList.Add (new Vector3(0.0f,0.0f,0.0f));
		pointsList.Add (new Vector3(200.0f,12.0f,130.0f));
		pointsList.Add (new Vector3(1.0f,1.0f,0.0f));


		pointsList2.Add (new Vector3(0.0f,0.0f,0.0f));
		pointsList2.Add (new Vector3(50.0f,0.0f,0.0f));
		pointsList2.Add (new Vector3(200.5f,12.12f,130.99f));
		pointsList2.Add (new Vector3(0.0f,1.0f,0.0f));

		for ( int i = 0; i<pointsList.Count; i++) {
			Vec1 = pointsList[i];
			Vec2 = pointsList2[i];
			semileVector =  (Vec1-Vec2);

			if (  (Mathf.Abs(semileVector.x) <= 1.0f) && (Mathf.Abs(semileVector.y) <= 1.0f) && (Mathf.Abs(semileVector.z) <= 1.0f) )
				{
					Debug.Log("Ura");
				    Debug.Log (semileVector);
				//Debug.Log (semileVector.y);
				//Debug.Log (semileVector.z);
					//break;
				}
			else {
				Debug.Log("False");
				break;
			}
		}
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
/*
		System.Collections.IEnumerator myEnumerator = pointsList.GetEnumerator ();
		System.Collections.IEnumerator myEnumerator2 = pointsList2.GetEnumerator ();

		while ((myEnumerator.MoveNext()) ) 
		{
			if (pointsList2 != pointsList)
			  Debug.Log(myEnumerator.Current);
		}
        */
