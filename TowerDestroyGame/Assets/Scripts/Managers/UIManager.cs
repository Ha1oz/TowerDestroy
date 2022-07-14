using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : SingleTone<UIManager>
{

    private Button pauseBtn, shieldBtn, resumeBtn, restartBtn, disabledShieldBtn;
    private VisualElement cooldownPlane, pausePanel, pauseImg;
    private ProgressBar PlayerHPBar, EnemyHPBar;
    private Label timerText;

    private bool isPause = false;

    private float playerMaxHealth, enemyMaxHealth;
    private int playerCooldown, minPlayerCooldown, secPlayerCooldown;
    private bool isShieldCanPressed, isShieldAlive;

    // Start is called before the first frame update
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pauseBtn = root.Q<Button>("PauseBtn");
        shieldBtn = root.Q<Button>("ShieldBtn");
        resumeBtn = root.Q<Button>("ResumeBtn");
        restartBtn = root.Q<Button>("RestartBtn");
        disabledShieldBtn = root.Q<Button>("DisabledShieldBtn");

        cooldownPlane = root.Q<VisualElement>("CooldownPlane");
        pausePanel = root.Q<VisualElement>("PausePanel");
        pauseImg = root.Q<VisualElement>("PauseText");

        timerText = root.Q<Label>("timer");

        PlayerHPBar = root.Q<ProgressBar>("HealthBar");
        EnemyHPBar = root.Q<ProgressBar>("EnemyHealthBar");
        
        PlayerHPBar.lowValue = 100f;
        playerMaxHealth = FindObjectOfType<PlayerAction>().health;

        EnemyHPBar.lowValue = 100f;
        enemyMaxHealth = FindObjectOfType<Enemy>().health;

        isShieldCanPressed = true;
        isShieldAlive = false;

        playerCooldown = FindObjectOfType<PlayerAction>().timer;
        minPlayerCooldown = playerCooldown / 60;
        secPlayerCooldown = playerCooldown % 60;

        pauseBtn.clicked += PauseBtnMethod;
        resumeBtn.clicked += ResumeBtnMethod;
        restartBtn.clicked += RestartBtnMethod;
        shieldBtn.clicked += ShieldBtnPressed;

    }
    
    private void PauseBtnMethod() 
    {
        isPause = !isPause;
        
        if (isPause)
        {
            Time.timeScale = 0;
            pausePanel.style.display = DisplayStyle.Flex;
        }

    }
    private void ResumeBtnMethod()
    {
        isPause = !isPause;

        if (!isPause)
        {
            Time.timeScale = 1;
            pausePanel.style.display = DisplayStyle.None;
        }

    }
    private void RestartBtnMethod()
    {
        if (isPause)
        {
            Time.timeScale = 1;
            isPause = false;
            SceneManager.LoadScene(0);
        }
    }

    private void ShieldBtnPressed()
    {
        
        if (isShieldCanPressed && !isShieldAlive) {
            isShieldCanPressed = false;
            isShieldAlive = true;
            cooldownPlane.style.display = DisplayStyle.Flex;
            disabledShieldBtn.style.display = DisplayStyle.Flex;

            shieldBtn.style.display = DisplayStyle.None;

            FindObjectOfType<PlayerAction>().ActivateShield();

            StartCoroutine(Timer());          
        }
        
    }

    IEnumerator Timer() 
    {
        
        while (playerCooldown >= 0)
        {
        
            timerText.text = (playerCooldown / 60) + ":" + (playerCooldown % 60);

            playerCooldown--;

            yield return new WaitForSeconds(1);
        }
        cooldownPlane.style.display = DisplayStyle.None;
        playerCooldown = minPlayerCooldown * 60 + secPlayerCooldown;

        disabledShieldBtn.style.display = DisplayStyle.None;
        shieldBtn.style.display = DisplayStyle.Flex;

        isShieldCanPressed = true;
    }


    public void UpdateMyHPBar(float currentlyHP) 
    {
        PlayerHPBar.lowValue = 100 * (currentlyHP / playerMaxHealth);
    }
    public void UpdateEnemyHPBar(float currentlyHP)
    {
        EnemyHPBar.lowValue = 100 * (currentlyHP / enemyMaxHealth);
    }
    public void GameOver()
    {
        isPause = !isPause;
        Time.timeScale = 0;

        pausePanel.style.display = DisplayStyle.Flex;
        resumeBtn.style.display = DisplayStyle.None;
        pauseImg.style.display = DisplayStyle.None;

    }

    public void ShieldIsBroken() 
    {
        isShieldAlive = false;
    }
}
