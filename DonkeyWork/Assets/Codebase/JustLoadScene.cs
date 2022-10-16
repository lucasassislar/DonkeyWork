using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class JustLoadScene : MonoBehaviour 
{
    public string NextlevelName;
    public bool LoadOnEnable;

    void Start()
    {
        if(LoadOnEnable) SceneManager.LoadScene(NextlevelName);
    }
    void Update()
    {
        //Keyboard keyboard = Keyboard.current;
        //if (keyboard.enterKey.isPressed && !LoadOnEnable)
        //{
        //    SceneManager.LoadScene(NextlevelName);
        //}
    }
    public void ChangeNextLevel(string levelName)
    {
        NextlevelName = levelName;
    }
}
