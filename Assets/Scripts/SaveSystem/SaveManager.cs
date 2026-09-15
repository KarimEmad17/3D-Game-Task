using System.Collections.Generic;
using System.IO;
using UnityEngine;


[DefaultExecutionOrder(-1000)]
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveData CurrentData { get; private set; }

    private string savePath;

    private readonly List<ISaveable> saveables = new();
   

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(
            Application.persistentDataPath,
            "save.json"
        );

        
    }

    private void Start()
    {
        Initialize();
        LoadSaveables();
    }
    public void Initialize()
    {
        if (GameManager.Instance.IsNewGame)
        {
            CurrentData = new SaveData();
        }
        else
        {
            LoadGame();
        }
    }
    public void Register(ISaveable saveable)
    {
        if (!saveables.Contains(saveable))
        {
            saveables.Add(saveable);
        }
    }

    public void Unregister(ISaveable saveable)
    {
        saveables.Remove(saveable);
    }

    private void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            CurrentData = new SaveData();
            return;
        }

        string json = File.ReadAllText(savePath);

        CurrentData = JsonUtility.FromJson<SaveData>(json);

        if (CurrentData == null)
        {
            CurrentData = new SaveData();
        }

        Debug.Log("Save data loaded.");
    }
    
    public void SaveGame()
    {
        foreach (ISaveable saveable in saveables)
        {
            saveable.Save(CurrentData);
        }

        string json = JsonUtility.ToJson(CurrentData, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Game saved.");
    }

    private void LoadSaveables()
    {
        foreach (ISaveable saveable in saveables)
        {
            saveable.Load(CurrentData);
        }
    }

    public void NewGame()
    {
        CurrentData = new SaveData();

        foreach (ISaveable saveable in saveables)
        {
            saveable.Load(CurrentData);
        }

        SaveGame();

        Debug.Log("New game started.");
    }
    public static bool HasSaveFile()
    {
        string path = Path.Combine(
            Application.persistentDataPath,
            "save.json"
        );

        return File.Exists(path);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}