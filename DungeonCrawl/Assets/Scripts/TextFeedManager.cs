using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFeedManager: MonoBehaviour
{

	public Text text;
	private int i;
	public bool stop;
	public GameObject scrollrectObject;
	ScrollRect scrollrect;

	// Use this for initialization
	void Start ()
	{
		setup ();
	}

	public void toTextFeed (string m)
	{
		//actual output should be on a new line, the carrot is optional
		string output = "\n" + " >" + m;
		text.text += output;
		StartCoroutine (scrollToBottom ());
	}

	/*
	 * A coroutine to set the scroll bar to the bottom, necesasry to do this in a coroutine
	 * so that we can wait until the end of the frame,
	 * otherwise, the scrollbar will scroll down before the new scrollbar size is calculated,
	 */
	IEnumerator scrollToBottom ()
	{
		yield return new WaitForEndOfFrame ();
		scrollrect.gameObject.SetActive (true);
		scrollrect.verticalNormalizedPosition = 0f;
	}

	void setup ()
	{
		scrollrect = scrollrectObject.GetComponent <ScrollRect> ();
	}
	// Update is called once per frame
	void Update ()
	{

	}
}
