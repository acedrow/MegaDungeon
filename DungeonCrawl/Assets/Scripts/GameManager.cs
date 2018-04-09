using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// this method of locking input won't work
	// consider the scenario in which a shorter method (like a move) is called.
	//followed by something longer, like an animation
	//(things would get weird if we don't lock move input during move animations)
	//and the move method terminates in the middle of the animation -
	//the lock would be released by the move method before the animation would release it.
	// all we'd need to do to change this is keep a list of locks, each with an ID,
	// so, a method requests a lock, getInputLock supplies it with a unique ID, and adds that ID to the list
	// when the method gives it's lock back, a method here checks for that ID in the list, removes it,
	// if that ID was the only value in the list (nothing else has an input lock), then change the boolean.

	//could also have multiple lock booleans, e.g. movelock, animationlock, etc.
	bool inputLock;

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
		//start by locking player input;
		inputLock = false;
	}

	public bool getInputLock ()
	{
		return inputLock;
	}

	public bool setInputLock (bool toSet)
	{
		if (toSet) { 
			inputLock = true;
			return true;
		} else {
			inputLock = false;
			return false;
		}
	}
}
