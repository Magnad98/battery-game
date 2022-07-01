using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    string activeScene;
    SaveManager saveManagerScript;
    PlayerData playerData;

    [SerializeField] GameObject backgroundUI, buttonsUI, recycleUI, newGameButton, loadButton, recycleButton, levelButtons, recycleWindow;
    bool levelsLoaded;

    void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;

        switch (activeScene)
        {
            case "MainMenu":
                {
                    break;
                }
            case "Map":
                {
                    saveManagerScript = FindObjectOfType<GameManager>().GetComponent<SaveManager>();
                    newGameButton.SetActive(true);
                    loadButton.SetActive(false);
                    recycleButton.SetActive(false);
                    levelButtons.SetActive(false);
                    levelsLoaded = false;
                    recycleUI.SetActive(false);
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
                    levelButtons.SetActive(true);
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
                    levelButtons.SetActive(true);
                    levelsLoaded = true;
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
    }

    public void OK()
    {
        backgroundUI.SetActive(true);
        buttonsUI.SetActive(true);
        recycleUI.SetActive(false);
    }
}
