using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePath
{
	TileObject startTile;
	TileObject endTile;
	double distance;

	public TilePath ()
	{
		
	}

	public TilePath (TileObject st, TileObject et)
	{
		startTile = st;
		endTile = et;
	}

	public TileObject getStartTile ()
	{
		return startTile;
	}

	public TileObject getEndTile ()
	{
		return endTile;
	}





}
