using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Monobehavior tile class to be attached to all tile gameObjects.
 * Does things like mouseover, collision tracking, etc
 * Only holds data immediately relevant to an instantiated tile - i.e. relevant to the game in an immediate state, 
 * purely game-centric data (walls, traps, hazards, etc) is all tracked in the linked tileData object
 * 
 */

public class TileObject : MonoBehaviour
{
	TileData tileData;
	Vector2 coordinate;
	Room parentRoom;

	// Use this for initialization
	void Start ()
	{
		
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0))
			Debug.Log ("tile " + tileData.getCoordinates ().x + "," + tileData.getCoordinates ().y + "left click.");
		

		if (Input.GetMouseButtonDown (1))
			Debug.Log ("Pressed right click.");
	}

	//MAKE SURE TO CALL WHEN THE TILE IS INSTANTIATED
	public void setup (Room parent, TileData t)
	{
		tileData = t;
		parentRoom = parent;
	}

	public bool getOccupied ()
	{
		return tileData.getOccupied ();
	}

	/*
	 * Getter methods for TileObject are just an interface for corresponding methods from the Tile dataclass.
	 * Should probably actually write an interface for this. 
	 */

	public Vector2 getCoordinates ()
	{
		return tileData.getCoordinates ();
	}

	public int getEdgeFeature (int index)
	{
		return tileData.getEdgeFeature (index);
	}

	public TileData.tileFloorType getTileFloorType ()
	{
		return tileData.getTileFloorType ();
	}

	public TileData getTileData ()
	{
		return tileData;
	}
		
}
