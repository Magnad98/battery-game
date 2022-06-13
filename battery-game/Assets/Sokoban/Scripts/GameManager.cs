using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    bool m_ReadyForInput;
    Player m_Player;

    string levelSceneName = "LevelScene";


    void Start()
    {
        ResetScene();
    }
    void Update()
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

    public void NextLevel()
    {
        m_LevelBuilder.NextLevel();
        ResetScene();
    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsync());
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
        m_Player = FindObjectOfType<Player>();
        m_NextButton.SetActive(false);
    }
}
