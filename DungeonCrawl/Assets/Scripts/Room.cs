using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Monobehavior room class to be instantiated.
 */

public class Room : MonoBehaviour
{
	//coordinate point indicating the size of the room, x by y
	public Vector2 roomsize;
	//array to hold tileData objects for this room, this will likely be hardcoded and brought in from a levelData-thingy
	public TileDataOriginal[,] tileData;
	//array to hold instantiated tileObjects
	public GameObject[,] tileObject;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	public void setup (Vector2 rs)
	{
		roomsize = rs;
		tileData = new TileDataOriginal[(int)rs.x, (int)rs.y];
		tileObject = new GameObject[(int)rs.x, (int)rs.y];
		setupTiles ();
	}

	/* Method to properly set data values in the dataTile objects.
	 * Needs to be called before the room can be instantiated
	 * This method sets edgeFeature values to 1 .
	 * currently takes no arguments and sets all floor types to stone (default)
	 */

	public void setupTiles ()
	{
		for (int x = 0; x < roomsize.x; x++) {
			for (int y = 0; y < roomsize.y; y++) {
				TileDataOriginal toAddTile = new TileDataOriginal (new Vector2 (x, y));

				//if we're on the bottom row, set south wall
				if (x == 0) {
					toAddTile.setEdgeFeature (2, TileDataOriginal.EDGE_FEATURE_WALL);
				}
				//if left colum, set west wall
				if (y == 0) {
					toAddTile.setEdgeFeature (3, TileDataOriginal.EDGE_FEATURE_WALL);
				}
				//if top row, set north wall
				if (x == roomsize.x - 1) {
					toAddTile.setEdgeFeature (0, TileDataOriginal.EDGE_FEATURE_WALL);
				}
				//if right column, set east wall
				if (y == roomsize.y - 1) {
					toAddTile.setEdgeFeature (1, TileDataOriginal.EDGE_FEATURE_WALL);
				}

				tileData [x, y] = toAddTile;
			}
		}
	}

	/*
	 * Method to add an instantiated tile gameObject to the tileObject array
	 */
	public bool setTileObject (GameObject toAdd, Vector2 pos)
	{
		if (pos.x < 0 || pos.x > roomsize.x - 1 || pos.y < 0 || pos.y > roomsize.y - 1) {
			return false;
		} else {
			tileObject [(int)pos.x, (int)pos.y] = toAdd;
			return true;
		}
	}

	public GameObject getTileObject (Vector2 pos)
	{ 
		if (pos.x < 0 || pos.x > roomsize.x - 1 || pos.y < 0 || pos.y > roomsize.y - 1) {
			return null;
		} else {
			return tileObject [(int)pos.x, (int)pos.y];
		}
	}

	public TileDataOriginal getTileData (Vector2 pos)
	{
		if (pos.x < 0 || pos.x > roomsize.x - 1 || pos.y < 0 || pos.y > roomsize.y - 1) {
			return null;
		} else {
			return tileData [(int)pos.x, (int)pos.y];
		}
	}
}
