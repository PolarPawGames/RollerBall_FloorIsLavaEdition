using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballScript : MonoBehaviour
{
    public Text indicator;
    public Slider meltingPoint;
    public Collider playerCollider;
    public bool inMax = false, inMin = false, inHalf = false;
    public int maxHeat;
    public boardScript board;
    public GameObject goalArea;
    
    // Start is called before the first frame update
    void Start()
    {
        goalArea = GameObject.Find ( "Board" ).transform.GetChild ( 0 ).gameObject;
        board = GameObject.Find ( "Board" ).GetComponent<boardScript> ();
        playerCollider = GetComponent<Collider> ();
        indicator = GameObject.Find ( "Canvas" ).transform.GetChild ( 1 ).GetComponent<Text> ();
      
        meltingPoint = GameObject.Find ( "Canvas" ).transform.GetChild ( 3 ).GetComponent<Slider> ();
        meltingPoint.maxValue = maxHeat;
        meltingPoint.value = 0;
        StartCoroutine ( nameof ( checkHeat ) );
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator checkHeat ( )
        {

        while (true)
            {
            Debug.Log ( "Checking heat" );
            if (inMax) { meltingPoint.value += 5; indicator.text = "HOT!"; indicator.color = Color.red; }
            else if (inHalf) { meltingPoint.value += 3; indicator.text = "WARMER"; indicator.color = Color.yellow; }
            else if (inMin) { meltingPoint.value++; indicator.text = "WARM"; indicator.color = Color.cyan; }
            else { meltingPoint.value--; indicator.text = "COLD"; indicator.color = Color.blue; board.score += 1; }


            if (meltingPoint.value >= maxHeat)
                {
                Destroy ( gameObject );
                board.score = 0;
                
                GameObject.Find ( "Board" ).GetComponent<boardScript> ().dropBall ();
                }

            if (meltingPoint.value == 0 && board.score > 100)
                {
                goalArea.gameObject.SetActive ( true );
                }
            else
                {
                goalArea.gameObject.SetActive ( false );
                }
            yield return new WaitForSeconds ( 0.1f );
            }

        }
    private void OnTriggerEnter ( Collider other )
        {
        if (other.gameObject.CompareTag ( "Max" ))
            {
          
            inMax = true;
            }
        else if (other.gameObject.CompareTag ( "Half" ))
            {
           
            inHalf = true;
            }
        else if (other.gameObject.CompareTag ( "Min" ))
            {
           
            inMin = true;
            }

        if (other.gameObject.CompareTag("Finish"))
            {
            board.startExit ();
           
            }
        }

    private void OnTriggerExit ( Collider other )
        {
        if (other.gameObject.CompareTag ( "Max" ))
            {
            inMax = false;
            }
        else if (other.gameObject.CompareTag ( "Half" ))
            {
            inHalf = false;
            }
        else if (other.gameObject.CompareTag ( "Min" ))
            {
            
            inMin = false;
            }
        }
    }
