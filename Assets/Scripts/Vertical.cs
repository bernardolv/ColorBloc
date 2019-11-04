using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vertical : MonoBehaviour{

	public enum Type {Color, Wall, Spikes, Portal, Unknown};

	public enum Color {Yellow, Red, Blue, Orange, Purple, Green, Unknown};
	
	//type of Vertical
	public Type type;

	//color of Vertical
	public Color color;

	//Location in array
	public Vector2 gridPosition;

	//Location in world
	public Vector2 worldPosition;

	// public Vertical(string nType, string nColor){
	// 	type = (Type)System.Enum.Parse(typeof(Type), nType);
	// 	color = (Color)System.Enum.Parse(typeof(Color), nColor);
	// }

	void Awake(){

	}
}
