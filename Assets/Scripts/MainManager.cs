using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
    {

    public static MainManager Instance;

    public string highscorePlayerName;
    public float highscorePlayerTime;

    public string storedPlayerName;
    public float storedTime;
    public bool shouldCheckHighScore;
    // Start is called before the first frame update
    private void Awake ( )
        {

        if (Instance != null)
            {
            Destroy ( gameObject );
            return;
            }
        Instance = this;
        DontDestroyOnLoad ( gameObject );

        loadGame ();
        }

    public void saveGame ( )
        {
        savedData newData = new savedData ();
        newData.playerAcive = storedPlayerName;
        newData.playerName = highscorePlayerName;
        newData.playerTime = highscorePlayerTime;

        string content = JsonUtility.ToJson ( newData );
         string path = Application.persistentDataPath + "/savegame.txt";
        File.WriteAllText ( path, content );
        Debug.Log ( "Data saved" );
        }

    public void loadGame()
        {
        bool loadExists;
         try            {
            string fromFile = File.ReadAllText ( Application.persistentDataPath + "/savegame.txt" );
            loadExists = true;
            Debug.Log ( "Found loadfile" );
            }
        catch (System.Exception)
            {
            loadExists = false;
            Debug.Log ( "No loadfile found" );
            throw;
            }
        

        if (loadExists)
            {
            //string fromFile = File.ReadAllText ( Application.persistentDataPath + "/savegame.txt" );
            savedData loadData = JsonUtility.FromJson<savedData> ( File.ReadAllText ( Application.persistentDataPath + "/savegame.txt" ));
            storedPlayerName = loadData.playerAcive;
            highscorePlayerName = loadData.playerName;
            highscorePlayerTime = loadData.playerTime;
            }
       
        }

    [System.Serializable]
    public class savedData
        {
        public string playerName;
        public float playerTime;
        public string playerAcive;
        }

    private void OnLevelWasLoaded ( int level )
        {
        if (level == 1)
            {
            Debug.Log ( "boardlevel loaded" );
            GameObject.Find ( "Canvas/PlayerName" ).GetComponent<Text> ().text = storedPlayerName;
            }
        if (level == 0)
            {
            GameObject.Find ("Canvas").GetComponent<mainUIScript>().updateMenu ();
            Debug.Log ( "Levelwasloaded" );
            }
        }
    }
