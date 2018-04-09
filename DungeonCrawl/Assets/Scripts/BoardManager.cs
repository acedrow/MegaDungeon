using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class BoardManager : MonoBehaviour
{
	public GameObject gameManagerObject;
	public GameObject tileObjectPrefab;
	Room currentRoom;
	GameManager gameManager;

	Room testRoom;

	// Use this for initialization
	void Start ()
	{
		setup (); 
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void setup ()
	{
		gameManager = gameManagerObject.GetComponent <GameManager> ();
		//hardcoded stuff to create a sample room, in the future will have to pull this data from an xml layout file.
		GameObject testRoomGameObject = new GameObject ();
		Room room = testRoomGameObject.AddComponent <Room> ();
		Vector2 roomsize = new Vector2 (5, 5);
		testRoomGameObject.GetComponent <Room> ().setup (roomsize);

		currentRoom = testRoomGameObject.GetComponent <Room> ();

		instantiateRoom (currentRoom);

		/*
		 * TESTING CODE
		Vector2 testtest = new Vector2 (2, 2);
		Debug.Log ("testing getTileDir on tile: " + testtest);
		checkMove (testtest, 0);
		checkMove (testtest, 1);
		checkMove (testtest, 2);
		checkMove (testtest, 3);

		testtest = new Vector2 (4, 4);
		Debug.Log ("testing getTileDir on tile: " + testtest);
		checkMove (testtest, 0);
		checkMove (testtest, 1);
		checkMove (testtest, 2);
		checkMove (testtest, 3);
		*/

	}

	//reads data from the tileData objects of toInstantiate and instantiates them as Tile game objects on the board.
	//Should be doing this from the room class, but it's fine here until we start getting into questions of instantiating multiple rooms.
	void instantiateRoom (Room toInstantiate)
	{
		//these first few lines are taken from elsewhere, gotta parse them and see if they're necessary.
		string holderName = "generatedMap";
		if (transform.FindChild (holderName)) {
			DestroyImmediate (transform.FindChild (holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		for (int x = 0; x < currentRoom.roomsize.x; x++) {
			for (int y = 0; y < currentRoom.roomsize.y; y++) {
				Vector3 tilePosition = new Vector3 (x, y, 1);
				Vector2 tilePos2 = new Vector2 (x, y);
				//Instantiates a copy of the linked tileObject prefab, in the correct position, rotation, and sets its parent to the currentRoom
				GameObject newTile = Instantiate (tileObjectPrefab, tilePosition, Quaternion.identity, currentRoom.transform);
				//This method specifically would be a lot nicer if called from room
				newTile.GetComponent <TileObject> ().setup (currentRoom, currentRoom.getTileData (tilePos2));
				currentRoom.setTileObject (newTile, tilePos2);
			}
		}
	}

	/*
	 * Attempts to move a gameObject, checks to see if the destination tile is occupied,
	 * will need to check for hazards, compute move cost, etc
	 * 
	 */
	public bool attemptMove (GameObject charObjectToMove, int dir)
	{
		
		Vector3 start = charObjectToMove.transform.position;
		start.z = 0;
		GameObject destination = checkMove (start, dir);

		//if an invalid direction, null gameOBject is passed, or the move is invalid end.
		if (dir < 0 || dir > 3 || charObjectToMove == null || destination == null) {
			return false;
		} 
			
		charObjectToMove.transform.position = destination.transform.position;
		charObjectToMove.GetComponent <Character> ().setTileGameObject (destination);
		return true;


		//Method Layout/schema:
		//lock input
		//get character position, and destination tile.
		//If destination occupied or impassable, or wall on current tile in movdir - can't move
		//if moving through a door, handle that here.
		//compute movecost, if greater than character's remaining AP, can't move.
		//Subtract movecost from character's AP
		//update character's tile gameobject
		//Physically move the gameObject
		//update character's location, set preview tile to unoccupied, new tile to occupied.
		//Might not be doing this exactly here, but it goes at this point in the flow:
		// check if the destination tile has traps, auras, if so, resolve them.
		// also will likely be happening elsewhere, but do player perception 
		// (I'd imagine this changes w/ a move - easier to percieve closer things)
		//unlock input
	}

	//given a start point and a direction, returns the adjacent tile in that direction.
	//if the move is invalid (trying to move off the edge of the room)
	//I need to hecking change this method name.
	GameObject checkMove (Vector2 start, int dir)
	{
		Vector2 vectorToReturn;
		GameObject toReturn = null;
		if (dir < 0 || dir > 3) {
			toReturn = null;
		} 
		if (dir == 0) {
			vectorToReturn = new Vector2 (start.x, start.y + 1);
			if (currentRoom.getTileObject (vectorToReturn) != null) {
				toReturn = currentRoom.getTileObject (vectorToReturn);
			} else
				toReturn = null;
		} else if (dir == 1) {
			vectorToReturn = new Vector2 (start.x + 1, start.y);
			if (currentRoom.getTileObject (vectorToReturn) != null) {
				toReturn = currentRoom.getTileObject (vectorToReturn);
			} else
				toReturn = null;
		} else if (dir == 2) {
			vectorToReturn = new Vector2 (start.x, start.y - 1);
			if (currentRoom.getTileObject (vectorToReturn) != null) {
				toReturn = currentRoom.getTileObject (vectorToReturn);
			} else
				toReturn = null;
		} else if (dir == 3) {
			vectorToReturn = new Vector2 (start.x - 1, start.y);
			if (currentRoom.getTileObject (vectorToReturn) != null) {
				toReturn = currentRoom.getTileObject (vectorToReturn);
			} else
				toReturn = null;
		} else {
			toReturn = null;
		}

		/* TESTING CODE
		 * 
		string direction = "";
		if (dir == 0)
			direction = "north";
		else if (dir == 1)
			direction = "east";
		else if (dir == 2)
			direction = "south";
		else if (dir == 3)
			direction = "west";
		else
			direction = "dir out of bounds";

		string result = "";
		if (toReturn == null) {
			result = "null";
		} else {
			result = toReturn.GetComponent <TileObject> ().getCoordinates ().ToString ();
		}
		Debug.Log ("GetTile " + direction + " called on: " + start + ", result: " + result);
		*/
		return toReturn; 
	}
}