using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour {


    public Button toMenu;

	// Use this for initialization
	void Start () {
        toMenu = toMenu.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
