using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameManager gameManagerScript;
    SaveManager saveManagerScript;
    LevelBuilder levelBuilderScript;
    string levelSceneName;
    bool readyForInput;
    Player player;

    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        saveManagerScript = FindObjectOfType<GameManager>().GetComponent<SaveManager>();
        levelBuilderScript = FindObjectOfType<GameManager>().GetComponent<LevelBuilder>();
        levelSceneName = "LevelScene";

        levelBuilderScript.Build(saveManagerScript.LoadGame().GetCurrentLevel());
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if (moveInput.sqrMagnitude > 0.5) // Button pressed or held
        {
            if (readyForInput)
            {
                readyForInput = false;
                player.Move(moveInput);
                gameManagerScript.ActivateSaveButton(IsLevelComplete());
            }
        }
        else
        {
            readyForInput = true;
        }
    }

    bool IsLevelComplete()
    {
        Wire[] wires = FindObjectsOfType<Wire>();
        foreach (var wire in wires)
        {
            if (!wire.m_OnCross)
            {
                return false;
            }
        }
        return true;
    }
}
