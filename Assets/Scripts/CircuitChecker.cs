using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CircuitChecker : MonoBehaviour {

    CircuitComponent circuitComponent;
    private Direction[][] directionSequence = {
    new Direction[] {Direction.Left, Direction.Up, Direction.Down},
    new Direction[] {Direction.Left, Direction.Up, Direction.Right},
    new Direction[] {Direction.Up, Direction.Right, Direction.Down},
    new Direction[] {Direction.Left, Direction.Right, Direction.Down}
};

    //Search variables for stack w/ while loop usage
    HashSet<NodePosition> discovered;
    private Stack<NodePosition> path;
    private Stack<NodePosition> validPath;
    private bool shortCircuit;
    private bool finished;

    //keeps track of number of goals reached/needed to be reached
    int numGoals;


    // Use this for initialization
    void Start () {
        circuitComponent = GetComponent<CircuitComponent>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void checkCircuit(NodePosition sourcePosition, ref Type[,] connections, Direction direction, int goalCount)
    {
        discovered = new HashSet<NodePosition>();
        discovered.Add(sourcePosition);
        path = new Stack<NodePosition>();
        numGoals = goalCount;
        checkForwardCircuits(sourcePosition, ref connections, direction, ref discovered);
    }

    bool checkForwardCircuits(NodePosition sourcePosition, ref Type[,] connections, Direction initialDirection, ref HashSet<NodePosition> discovered)
    {
        shortCircuit = false;
        bool finished = false;
        validPath = new Stack<NodePosition>();

        //checks if children made a correct connection to goal or not
        bool notConnected = false;

        //begin initializing Stack search case with first source node


        List<NodePosition> adjacentConnectors = CircuitComponent.adjMatrix(sourcePosition, initialDirection);

        switch (initialDirection)
        {

            case Direction.Left:
                
                
                break;
            case Direction.Up:
                break;
            case Direction.Right:
                break;
            case Direction.Down:

                break;
        }
        
        return false;

    }

    void searchNodes(List<NodePosition> adjacentConnectors, Type[,] connections, Direction initialDirection, ref HashSet<NodePosition> discovered)
    {
        int x = 0;
        int conditional = 0;
        Direction direction;
        foreach (NodePosition nextPosition in adjacentConnectors)
        {
            direction = directionSequence[(int)initialDirection][x];
            conditional = validateConnections(nextPosition, ref connections, direction, ref discovered);

            if (conditional % 2 == 0)
            {
                if (conditional == 0)
                {
                    if(checkBackwardCircuitsStart(nextPosition, ref connections, direction, ref discovered))
                    {
                        validPath.Push(nextPosition);
                    }
                }
                else
                {
                    path.Push(nextPosition);
                    discovered.Add(nextPosition);
                    checkForwardCircuits(nextPosition, ref connections, direction, ref discovered);
                }
            }
            else
            {
                if (conditional < 0)
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

        //if no valid path is found, add current node to discovered list
        discovered.Add(path.Pop());
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
