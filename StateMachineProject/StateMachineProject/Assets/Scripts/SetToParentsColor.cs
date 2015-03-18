using UnityEngine;
using System.Collections;

public class SetToParentsColor : MonoBehaviour {
	public GameObject parent;

	void Update () {
		Renderer myRenderer = GetComponent<Renderer> ();
		Renderer parentRenderer = parent.GetComponent<Renderer> ();
		myRenderer.material.color = parentRenderer.material.color;
	}
}
