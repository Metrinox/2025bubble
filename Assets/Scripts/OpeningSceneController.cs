using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class OpeningSceneController : MonoBehaviour
{
    private Image storyBoard; 
    private TextMeshProUGUI subtitle; 
    private Button skipButton; 
    public Sprite scene2; 
    public Sprite scene3; 
    public Sprite scene4; 
    public Sprite scene5; 

    void Start() 
    {
        storyBoard = GameObject.FindGameObjectWithTag("Storyboard").GetComponent<Image>(); 
        subtitle = GetComponentInChildren<TextMeshProUGUI>();
        skipButton = GetComponentInChildren<Button>(); 
        skipButton.onClick.AddListener(skipScenes); 
        StartCoroutine(RunScenes()); 
    }

    public void skipScenes() {
        SceneManager.LoadScene("Tutorial"); 
    }

    private IEnumerator RunScenes() 
    {
        yield return new WaitForSeconds(3);
        storyBoard.overrideSprite = scene2;
        subtitle.text = "Icarus, you are the chosen one.";
        yield return new WaitForSeconds(3); 
        storyBoard.overrideSprite = scene3; 
        subtitle.text = "I am transforming you to a bubble, to escape this maze.";
        yield return new WaitForSeconds(4);
        storyBoard.overrideSprite = scene4;
        subtitle.text = "Take this gun with you.";
        yield return new WaitForSeconds(2); 
        storyBoard.overrideSprite = scene5; 
        subtitle.text = "Remember... don't get too close to the sun."; 
        yield return new WaitForSeconds(4); 
        SceneManager.LoadScene("Tutorial"); 
    }
}
