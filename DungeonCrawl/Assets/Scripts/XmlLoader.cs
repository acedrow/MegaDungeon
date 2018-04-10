using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/*
 * holder class for XML serializable variables, root is a <levelData> tag,
 * followed by as many <room> tags as there are rooms in the level.
 */
[XmlRoot ("levelData")]
public class levelData
{
	string path;

	[XmlArray ("roomDataList")]
	[XmlArrayItem ("roomData")]
	public List<roomData> rdl = new List<roomData> ();

	//NO-ARG CONSTRUCTOR
	public levelData ()
	{
		
	}
	//1-ARG CONSTRUCTOR
	public levelData (string p)
	{
		path = p;
		roomData r = new roomData (5, 5);
		roomData s = new roomData (4, 4);
		rdl.Add (r);
		rdl.Add (s);
	}

	public void readData ()
	{

		var serializer = new XmlSerializer (typeof(levelData));
		var stream = new FileStream (path, FileMode.Open);
		var container = serializer.Deserialize (stream) as levelData;
		stream.Close ();
	}

	public void writeData ()
	{
		var serializer = new XmlSerializer (typeof(levelData));
		var stream = new FileStream (path, FileMode.Create);
		serializer.Serialize (stream, this);
		stream.Close ();
	}
}

//data class to specify XML-serializable variables to hold room data.
public class roomData
{
	[XmlAttribute ("x")]
	public int x;
	[XmlAttribute ("y")]
	public int y;

	//NO-ARG CONSTRUCTOR
	public roomData ()
	{
		
	}

	//1-ARG CONSTRUCTOR
	public roomData (int xx, int yy)
	{
		x = xx;
		y = yy;
	}
}

//Class to test loading in room data from an XML level file,

public class XmlLoader : MonoBehaviour
{
	//Path to our test XML file for serialized reading and writing
	string path = "C:\\Users\\gamebox\\Desktop\\levelDataTest.xml";
	//Need to link the levelData .xml file in the inspector
	public TextAsset levelDataAsset;

	void Start ()
	{
		levelData ld = new levelData (path);
		ld.writeData ();
		//loadLevel ();
	}
		
	/*
	 * Just a basic read implementation, not using the XMLserializer
	 */
	public void loadLevel ()
	{

		/*
		 * How this will all work at a more complete stage:
		 * When a new game is started, we'll run the procedural generation for all levels, then do rooms for each level 
		 * (maybe will split this up so that the rooms are generated when a level is first loaded, we'll have to see)
		 * This will stay as xml until we're ready to instantiate a specific room, at which point we'll read the room data directly in
		 * from the xml and instantiate.
		 */
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml (levelDataAsset.text);
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
