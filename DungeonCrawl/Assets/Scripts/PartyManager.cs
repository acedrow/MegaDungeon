using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
	public GameObject boardManagerObject;
	public GameObject gameManagerObject;
	//only using this for testing, shouldn't be public
	public GameObject activeCharacterObject;

	GameObject[] party;

	BoardManager boardManager;
	GameManager gameManager;
	Character activeCharacter;

	int partySize;

	void Start ()
	{
		setup ();
	}
	
	// Update is called once per frame - handle input here.
	void Update ()
	{
		if (!gameManager.getInputLock ()) {
			if (Input.GetKeyDown ("w")) {
				boardManager.attemptMove (activeCharacterObject, 0);
			}
			if (Input.GetKeyDown ("d")) {
				boardManager.attemptMove (activeCharacterObject, 1);
			}
			if (Input.GetKeyDown ("s")) {
				boardManager.attemptMove (activeCharacterObject, 2);
			}
			if (Input.GetKeyDown ("a")) {
				boardManager.attemptMove (activeCharacterObject, 3);
			}
		}
	}

	//does constructor things.
	void setup ()
	{
		//assign script instances from object instances for boardManager, gameManager, activeCharacter, etc.
		boardManager = boardManagerObject.GetComponent <BoardManager> ();
		gameManager = gameManagerObject.GetComponent <GameManager> ();
		//only doing this for testing
		//activeCharacter = activeCharacterObject.GetComponent <Character> ();

	}
}
