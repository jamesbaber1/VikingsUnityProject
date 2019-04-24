using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
  
    public void play()
    {
        SceneManager.LoadScene("You");

    }
}