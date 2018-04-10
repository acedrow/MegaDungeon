using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	//Reference to the text feed text object (the object to which the TextFeedManager script is attached.
	public GameObject TextFeedObject;
	static TextFeedManager textFeedManager;

	// Use this for initialization
	void Start ()
	{
		setup ();
	}

	void setup ()
	{
		textFeedManager = TextFeedObject.GetComponent <TextFeedManager> ();
	}

	public static void toTextFeed (string m)
	{
		textFeedManager.toTextFeed (m);
	}
	// Update is called once per frame
	void Update ()
	{
		
	}
}
