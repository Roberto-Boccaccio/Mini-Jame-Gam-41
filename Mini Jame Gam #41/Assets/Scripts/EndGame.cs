using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    public GameObject score;
    public GameObject highScore;
    Text score_text;
    Text highScore_text;



    void Start()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        score_text = score.GetComponent<Text>();
        highScore_text = highScore.GetComponent<Text>();

        score_text.text = characterData.Points.ToString();
        highScore_text.text = characterData.HighScore.ToString();
    }
}
