using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class boardScript : MonoBehaviour
{
    public GameObject board, balldropper, ball;
    public Text scoreText, timeText;
    public float horizontalInput, verticalInput, speed, score;
    public Button exit;
    private float timeUsed;
    public bool PlayerWin = false;
    // Start is called before the first frame update
    void Start()
    {
       
        scoreText = GameObject.Find ( "Canvas" ).transform.GetChild ( 2 ).GetComponent<Text> ();
        score = 0;
        dropBall ();
        }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis ( "Horizontal" );
        verticalInput = Input.GetAxis ( "Vertical" );
        board.transform.Rotate ( Vector3.right * Time.deltaTime * verticalInput * speed );
        board.transform.Rotate ( Vector3.forward * Time.deltaTime * horizontalInput * speed );

        if (score > 100 ) { score = 100; }
        scoreText.text = "Score: " + score;
        timeText.text = Time.timeSinceLevelLoad.ToString ();
        
    }

    public void startExit()
        {
        StartCoroutine ( nameof ( PlayerHasWon ) );
        }

    private IEnumerator PlayerHasWon()
        {
        ball.GetComponent<Rigidbody> ().drag = 100;
        MainManager.Instance.storedTime = Time.timeSinceLevelLoad;
        checkHighScore (MainManager.Instance.storedTime, MainManager.Instance.storedPlayerName);
        yield return new WaitForSeconds ( 3.0f );
        ExitToMenu ();
        }
    public void dropBall()
        {
        Instantiate ( ball, balldropper.transform.position, Quaternion.identity );
        }

    public void ExitToMenu()
        {
        SceneManager.LoadScene ( 0 );
        }

    public void checkHighScore ( float nowScore, string playerName )
        {
        Debug.Log ( "Checking highscore" );
        if (nowScore < MainManager.Instance.highscorePlayerTime)
            {
            Debug.Log ( "New highscore found" );
            MainManager.Instance.highscorePlayerTime = nowScore;
            MainManager.Instance.highscorePlayerName = playerName;
            MainManager.Instance.saveGame ();
            
            }
        }

    }
