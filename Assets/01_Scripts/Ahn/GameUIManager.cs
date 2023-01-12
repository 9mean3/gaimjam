using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
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
}
