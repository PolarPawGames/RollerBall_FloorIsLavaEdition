using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class mainUIScript : MonoBehaviour
{
    public Text playerHighScore;
    public InputField playerName;
    // Start is called before the first frame update
    void Start()
    {
        updateMenu ();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateMenu()
        {
        if (MainManager.Instance.storedPlayerName != null)
            {
            playerName.text = MainManager.Instance.storedPlayerName;
            }
        if (MainManager.Instance.highscorePlayerName != null)
            {
            playerHighScore.text = "Current Highscore: " + MainManager.Instance.highscorePlayerName + " " + MainManager.Instance.highscorePlayerTime;
            }
        }

    public void loadScene()
        {
        SceneManager.LoadScene ( 1 );
        }

    public void enterName()
        {
        MainManager.Instance.storedPlayerName = playerName.text;
        Debug.Log ( "playername " + playerName.text );
        }

   public void fillVariables()
        {
        Application.Quit ();
      
        }
    }
