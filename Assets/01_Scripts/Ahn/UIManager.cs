using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class UIManager : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject SettingUICanvas;
    [SerializeField] GameObject SettingButton;

    public void sceneLoad()
    {
        SceneManager.LoadScene(sceneName);
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
