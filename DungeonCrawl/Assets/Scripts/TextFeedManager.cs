using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFeedManager: MonoBehaviour
{

	public Text text;
	private int i;
	public bool stop;
	public GameObject scrollbarObject;
	Scrollbar scrollbar;

	// Use this for initialization
	void Start ()
	{
		setup ();
	}

	public void toTextFeed (string m)
	{
		string output = "\n" + ">" + m;
		text.text += output;
		scrollbar.value = 0;
	}

	void setup ()
	{
		scrollbar = scrollbarObject.GetComponent <Scrollbar> ();
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
