using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool m_OnCross; // true if wire has been pushed on to a cross
    public bool Move(Vector2 direction) // Avoid ability to move diagonally
    {
        if (WireBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction); //Wire not blocked so move it
            TestForOnCross();
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

    void TestForOnCross()
    {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        foreach (var cross in crosses)
        {
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y)
            {
                //On a cross
                GetComponent<SpriteRenderer>().color = Color.green;
                m_OnCross = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnCross = false;
    }
}
