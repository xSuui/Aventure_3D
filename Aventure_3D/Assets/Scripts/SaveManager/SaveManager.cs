using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;

    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Samuel";
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region Save
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }


    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();

    }

    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            // Check if _saveSetup is not null before accessing its members
            if (_saveSetup != null)
            {
                lastLevel = _saveSetup.lastLevel;
            }
            else
            {
                // Handle the case when _saveSetup is null (log, throw exception, etc.)
                // Example: Debug.LogError("SaveSetup is null after deserialization!");
            }
        }
        else
        {
            CreateNewSave();
            Save();
        }

        // Check if FileLoaded event is not null before invoking it
        if (FileLoaded != null)
        {
            FileLoaded.Invoke(_saveSetup);
        }
        else
        {
            // Handle the case when FileLoaded event is null (log, throw exception, etc.)
            // Example: Debug.LogWarning("FileLoaded event is null!");
        }
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    private void SaveLeveFive()
    {
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float health;
    public string playerName;
}




#region codigo aula
/* string fileLoaded = "";

 if (File.Exists(_path))
 {

      fileLoaded = File.ReadAllText(_path);
     _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
     lastLevel = _saveSetup.lastLevel;

 } 
 else
 {
     CreateNewSave();
     Save();
 }

 //FileLoaded.Invoke(_saveSetup);
*/
#endregion