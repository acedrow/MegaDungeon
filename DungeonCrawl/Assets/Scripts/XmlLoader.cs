using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/*
 * Defines classes to hold level data and their implementation as XML-serializable variables
 * root is a <levelData> tag followed by as many <room> tags as there are rooms in the level.
 */
[XmlRoot ("LevelData")]
public class LevelData
{
	string path;

	[XmlArray ("RoomDataList")]
	[XmlArrayItem ("RoomData")]
	public List<RoomData> rdl = new List<RoomData> ();

	//NO-ARG CONSTRUCTOR
	public LevelData ()
	{
		
	}
	//1-ARG CONSTRUCTOR
	public LevelData (string p)
	{
		path = p;
	}

	public static LevelData ReadData (string loadPath)
	{

		var serializer = new XmlSerializer (typeof(LevelData));
		using (var stream = new FileStream (loadPath, FileMode.Open)) {
			return serializer.Deserialize (stream) as LevelData;
		}

	}

	public void WriteData ()
	{
		var serializer = new XmlSerializer (typeof(LevelData));
		var stream = new FileStream (path, FileMode.Create);
		serializer.Serialize (stream, this);
		stream.Close ();
	}
}

//data class to specify XML-serializable variables to hold room data.
public class RoomData
{
	//room number, will be set in order of creation by the level generation function
	[XmlAttribute ("roomNumber")]
	public int num;
	[XmlAttribute ("x")]
	public int x;
	[XmlAttribute ("y")]
	public int y;
	//array to hold all tileData in the room, array is of a length equal to the area of the room,
	//each index corresponds to a specific tile such that index = tile.y * room.x + tile.x
	public TileData[] tileDataArray;
	//the default, empty tile object. It is defined here currently for convenience, however, in the future,
	// will need to be defined in the LevelGenerator and passed in (because different rooms may have different default tiles (floor types))
	public TileData defaultTile = new TileData ();

	//NO-ARG CONSTRUCTOR
	public RoomData ()
	{
		
	}
		
	//3-ARG CONSTRUCTOR - ints
	public RoomData (int n, int xx, int yy)
	{
		x = xx;
		y = yy;
		num = n;
		tileDataArray = new TileData[x * y];
	}

	//sets all tileData objects to the default tile, and sets their coordinates correctly.
	//should call this in levelgenerator immediately after instantiating the room
	//call from roomData() constructor?
	public void defaultAll ()
	{
		for (int xloop = 0; xloop < x; xloop++) {
			for (int yloop = 0; yloop < y; yloop++) {
				/*
				 * commented out code here throws a bug in which the coordinates (As shown by the lowercase print statements)
				 * would be set correctly in the loop, but after the loops (as shown by the all-caps print statements)
				 * would be reset to the max value. 
				 * Uncommented code works fine, but might cause issues in the future as we can't direcntly copy in the default tile,
				 * could get around that by copying in values as we need them. 
				 */ 
				//TileData dTile = defaultTile;
				//dTile.coordinate = new Vector2 (x, y);
				//tileDataArray [(y * (int)coordinate.x) + x] = dTile;
				tileDataArray [(yloop * x) + xloop] = new TileData (xloop, yloop, true);

				// Debug.Log ("tile coordinate: " + tileDataArray [(y * (int)coordinate.x) + x].coordinate);
			}
		}
		//Debug.Log ("TILE COORDINATE: " + tileDataArray [0].coordinate);
		//Debug.Log ("TILE COORDINATE: " + tileDataArray [1].coordinate);
		//Debug.Log ("TILE COORDINATE: " + tileDataArray [5].coordinate);
	}
}

public class TileData
{

	[XmlAttribute ("x")]
	public int x;
	[XmlAttribute ("y")]
	public int y;
	[XmlAttribute ("defaultTile")]
	public bool def;

	//NO ARG CONSTRUCTOR
	public TileData ()
	{
		
	}

	public TileData (int xx, int yy, bool d)
	{
		x = xx;
		y = yy;
		def = d;
	}
}

//Class to test loading in room data from an XML level file,

public class XmlLoader : MonoBehaviour
{
	//Need to link the levelData .xml file in the inspector
	public TextAsset levelDataAsset;

	void Start ()
	{
		string path = "C:\\Users\\gamebox\\Desktop\\levelDataTest.xml";
		LevelData ld = LevelData.ReadData (path);
		//ld.WriteData ();
		//loadLevel ();
		Debug.Log ("did it work?");
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
