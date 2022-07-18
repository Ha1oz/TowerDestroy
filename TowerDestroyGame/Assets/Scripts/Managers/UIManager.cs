using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : SingleTone<UIManager>
{

    private Button pauseBtn, shieldBtn, disabledShieldBtn; //resumeBtn, restartBtn;
    private VisualElement cooldownPlane;//, pauseImg, pauseMenu; //pausePanel
    private ProgressBar PlayerHPBar, EnemyHPBar;
    private Label timerText;

    //private bool isPause = false;

    private float playerMaxHealth, enemyMaxHealth;
    private int playerCooldown, minPlayerCooldown, secPlayerCooldown;
    private bool isShieldCanPressed, isShieldAlive;

    // Start is called before the first frame update
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pauseBtn = root.Q<Button>("PauseBtn");
        shieldBtn = root.Q<Button>("ShieldBtn");
        disabledShieldBtn = root.Q<Button>("DisabledShieldBtn");

        cooldownPlane = root.Q<VisualElement>("CooldownPlane");

        timerText = root.Q<Label>("Timer");

        PlayerHPBar = root.Q<ProgressBar>("HealthBar");
        EnemyHPBar = root.Q<ProgressBar>("EnemyHealthBar");
        
        PlayerHPBar.lowValue = 100f;
        playerMaxHealth = FindObjectOfType<PlayerAction>().health;

        EnemyHPBar.lowValue = 100f;
        enemyMaxHealth = FindObjectOfType<Enemy>().health;

        isShieldCanPressed = true;
        isShieldAlive = false;

        playerCooldown = FindObjectOfType<PlayerAction>().getShieldDelay();
        minPlayerCooldown = playerCooldown / 60;
        secPlayerCooldown = playerCooldown % 60;

        pauseBtn.clicked += PauseBtnMethod;
        shieldBtn.clicked += ShieldBtnPressed;

    }
    
    private void PauseBtnMethod() 
    {
        UIPauseManager.Instance.OpenPauseMenu();
        Time.timeScale = 0;
        

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
        Time.timeScale = 0;
        UIPauseManager.Instance.GameOver();

    }

    public void ShieldIsBroken() 
    {
        isShieldAlive = false;
    }
}
