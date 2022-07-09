using UnityEngine;

public class Player : MonoBehaviour
{
    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5)
            direction.x = 0;
        else
            direction.y = 0;
        direction.Normalize();

        if (!Blocked(transform.position, direction))
        {
            transform.Translate(direction);
            return true;
        }
        return false;
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (var wall in walls)
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                return true;

        GameObject[] wires = GameObject.FindGameObjectsWithTag("Wire");
        foreach (var wire in wires)
            if (wire.transform.position.x == newPos.x && wire.transform.position.y == newPos.y)
                return !(wire.GetComponent<Wire>().Move(direction));

        return false;
    }
}
