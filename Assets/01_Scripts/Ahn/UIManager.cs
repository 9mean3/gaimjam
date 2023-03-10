using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] string _sceneName;
    [SerializeField] GameObject SettingUICanvas;
    [SerializeField] GameObject SettingButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void sceneLoad()
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Setting_Exit()
    {
        SettingUICanvas.SetActive(false);
        SettingButton.SetActive(true);
    }
}
