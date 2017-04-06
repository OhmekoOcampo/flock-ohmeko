using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PauseGame : MonoBehaviour {

    public Transform canvas;
    
    // Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
            
        	
	}

    public void Pause()
    {
         if(canvas.gameObject.activeInHierarchy == false)
         {
               canvas.gameObject.SetActive(true);
               Time.timeScale = 0;//Stop time in game.
         }
         else
         {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;//Resume time in game.
         }
    }

    public void BackToMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        
    }
}
