using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYAxis;
    int Inverted = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("InvertYToggle"))
            invertYAxis.isOn = PlayerPrefs.GetInt("InvertYToggle") == 0 ? false : true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        string lastScene = PlayerPrefs.GetString("PreviousScene");
        SceneManager.LoadScene(lastScene);
    }

    public void Apply()
    {
        if (invertYAxis.isOn)
            PlayerPrefs.SetInt("InvertYToggle", 1);
        else
            PlayerPrefs.SetInt("InvertYToggle", 0);
        Back();
    }
}