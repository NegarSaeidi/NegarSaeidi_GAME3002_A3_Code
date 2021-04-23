using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class Movement : MonoBehaviour
{
   
    private Rigidbody body;
    public bool grounded;
    public Text timerText;
    public static float timer=120;
    public int speed = 40, jump = 60;
    public static bool hasKey = false;
    private bool closeToDoor = false;
    public static bool speedup = false,slowdown=false, unlocked = false;
    public Text key;
    public GameObject keyObject;
    private AudioSource keyPickup,speedSound;
    private AudioSource[] array;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        keyObject.SetActive(false);
      
            array= GetComponents<AudioSource>();
        keyPickup = array[0];
        speedSound = array[1];
       // timer = 120;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        /** * TIME  * *//////////////////////////////////////////////////////////
        float t =timer - Time.timeSinceLevelLoad;

        string min = ((int)t / 60).ToString();
        string sec = ((int)t % 60).ToString();
        if (t <= 0)
            SceneManager.LoadScene("lose");
        timerText.text = min + " : " + sec;
        //MOVEMENT/////////////////////////////////////////////////////////
        if (!grounded)
            body.AddForce(Vector3.down * speed);
        if (Input.GetKey(KeyCode.UpArrow))
            {
            body.AddForce(Vector3.up * jump);
            
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
         
                body.AddForce(Vector3.left * speed);
           
       
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                body.AddForce(Vector3.right * 20);
         
            }
    }
    private void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.tag == "floor")
        {
            grounded = true;
            if (!speedup)
            {
                if (!slowdown)
                {
                    speed = 40;
                    jump = 60;
                }
            }
        }
        if (other.gameObject.tag == "door")
        {
            Debug.Log("you near to door"+hasKey);
           
            if (hasKey)
            {
                Destroy(other.gameObject);
                keyObject.SetActive(false);
                hasKey = false;
             
            }
            else
            {
                closeToDoor = true;

                key.text = "No keys!";
               
                keyObject.SetActive(true);
            }
          

           
        }
        if (other.gameObject.tag == "key")
        {
            Debug.Log("you got it");
            Destroy(other.gameObject);
            hasKey = true;
            keyPickup.Play();
                key.text = "Door's unlocked!";
                keyObject.SetActive(true);
           

        }

        if (other.gameObject.tag == "spikes")
            SceneManager.LoadScene("lose");
        if (other.gameObject.tag == "trap")
            SceneManager.LoadScene("lose");
        if (other.gameObject.tag == "end")
            SceneManager.LoadScene("win");

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "floor")
            grounded = false;

        if (other.gameObject.tag == "door")
        {
          
            keyObject.SetActive(false);
        }
      

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Speedup")
        {
            speedSound.Play();
            speed *= 2;
            jump *=2;
            speedup = true;
        }
        if (other.gameObject.tag == "Slowdown")
        {

            speed /= 3;
            
            slowdown = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Speedup")
        {
           
            speedup = false;
        }
        if (other.gameObject.tag == "Slowdown")
        {

            slowdown = false;
        }
    }

}
