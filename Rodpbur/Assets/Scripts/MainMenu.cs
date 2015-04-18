using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Button playBtn;
    public Button quitBtn;
	// Use this for initialization
	void Start () {
        playBtn.onClick.AddListener(() => { ButtonClicked("play"); });
        quitBtn.onClick.AddListener(() => { ButtonClicked("quit"); });
	}

    private void ButtonClicked(string btn)
    {
        switch (btn)
        {
            case"play":
                Application.LoadLevel(1);
                break;
            case"quit":
                Application.Quit();
                break;
            default:
                Debug.Log("error in main Menu buttons");
                break;
        }
    }
}
