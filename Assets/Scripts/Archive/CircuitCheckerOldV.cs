using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CircuitCheckerOldV : MonoBehaviour {

    CircuitComponent circuitComponent;
    private Direction[][] directionSequence = {
    new Direction[] {Direction.Left, Direction.Up, Direction.Down},
    new Direction[] {Direction.Left, Direction.Up, Direction.Right},
    new Direction[] {Direction.Up, Direction.Right, Direction.Down},
    new Direction[] {Direction.Left, Direction.Right, Direction.Down}
};

    //Search variables
    HashSet<NodePosition> discovered;
    private Stack<NodePosition> positionStack;
    private Stack<Direction> directionStack;
    private bool shortCircuit;
    private bool finished;
    private Stack<NodePosition> path;

    //keeps track of number of goals reached/needed to be reached



    // Use this for initialization
    void Start () {
        circuitComponent = GetComponent<CircuitComponent>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void checkCircuit(NodePosition sourcePosition, ref Type[,] connections, Direction direction, int goalCount)
    {
        discovered = new HashSet<NodePosition>();
        discovered.Add(sourcePosition);
        checkForwardCircuits(sourcePosition, ref connections, direction, ref discovered);
        positionStack = new Stack<NodePosition>();
        directionStack = new Stack<Direction>();
    }

    bool checkForwardCircuits(NodePosition sourcePosition, ref Type[,] connections, Direction initialDirection, ref HashSet<NodePosition> discovered)
    {
        //NOTE: adjacentConnectors must start at lenght of 3; conditionals and loops assume the initial size to be 3
        List<NodePosition> adjacentConnectors;
        path = new Stack<NodePosition>();
        Direction direction = initialDirection;
        Direction currentDirection = initialDirection;
        NodePosition currentPosition = sourcePosition;
        int x = 0;
        int conditional = 0;
        shortCircuit = false;
        bool finished = false;

        //checks if children made a correct connection to goal or not
        bool notConnected = false;

        //begin initializing Stack search case with first source node
        positionStack.Push(sourcePosition);
        directionStack.Push(direction);
        while ((positionStack.Count >0) && (!finished))
        {
            adjacentConnectors = CircuitComponent.adjMatrix(currentPosition, direction);
            initialDirection = directionSequence[(int)direction][x];
            switch (direction)
            {

                case Direction.Left:
                    foreach (NodePosition nextPosition in adjacentConnectors)
                    {
                        conditional = validateConnections(nextPosition, ref connections, direction, ref discovered);
                        if (conditional % 2 == 0)
                        {
                            if (conditional == 0)
                            {
                               notConnected = false;
                                
                            }
                            else
                            {
                                notConnected = false;
                                path.Push(nextPosition);
                                positionStack.Push(nextPosition);
                                directionStack.Push(direction);
                                discovered.Add(nextPosition);
                            }
                        }
                        else
                        {
                            if(conditional < 0)
                            {
                                shortCircuit = true;
                                finished = true;
                            }
                            else
                            {
                                //nextPosition already added to discovered in validateConnections() method (only if it wasn't already in the discovered set)
                            }

                        }
                        x++;
                    }
                    break;
                case Direction.Up:
                    break;
                case Direction.Right:
                    break;
                case Direction.Down:

                    break;
            }
            if (notConnected)
            {
                path.Pop();
            }
            
        }

        return false;

    }

    /***returns protocol for type of connections found
     * Even = good connections
     * Odd = no connection or error
     * -1 = short circuit
     * 0 = connected to lightbulb
     * */
    int validateConnections(NodePosition sourcePosition, ref Type[,] connections, Direction direction, ref HashSet<NodePosition> discovered)
    {
        if(discovered.Contains(sourcePosition))
            return 1;
        //checks if position in bounds of the circuitBoard
        if((sourcePosition.x < connections.GetLength(0) && sourcePosition.x >= 0) && (sourcePosition.y < connections.GetLength(1) && sourcePosition.y >= 0))
        {
            if (connections[sourcePosition.x, sourcePosition.y] == Type.Goal)
                return 0;
            else if (connections[sourcePosition.x, sourcePosition.y] == Type.Wires)
                return 2;
            else if (connections[sourcePosition.x, sourcePosition.y] == Type.Battery)
                return -1;
            else
            {
                discovered.Add(sourcePosition);
                return 1;
            }
        }
        return 1;
    }

    //checks if another connection can be made backwards to the battery without overlapping with original path to goal
    bool checkBackwardCircuitsStart(NodePosition sourcePosition, ref Type[,] connections, Direction direction, ref HashSet<NodePosition> discovered)
    {
        return false;
    }
}
