﻿using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour {

	public Camera myCamera;
	
	private GameObject parent;
	private StarDisplay starDisplay;
	
	// Use this for initialization
	void Start () {
		parent = GameObject.Find("Defenders");
		starDisplay = GameObject.FindObjectOfType<StarDisplay>();
		
		if (!parent) {
			parent = new GameObject("Defenders");
		}
	}
	
	void OnMouseDown() {
		Vector2 rawPosition = CalculateWorldPointOfMouseClick();
		Vector2 roundedPosition = SnapToGrid(rawPosition);
		GameObject defender = Button.selectedDefender;
		
		int defenderCost = defender.GetComponent<Defender>().starCost;
		
		if (starDisplay.UseStars(defenderCost) == StarDisplay.Status.SUCCESS) {
			SpawnDefender(roundedPosition, defender);
		} else {
			Debug.Log("Insufficient stars to spawn");
		}
	}
	

	void SpawnDefender (Vector2 roundedPosition, GameObject defender)
	{
		Quaternion zeroRotation = Quaternion.identity;
		GameObject newDefender = Instantiate (defender, roundedPosition, zeroRotation) as GameObject;
		newDefender.transform.parent = parent.transform;
	}
	
	Vector2 SnapToGrid(Vector2 rawWorldPosition) {
		float newX = Mathf.RoundToInt(rawWorldPosition.x);
		float newY = Mathf.RoundToInt(rawWorldPosition.y);
		
		return new Vector2(newX, newY);
	}
	
	Vector2 CalculateWorldPointOfMouseClick() {
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		float distanceFromCamera = 10f;
		
		Vector3 weirdTriplet = new Vector3(mouseX, mouseY, distanceFromCamera);
		Vector2 worldPosition = myCamera.ScreenToWorldPoint(weirdTriplet);
		
		return worldPosition;
	}
}
