using UnityEngine;
using System.Collections;

public class MouseFollower : MonoBehaviour {
	public int heightOffset = 32;

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = Input.mousePosition;
		Rect rect = guiTexture.pixelInset;
		rect.x = pos.x;
		rect.y = pos.y;//+ heightOffset;

		guiTexture.pixelInset = rect;	
	}
}
