using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool onCross;
    public bool Move(Vector2 direction)
    {
        if (!WireBlocked(transform.position, direction))
        {
            transform.Translate(direction);
            TestForOnCross();
            return true;
        }
        return false;
    }

    bool WireBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                return true;

        GameObject[] wires = GameObject.FindGameObjectsWithTag("Wire");
        foreach (var wire in wires)
            if (wire.transform.position.x == newPos.x && wire.transform.position.y == newPos.y)
                return true;

        return false;
    }

    void TestForOnCross()
    {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");

        foreach (var cross in crosses)
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                onCross = true;
                return;
            }

        GetComponent<SpriteRenderer>().color = Color.white;
        onCross = false;
    }
}
