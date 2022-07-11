using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Scripts
    SaveManager saveManagerScript;
    LevelManager levelManager;

    //UI Elements
    [SerializeField] GameObject backgroundUI, buttonsUI, recycleUI, recycleButton, saveButton;
    [SerializeField] GameObject[] levelButtons;

    //Prefabs
    [SerializeField] GameObject pusherManagerPrefab;

    //Variables
    GameObject pusherInstance;
    PlayerData playerData;
    string activeScene;

    //Methods
    void Start()
    {
        saveManagerScript = FindObjectOfType<GameManager>().GetComponent<SaveManager>();
        activeScene = SceneManager.GetActiveScene().name;

        switch (activeScene)
        {
            case "Map":
                {
                    if (System.IO.File.Exists(saveManagerScript.GetSaveGamePath()))
                    {
                        playerData = saveManagerScript.LoadGame();
                        LoadRecycleAndLevelButtonsUI();
                    }
                    break;
                }
            case "MainScene": { levelManager = FindObjectOfType<GameManager>().GetComponent<LevelManager>(); break; }
            default: break;
        }
    }

    void LoadRecycleAndLevelButtonsUI()
    {
        recycleButton.SetActive(true);

        List<Status> statuses = playerData.GetStatuses();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(true);
            levelButtons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
            switch (statuses[i])
            {
                case Status.unlocked: { levelButtons[i].GetComponent<Button>().interactable = true; break; }
                case Status.completed:
                    {
                        levelButtons[i].GetComponent<Button>().interactable = true;
                        levelButtons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(0.72f, 0.88f, 0.98f);
                        break;
                    }
                default: { levelButtons[i].GetComponent<Button>().interactable = false; break; }
            }
        }
    }

    //Menu
    public void LoadMap() { SceneManager.LoadScene("Map"); }

    public void ExitGame() { Application.Quit(); }

    //Map
    public void LoadMenu() { SceneManager.LoadScene("MainMenu"); }

    public void NewGame()
    {
        playerData = saveManagerScript.NewGame();
        LoadRecycleAndLevelButtonsUI();
    }

    public void LoadRecycleUI()
    {
        backgroundUI.SetActive(false);
        buttonsUI.SetActive(false);
        recycleUI.SetActive(true);
        pusherInstance = Instantiate(pusherManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void AddBatteries(int NineVolt, int D, int C, int AA, int AAA, int Cell)
    {
        Debug.Log(playerData.AddBatteries(NineVolt, D, C, AA, AAA, Cell));
        saveManagerScript.SaveGame(playerData);
    }

    public void CloseRecycleUI()
    {
        pusherInstance.GetComponent<PusherManager>().pusher.DisconnectAsync();
        Destroy(pusherInstance);
        LoadMap();
    }

    public void LoadLevel(int levelID)
    {
        playerData.SetCurrentLevel(levelID - 1);
        saveManagerScript.SaveGame(playerData);
        SceneManager.LoadScene("MainScene");
    }

    //Level
    public void ReloadLevel() { SceneManager.LoadScene("MainScene"); }

    public void LoadSaveButtonUI(bool activate) { saveButton.SetActive(activate); }

    public void SaveLevel()
    {
        playerData = saveManagerScript.LoadGame();
        playerData.CompleteCurrentLevel();
        saveManagerScript.SaveGame(playerData);
        LoadMap();
    }
}
