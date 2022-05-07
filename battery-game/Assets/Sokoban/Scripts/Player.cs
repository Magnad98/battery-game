using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool Move(Vector2 direction) // Avoid ability to move diagonally
    {
        if (Mathf.Abs(direction.x) < 0.5) // Will always set one of the coordinates to 0
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize(); // Makes either x or y = 1
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] wires = GameObject.FindGameObjectsWithTag("Wire");
        foreach (var wire in wires)
        {
            if (wire.transform.position.x == newPos.x && wire.transform.position.y == newPos.y)
            {
                Wire w = wire.GetComponent<Wire>();
                if (w && w.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
}
