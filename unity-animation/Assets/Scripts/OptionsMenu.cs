using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle invertYToggle;
    [SerializeField]
    private Button backButton;
    [SerializeField] 
    private Button applyButton;
    
    void Start()
    {
        invertYToggle.isOn = PlayerPrefs.GetInt("yInverted") == 1;
        
        backButton.onClick.AddListener(Back);
        applyButton.onClick.AddListener(Apply);
    }
    
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString
            ("PreviousScene"));
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("yInverted", invertYToggle.isOn ? 1 : 0);
    }
}
