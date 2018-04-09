using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Non-monobehavior tile class to hold tile data. 
 *TileData objects aren't instantiated themselves, 
 *but instead will be copied over to TileObject objects to be instantiated in boardmanager.
 *
 *applicable constants and types (e.g. floortype enum) should be declared here and 
 *copied in Tile, not vice-versa.
 */

public class TileData
{
	//CONSTANTS:
	//the maximum value for an edge feature
	public static int MAX_EDGE_FEATURE_VALUE = 2;
	//the int value corresponding to a wall edge feature
	public static int EDGE_FEATURE_WALL = 1;
	//the int value corresponding to a door edge feature
	public static int EDGE_FEATURE_DOOR = 2;

	//Enumeration to indicate type of floor.
	public enum tileFloorType
	{
		stone,
		wood}
	;

	Vector2 coordinate;
	int[] edgeFeature;
	tileFloorType floorType;

	//true if the tile contains objects or characters.
	bool occupied;
	//true unless the tile contains impassable terrain feature.
	bool passable;

	//NO ARG CONSTRUCTOR
	public TileData ()
	{
		//Floor type detault is stone.
		floorType = tileFloorType.stone;
		//x,y coordinate value of the tile within the room
		coordinate = new Vector2 (0, 0);
		//array of integers to indicate objects occupying the edges of the tile -
		//a value of 1 indicates a door, a value of 2, a wall.
		//the 4 indicies of the array correspond to each edge of the wall,
		//from north clockwise: [0] is north, [1] east, [2] south, [3] west
		edgeFeature = new int[4];
	}

	public TileData (Vector2 c)
	{
		floorType = tileFloorType.stone;
		coordinate = c;
		edgeFeature = new int[4];
	}

	public bool setEdgeFeature (int[] e)
	{
		if (e.Length != 4) {
			return false;
		}
		foreach (int i in e) {
			if (i > MAX_EDGE_FEATURE_VALUE || i < 0) {
				return false;
			}
		}
		edgeFeature = e;
		return true;
	}

	public bool setEdgeFeature (int index, int value)
	{
		if (index > 3 || index < 0) {
			return false;
		} else if (value > MAX_EDGE_FEATURE_VALUE || value < 0) {
			return false;
		} else {
			edgeFeature [index] = value;
			return true;
		}


	}

	public Vector2 getCoordinates ()
	{
		return coordinate;
	}

	public int getEdgeFeature (int index)
	{
		return edgeFeature [index];
	}

	public tileFloorType getTileFloorType ()
	{
		return floorType;
	}

	public bool getOccupied ()
	{
		return occupied;
	}

	public bool getPassable ()
	{
		return passable;
	}
}