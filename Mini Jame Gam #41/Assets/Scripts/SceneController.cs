using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneChange(string name)
    {
        Debug.Log(name);
        SceneManager.LoadScene(name);
    }

}
