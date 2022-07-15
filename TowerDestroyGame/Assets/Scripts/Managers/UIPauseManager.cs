using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIPauseManager : SingleTone<UIPauseManager>
{
    private Button resumeBtn, restartBtn;
    private VisualElement pausePanel, pauseImg;

    // Start is called before the first frame update
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        resumeBtn = root.Q<Button>("ResumeBtn");
        restartBtn = root.Q<Button>("RestartBtn");

        pausePanel = root.Q<VisualElement>("PausePanel");
        pauseImg = root.Q<VisualElement>("PauseImg");
        pausePanel.style.display = DisplayStyle.None;

        resumeBtn.clicked += ResumeBtnMethod;
        restartBtn.clicked += RestartBtnMethod;
    }

    public void OpenPauseMenu()
    {
        pausePanel.style.display = DisplayStyle.Flex;
    }

    private void ResumeBtnMethod()
    {
        Time.timeScale = 1;
        pausePanel.style.display = DisplayStyle.None;

            //pauseMenu.style.display = DisplayStyle.None;
    }
    private void RestartBtnMethod()
    {
        Time.timeScale = 1;
        pausePanel.style.display = DisplayStyle.None;
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        pausePanel.style.display = DisplayStyle.Flex;
        resumeBtn.style.display = DisplayStyle.None;
        pauseImg.style.display = DisplayStyle.None;

    }

}
