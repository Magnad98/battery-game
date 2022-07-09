using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Scripts
    LevelLoader levelLoaderScript;
    SaveManager saveManagerScript;
    LevelManager levelManager;

    //UI Elements
    [SerializeField] GameObject backgroundUI, buttonsUI, recycleUI, newGameButton, loadButton, recycleButton, recycleWindow, saveButton;
    [SerializeField] GameObject[] levelButtons;

    //Prefabs
    [SerializeField] GameObject pusherManagerPrefab;

    //Variables
    GameObject pusherInstance;
    PlayerData playerData;
    string activeScene;
    bool levelsLoaded;

    //Methods
    void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
        saveManagerScript = FindObjectOfType<GameManager>().GetComponent<SaveManager>();
        levelLoaderScript = FindObjectOfType<GameManager>().GetComponent<LevelLoader>();

        switch (activeScene)
        {
            case "MainMenu":
                {
                    break;
                }
            case "Map":
                {
                    newGameButton.SetActive(true);

                    loadButton.SetActive(false);
                    recycleButton.SetActive(false);
                    foreach (GameObject levelButton in levelButtons)
                    {
                        levelButton.SetActive(false);
                    }
                    levelsLoaded = false;
                    recycleUI.SetActive(false);
                    break;
                }
            case "MainScene":
                {
                    levelManager = FindObjectOfType<GameManager>().GetComponent<LevelManager>();
                    saveButton.SetActive(false);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void Update()
    {
        switch (activeScene)
        {
            case "MainMenu":
                {
                    break;
                }
            case "Map":
                {
                    if (!System.IO.File.Exists(saveManagerScript.GetSaveGamePath()))
                    {
                        loadButton.SetActive(false);
                    }
                    else
                    {
                        if (!levelsLoaded)
                        {
                            loadButton.SetActive(true);
                        }
                    }
                    break;
                }
            case "MainScene":
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void NewGame()
    {
        switch (activeScene)
        {
            case "MainMenu":
                {
                    break;
                }
            case "Map":
                {
                    playerData = saveManagerScript.NewGame();
                    newGameButton.SetActive(false);
                    loadButton.SetActive(false);
                    recycleButton.SetActive(true);

                    List<Status> statuses = playerData.GetStatuses();
                    for (int i = 0; i < levelButtons.Length; i++)
                    {
                        levelButtons[i].SetActive(true);
                        levelButtons[i].GetComponent<Button>().interactable = statuses[i] == Status.unlocked || statuses[i] == Status.completed ? true : false;
                    }

                    levelsLoaded = true;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    public void LoadGame()
    {
        switch (activeScene)
        {
            case "MainMenu":
                {
                    break;
                }
            case "Map":
                {
                    playerData = saveManagerScript.LoadGame();
                    newGameButton.SetActive(false);
                    loadButton.SetActive(false);
                    recycleButton.SetActive(true);

                    List<Status> statuses = playerData.GetStatuses();
                    for (int i = 0; i < levelButtons.Length; i++)
                    {
                        levelButtons[i].SetActive(true);
                        levelButtons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
                        switch (statuses[i])
                        {
                            case Status.unlocked:
                                {
                                    levelButtons[i].GetComponent<Button>().interactable = true;
                                    break;
                                }
                            case Status.completed:
                                {
                                    levelButtons[i].GetComponent<Button>().interactable = true;
                                    levelButtons[i].GetComponent<Button>().GetComponent<Image>().color = new Color(0.72f, 0.88f, 0.98f);
                                    break;
                                }
                            default:
                                {
                                    levelButtons[i].GetComponent<Button>().interactable = false;
                                    break;
                                }
                        }
                    }
                    levelsLoaded = true;
                    break;
                }
            case "MainScene":
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void SaveGame()
    {
        switch (activeScene)
        {
            case "MainMenu":
                {
                    break;
                }
            case "Map":
                {
                    break;
                }
            case "MainScene":
                {
                    CompleteLevel();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void Recycle()
    {
        backgroundUI.SetActive(false);
        buttonsUI.SetActive(false);
        recycleUI.SetActive(true);
        pusherInstance = Instantiate(pusherManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void OK()
    {
        // backgroundUI.SetActive(true);
        // buttonsUI.SetActive(true);
        // recycleUI.SetActive(false);
        Destroy(pusherInstance);
        pusherInstance.GetComponent<PusherManager>().pusher.DisconnectAsync();
        LoadMap();
    }

    public void LoadLevel(int levelID)
    {
        playerData.SetCurrentLevel(levelID - 1);
        saveManagerScript.SaveGame(playerData);
        levelLoaderScript.LoadLevel("MainScene");
    }

    public void CompleteLevel()
    {
        playerData = saveManagerScript.LoadGame();
        playerData.CompleteCurrentLevel();
        saveManagerScript.SaveGame(playerData);
        LoadMap();
    }

    public void RestartLevel()
    {
        levelLoaderScript.LoadLevel("MainScene");
    }

    public void LoadMap()
    {
        levelLoaderScript.LoadLevel("Map");
    }

    public void LoadMenu()
    {
        levelLoaderScript.LoadLevel("MainMenu");
    }

    public void ActivateSaveButton(bool activate)
    {
        saveButton.SetActive(activate);
    }

    public void AddBatteries(int NineVolt, int D, int C, int AA, int AAA, int Cell)
    {
        Debug.Log(playerData.AddBatteries(NineVolt, D, C, AA, AAA, Cell));
        saveManagerScript.SaveGame(playerData);
    }
}
