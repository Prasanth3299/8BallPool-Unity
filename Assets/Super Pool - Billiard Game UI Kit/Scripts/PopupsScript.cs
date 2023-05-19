using UnityEngine;
using System.Collections;

public class PopupsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey("escape"))
			Application.Quit();
	
	}
	public void SettingsOn()
	{

		GameObject.Find("Canvas Global").transform.Find("Options").gameObject.SetActive(true);

	}

	public void SettingsOff()
	{

		GameObject.Find("Canvas Global").transform.Find("Options").gameObject.SetActive(false);

	}



}
