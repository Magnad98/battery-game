using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool Move(Vector2 direction) // Avoid ability to move diagonally
    {
        if (WireBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction); //Wire not blocked so move it
            // TestForOnCross();
            return true;
        }
    }

    bool WireBlocked(Vector3 position, Vector2 direction) // Wires blocked by another wires and walls
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
                return true;
            }
        }
        return false;
    }
}
