using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGenerator
{

	public static LevelData generateLevel ()
	{
		string path = "C:\\Users\\gamebox\\Desktop\\levelDataTest.xml";
		LevelData toReturn = null;
		bool updateLevel = false;

		if (updateLevel) {
			toReturn = new LevelData (path);
			RoomData r = new RoomData (1, 5, 5);
			r.defaultAll ();
			//RoomData s = new RoomData (2, 4, 6);
			//s.defaultAll ();
			toReturn.rdl.Add (r);
			//toReturn.rdl.Add (s);
			toReturn.WriteData ();

		} else {
			toReturn = new LevelData (path);
			toReturn = LevelData.ReadData (path);
		}

		return toReturn;
	}

}
