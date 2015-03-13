using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {
	private TrailRenderer trLine;
	// Use this for initialization
	void Awake()
	{
		trLine = gameObject.AddComponent<TrailRenderer>();
		trLine.material = new Material (Shader.Find("Particles/Additive"));
		trLine.startWidth = 0.1f;
		trLine.endWidth = 0.1f;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
