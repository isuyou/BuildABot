	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type{ Wires, Resistors, Battery, Goal, None};
public enum Direction{ Up, Down, Left, Right};
public class CircuitComponent : MonoBehaviour {
	

	private int xPosition;
	private int yPosition;
	private Type type;
	public CircuitComponent(int xPosition, int yPosition, Type type){
		this.xPosition = xPosition;
		this.yPosition = yPosition;
		this.type = type;
	}

    public static List<NodePosition> adjMatrix(NodePosition position, Direction dir)
    {
        List<NodePosition> result = new List<NodePosition>();
        int xPosition = position.x;
        int yPosition = position.y;
        //Protocl to add to list in order of: Left, Up, Right, Down
        switch (dir)
        {
            //add Left, Up, Down
            case Direction.Left:
                result.Add(new NodePosition(xPosition - 2, yPosition));
                result.Add(new NodePosition(xPosition - 1, yPosition));
                result.Add(new NodePosition(xPosition - 1, yPosition + 1));
                return result;
            //add Left, Up, Right
            case Direction.Up:
                result.Add(new NodePosition(xPosition - 1, yPosition - 1));
                result.Add(new NodePosition(xPosition, yPosition - 1));
                result.Add(new NodePosition(xPosition + 1, yPosition - 1));
                return result;
            //add Up, Right, Down
            case Direction.Right:
                result.Add(new NodePosition(xPosition + 1, yPosition));
                result.Add(new NodePosition(xPosition + 2, yPosition));
                result.Add(new NodePosition(xPosition + 1, yPosition + 1));
                return result;
            //add Left, Right, Down
            case Direction.Down:
                result.Add(new NodePosition(xPosition - 1, yPosition));
                result.Add(new NodePosition(xPosition + 1, yPosition));
                result.Add(new NodePosition(xPosition, yPosition + 1));
                return result;
            
           
           
        }
        return result;
    }

    public Type getType(){
		return this.type;
	}

	public void setType(Type type){
		this.type = type;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
