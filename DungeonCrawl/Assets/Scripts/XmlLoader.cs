using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

//Class to test loading in room data from an XML level file,

public class XmlLoader : MonoBehaviour
{
	//Need to link the levelData .xml file in the inspector
	public TextAsset levelData;

	void Start ()
	{
		loadLevel ();
	}

	public void loadLevel ()
	{
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml (levelData.text);
		XmlNodeList roomList = xmldoc.GetElementsByTagName ("room");
		Debug.Log (roomList.Count);

		XmlElement e = roomList.Item (0) as XmlElement;
		string size = e.GetAttribute ("size");
		Debug.Log (size);

		foreach (XmlNode roomNode in roomList) {
			
			Debug.Log (roomNode.Name);

		}
	}

}
