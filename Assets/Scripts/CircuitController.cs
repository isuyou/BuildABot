using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitController : MonoBehaviour {
	public Button button;
	public bool win = false;
	public int lightBulbPass = 0;
	public int numberOfLightBulb = 2;
	public static CircuitController controller;
	public GameObject vertBulbPrefab;
	public GameObject horiBulbPrefab;
	public GameObject resistorPrefab;
	public GameObject batteryPrefab;
	public NodePosition BatteryFirst;
	public NodePosition BatterySecond;
	public NodePosition[] resistorFirst;
	public NodePosition[] resistorSecond;
	public NodePosition[] bulbFirst;
	public NodePosition[] bulbSecond;
	public NodePosition firstPoint, secondPoint;
	public bool released = true;
	public GameObject wirePrefab;
	public int xSize = 8;
	public int ySize = 5;
	public Type[,] connections;
	public GameObject[,] node;
	public bool dragging;
	public GameObject[,] saveConnection;
	public Text resultText;
	private Direction[][] direction = {
		new Direction[] {Direction.Left, Direction.Up, Direction.Down},  //left
		new Direction[] {Direction.Left, Direction.Up, Direction.Right},   // up
		new Direction[] {Direction.Up, Direction.Right, Direction.Down},   // right
		new Direction[] {Direction.Left, Direction.Right, Direction.Down}  // down
	};

	// Use this for initialization
	void Start () {
		controller = new CircuitController ();
		bulbFirst = new NodePosition[2]{new NodePosition(3,1), new NodePosition(7,2)}; // Input ourselve
		bulbSecond = new NodePosition[2]{new NodePosition(4,1), new NodePosition(7,3)}; // Input ourselve
		resistorFirst = new NodePosition[4]{new NodePosition(2,0),new NodePosition(2,1), new NodePosition(2,2), new NodePosition(3,2)}; // Input ourselve
		resistorSecond = new NodePosition[4]{new NodePosition(2,1), new NodePosition(2,2), new NodePosition(3,2), new NodePosition(4,2)}; // Input ourselve
		BatteryFirst = new NodePosition(0,1);
		BatterySecond = new NodePosition (0	, 2);
		saveConnection = new GameObject[xSize * 2 + 1, ySize + 1];
		dragging = false;
		node = new GameObject[xSize, ySize];
		for(int j = 0; j < ySize; j++){
			for(int i = 0; i < xSize; i++){
				node [i, j] = GameObject.Find("Node(" + i + "," + j + ")");
			}
		}

		connections = new Type [xSize * 2 + 1, ySize + 1] ;
		for (int i = 0; i <= xSize * 2; i += 2) {
			for (int j = 0; j <= ySize - 1; j++) {
				saveConnection [i, j] = null;
				connections [i, j] = Type.None;
			}
		}
		for (int i = 1; i <= xSize * 2; i += 2) {
			for (int j = 0; j <= ySize ; j++) {
				saveConnection [i, j] = null;
				connections [i, j] = Type.None;
			}
		}

		for (int i = 0; i < 2; i++) {
			InstanceBulb (bulbFirst [i], bulbSecond [i]);
		}

		for (int i = 0; i < 4; i++) {
			InstanceResistor (resistorFirst [i], resistorSecond [i]);
		}
		InstanceBattery (BatteryFirst, BatterySecond);
		button.onClick.AddListener (runAlgorithm);
	}

	void InstanceResistor(NodePosition firstPoint, NodePosition secondPoint){
		int xCord = firstPoint.x + secondPoint.x + 1;
		int yCord = Mathf.Max (firstPoint.y, secondPoint.y);
		float xPosition = (node [firstPoint.x, firstPoint.y].transform.position.x + node [secondPoint.x, secondPoint.y].transform.position.x) / 2;
		float yPosition = (node [firstPoint.x, firstPoint.y].transform.position.y + node [secondPoint.x, secondPoint.y].transform.position.y) / 2;
		connections [xCord, yCord] = Type.Battery;
		Vector3 position = new Vector3 (xPosition, yPosition);
		if (xCord % 2 == 0) {
			Instantiate (resistorPrefab, position, Quaternion.Euler(0,0,90));
		}
		else {
			Instantiate (resistorPrefab, position, Quaternion.identity);
		}
		connections [xCord, yCord] = Type.Resistors;
	}
	void InstanceBattery(NodePosition firstPoint, NodePosition secondPoint){
		int xCord = firstPoint.x + secondPoint.x + 1;
		int yCord = Mathf.Max (firstPoint.y, secondPoint.y);
		float xPosition = (node [firstPoint.x, firstPoint.y].transform.position.x + node [secondPoint.x, secondPoint.y].transform.position.x) / 2;
		float yPosition = (node [firstPoint.x, firstPoint.y].transform.position.y + node [secondPoint.x, secondPoint.y].transform.position.y) / 2;
		connections [xCord, yCord] = Type.Battery;
		Vector3 position = new Vector3 (xPosition, yPosition);
		if (xCord % 2 == 0) {
			Instantiate (batteryPrefab, position, Quaternion.Euler(0,0,90));
		}
		else {
			Instantiate (batteryPrefab, position, Quaternion.identity);
		}
		connections [xCord, yCord] = Type.Battery;
	}
	void InstanceBulb(NodePosition firstPoint, NodePosition secondPoint){
		int xCord = firstPoint.x + secondPoint.x + 1;
		int yCord = Mathf.Max (firstPoint.y, secondPoint.y);
		float xPosition = (node [firstPoint.x, firstPoint.y].transform.position.x + node [secondPoint.x, secondPoint.y].transform.position.x) / 2;
		float yPosition = (node [firstPoint.x, firstPoint.y].transform.position.y + node [secondPoint.x, secondPoint.y].transform.position.y) / 2;
		connections [xCord, yCord] = Type.Goal;
		Vector3 position = new Vector3 (xPosition, yPosition);
		if (xCord % 2 == 0) {
			saveConnection[xCord, yCord] = Instantiate (horiBulbPrefab, position, Quaternion.identity);
		} else {
			saveConnection[xCord, yCord] = Instantiate (vertBulbPrefab, position, Quaternion.identity);
		}
	}


	// Create a wire have 2 end at the node 
	void InstanceWire(NodePosition firstPoint, NodePosition secondPoint){
		int xCord = firstPoint.x + secondPoint.x + 1;
		int yCord = Mathf.Max (firstPoint.y, secondPoint.y);
		float xPosition = (node [firstPoint.x, firstPoint.y].transform.position.x + node [secondPoint.x, secondPoint.y].transform.position.x) / 2;
		float yPosition = (node [firstPoint.x, firstPoint.y].transform.position.y + node [secondPoint.x, secondPoint.y].transform.position.y) / 2;
		if (connections [xCord, yCord] == Type.None || connections [xCord, yCord] == Type.Wires) {
			if (saveConnection [xCord, yCord] == null) {
				connections [xCord, yCord] = Type.Wires;
				Quaternion rotation;
				Vector3 postion = new Vector3 (xPosition, yPosition);
				if (xCord % 2 == 0) {
					rotation = Quaternion.Euler (0, 0, 90);
				} else {
					rotation = Quaternion.identity;
				}
				saveConnection [xCord, yCord] = Instantiate (wirePrefab, postion, rotation);
			} else {
				connections [xCord, yCord] = Type.None;
				Destroy(saveConnection [xCord, yCord]);
				saveConnection [xCord, yCord] = null;
			}
		}
	}

	bool checkLegitPoint(NodePosition position){
		int wireCount = 0;
		if (connections [position.x * 2, position.y] != Type.None && connections [position.x * 2, position.y] != Type.Resistors)
			wireCount++;
		if (connections [position.x * 2 + 1, position.y] != Type.None && connections [position.x * 2 + 1, position.y] != Type.Resistors)
			wireCount++;
		if (connections [position.x * 2 + 2, position.y] != Type.None && connections [position.x * 2 + 2, position.y] != Type.Resistors)
			wireCount++;
		if (connections [position.x * 2 + 1, position.y + 1] != Type.None && connections [position.x * 2 + 1, position.y + 1] != Type.Resistors)
			wireCount++;
		if (wireCount == 2)
			return false;
		else
			return true;
	}

	void Dfs(NodePosition position, Direction dir){
		List<NodePosition> list = CircuitComponent.adjMatrix (position, dir);
		int dirRep = 0;
		switch(dir){
		case Direction.Left: 
			dirRep = 0;
			break;
		case Direction.Up: 
			dirRep = 1;
			break;
		case Direction.Right: 
			dirRep = 2;
			break;
		case Direction.Down: 
			dirRep = 3;
			break;
		}
		int count = 0;
		foreach(NodePosition pos in list){
			if(connections[pos.x,pos.y] == Type.Wires){
				Dfs (pos, direction [dirRep][count]);
				break;
			} else if ( connections[pos.x,pos.y] == Type.Goal){
				lightBulbPass++;
				Dfs (pos, direction [dirRep][count]);
				break;
			} else if ( connections[pos.x,pos.y] == Type.Battery){
				if(lightBulbPass >= numberOfLightBulb){
					Debug.Log (lightBulbPass);
					win = true;
				}
				else {
					win = false;
				}
			} else if (count <= 2){ count++;}
				
		}
	}


	void runAlgorithm(){
		win = false;
		lightBulbPass = 0;
		Direction dir = Direction.Up;
		switch ((BatteryFirst.x - BatterySecond.x) * 3 + (BatteryFirst.y - BatterySecond.y) * 4) {
		case 4:
			dir = Direction.Down;
			break;
		case 3:
			dir = Direction.Right;
			break;
		case -3:
			dir = Direction.Left;
			break;
		case -4:
			dir = Direction.Up;
			break;
		}
		Dfs(new NodePosition(BatteryFirst.x + BatterySecond.x + 1, Mathf.Max(BatteryFirst.y,BatterySecond.y)), dir);
		if (win) {
			CircuitController.controller.win = true;
			resultText.text = "Good job! You win!";
			Debug.Log (lightBulbPass);

		} else {
			resultText.text = "Try harder! You fail to connect the line";
		}
	}

		

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (!dragging) {
				released = false;
				Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				for (int i = 0; i < xSize; i++) {
					for (int j = 0; j < ySize; j++) {
						if (node [i, j].GetComponent<Collider2D> ().OverlapPoint (wp)) {
								firstPoint = new NodePosition (node [i, j].GetComponent<CircuitNode> ().x, node [i, j].GetComponent<CircuitNode> ().y);
								resultText.text = "Choose second point";
								dragging = true;
						}
					}
				}
			}
		}
		if (!Input.GetMouseButtonDown (0)) {
			released = true;
		}
		if (Input.GetMouseButtonDown (0)){
			if (dragging && released) {
				dragging = false;
				Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				for (int i = 0; i < xSize; i++) {
					for (int j = 0; j < ySize; j++) {
						if (node [i, j].GetComponent<Collider2D> ().OverlapPoint (wp)) {
								secondPoint = new NodePosition (node [i, j].GetComponent<CircuitNode> ().x, node [i, j].GetComponent<CircuitNode> ().y);
								resultText.text = "Choose the first point";
						}
					}
				}
				if (connections [firstPoint.x + secondPoint.x + 1, Mathf.Max (firstPoint.y, secondPoint.y)] == Type.Wires ||(checkLegitPoint(firstPoint) && (checkLegitPoint(secondPoint)) && firstPoint != null && secondPoint != null && !(firstPoint.x == secondPoint.x && firstPoint.y == secondPoint.y) && (Mathf.Abs (firstPoint.x - secondPoint.x) + Mathf.Abs (secondPoint.y - firstPoint.y) < 2))) {
					InstanceWire (firstPoint, secondPoint);
				} else {
					resultText.text = "Not allowed !! Choose new pair of points";
				}

				dragging = false;
				firstPoint = null;
				secondPoint = null;
			}
		}
	}


		
}
