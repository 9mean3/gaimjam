using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Slider leftSlider;
    [SerializeField] Slider rightSlider;

    float maxHp = 0;
    float curHp = 0;  
    void Start()
    {
        maxHp = FindObjectOfType<Player>().hp;
        curHp = maxHp;
        leftSlider.value = curHp / maxHp;
        rightSlider.value = curHp / maxHp;
    }
    void Update()
    {
        HandleHp();
    }

    private void HandleHp()
    {
        leftSlider.value = Mathf.Lerp(leftSlider.value, curHp / maxHp, Time.deltaTime);
        rightSlider.value = Mathf.Lerp(leftSlider.value, curHp / maxHp, Time.deltaTime);
    }

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
