using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playAgain()
    {
        SceneManager.LoadScene("SampleScene");
        Movement.timer = 120;
        Movement.hasKey = false;
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
