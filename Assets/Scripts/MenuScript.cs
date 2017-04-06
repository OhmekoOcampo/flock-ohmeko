
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas choiceMenu;
    public Button startText;
    public Button exitText;
    public Button creditText;


	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        choiceMenu = choiceMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        creditText = creditText.GetComponent<Button>();

        quitMenu.enabled = false; //make the quitMenu not shown at the beginning of game.
        choiceMenu.enabled = false; //make the choiceMenu not shown at the beginnning of game. 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChoicePress()
    {
        quitMenu.enabled = false;
        choiceMenu.enabled = true;
        
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
        creditText.enabled = false;
        choiceMenu.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
        creditText.enabled = true;
        choiceMenu.enabled = false;

    }

    public void BackToMenuPress()
    {
        choiceMenu.enabled = false;
        quitMenu.enabled = true;
        startText.enabled = true;
        creditText.enabled = true;
        exitText.enabled = true;

    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Flock1");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectFlock2()
    {
        SceneManager.LoadScene("Flock2");
    }

    public void SelectFlock1()
    {
        SceneManager.LoadScene("Flock1");
    }

}
