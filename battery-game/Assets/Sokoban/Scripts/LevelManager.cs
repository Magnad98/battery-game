using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameManager gameManagerScript;
    SaveManager saveManagerScript;
    LevelBuilder levelBuilderScript;
    bool readyForInput;
    Player player;

    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        saveManagerScript = FindObjectOfType<GameManager>().GetComponent<SaveManager>();
        levelBuilderScript = FindObjectOfType<GameManager>().GetComponent<LevelBuilder>();

        levelBuilderScript.Build(saveManagerScript.LoadGame().GetCurrentLevel());
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if (moveInput.sqrMagnitude > 0.5)
        {
            if (readyForInput)
            {
                readyForInput = false;
                player.Move(moveInput);
                gameManagerScript.LoadSaveButtonUI(IsLevelComplete());
            }
        }
        else
            readyForInput = true;
    }

    bool IsLevelComplete()
    {
        Wire[] wires = FindObjectsOfType<Wire>();
        foreach (var wire in wires)
            if (!wire.onCross)
                return false;
        return true;
    }
}
