using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    JSONSaving jsonSavingScript;
    PlayerData playerData;

    string levelSceneName = "LevelScene";
    [SerializeField] LevelBuilder m_LevelBuilder;
    [SerializeField] GameObject m_NextButton;
    Player m_Player;

    bool m_ReadyForInput;

    void Start()
    {
        HandleInitialization();
        HandleSceneRestart();
    }

    void Update()
    {
        HandleMovement();
        HandleSaving();
    }

    void HandleInitialization()
    {
        jsonSavingScript = FindObjectOfType<GameManager>().GetComponent<JSONSaving>();
        playerData = new PlayerData(
            new List<Status>() { Status.unlocked, Status.unlocked, Status.unlocked, Status.unlocked, Status.unlocked, Status.locked, Status.locked },
            new List<int>() { 0, 0, 0, 0, 0, 0 }
        );
        // Debug.Log(playerData.ToString());
    }

    void HandleSceneRestart()
    {
        StartCoroutine(ResetSceneAsync());
    }

    IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(levelSceneName);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelSceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelSceneName));

        m_LevelBuilder.Build();
        m_NextButton.SetActive(false);
        m_Player = FindObjectOfType<Player>();
    }

    void HandleMovement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if (moveInput.sqrMagnitude > 0.5) // Button pressed or held
        {
            if (m_ReadyForInput)
            {
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
                m_NextButton.SetActive(IsLevelComplete());
            }
        }
        else
        {
            m_ReadyForInput = true;
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

    void HandleSaving()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            jsonSavingScript.SaveData(playerData);
            Debug.Log(playerData.ToString());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            playerData = jsonSavingScript.LoadData();
            Debug.Log(playerData.ToString());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AddBatteries(0, 0, 0, 1, 1, 0);
            Debug.Log(playerData.ToString());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerData.CompleteLevel(1);
            Debug.Log(playerData.ToString());
        }
    }

    public void AddBatteries(int NineVolt, int D, int C, int AA, int AAA, int Cell)
    {
        playerData.AddBatteries(NineVolt, D, C, AA, AAA, Cell);
    }

    public void NextLevel()
    {
        m_LevelBuilder.NextLevel();
        HandleSceneRestart();
    }
}
