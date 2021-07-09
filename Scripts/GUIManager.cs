using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GUIManager : MonoBehaviour
{
    public static GUIManager sharedInstance;
    [SerializeField] private Text healthText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text finalScore;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject inGameScreen;
    [SerializeField] private GameObject mainMenuScreen;
    
    public AudioSource soundButton;
    public AudioSource mainMenuClip;
    public AudioSource inGameClip;
    public AudioSource gameOverClip;

    public GameState currentGameState = GameState.mainMenu;
    public enum GameState{
        mainMenu,
        menu,
        inGame,
        gameOver
    }
    
    private void Awake()
    {
        Time.timeScale = 0f;
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    
    public void updateUIScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    
    public void updateUIHealth(int newHealth)
    {
        healthText.text = newHealth.ToString();
    }
    
    public void updateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }
    
    public void StartGame(){
        Time.timeScale = 1f;
        SetGameState(GameState.inGame);
    }

    public void Pause(){
        SetGameState(GameState.menu);
    }
    public void Resume(){
        Time.timeScale = 1f;
        SetGameState(GameState.inGame);
    }
    public void PlayAgain(){
        SceneManager.LoadScene("Game");
    }
    
    public void BackToMenu(){
        SetGameState(GameState.mainMenu);
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }


    public void ShowGameOverMenu()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        finalScore.text = "" + GameManager.sharedInstance.Score;
    }
    public void HideGameOverMenu()
    {
        gameOverScreen.SetActive(false);
    }
    public void ShowMainMenu() {
        mainMenuScreen.SetActive(true);
    }
    
    public void HideMainMenu() {
        mainMenuScreen.SetActive(false);
    }
    
    public void ShowMenu() {
        //Pausar juego
        Time.timeScale = 0f;
        menuScreen.SetActive(true);
    }
    
    public void HideMenu() {
        //Quitar pausa del juego
        Time.timeScale = 1f;
        menuScreen.SetActive(false);
    }
    public void ShowInGame() {
        inGameScreen.SetActive(true);
    }
    
    public void HideInGame() {
        inGameScreen.SetActive(false);
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    

    private void SetGameState(GameState newgameState)
    {
        switch (newgameState)
        {
            case GameState.mainMenu:
                ShowMainMenu();
                HideInGame();
                HideGameOverMenu();
                break;
            case GameState.menu:
                HideInGame();
                ShowMenu();
                break;
            case GameState.inGame:
                //Controller.StartGame(); //aqui inicia el juego o algo asi :v
                AudioClip.Destroy(mainMenuClip);
                inGameClip.Play();
                HideMainMenu();
                ShowInGame();
                HideMenu();
                HideGameOverMenu();
                break;
            case GameState.gameOver:
                Destroy(inGameClip);
                gameOverClip.Play();
                Time.timeScale = 0f;
                ShowGameOverMenu();
                HideInGame();
                break;
        }
        this.currentGameState = newgameState;
    }
    
    public void SoundButton()
    {
        soundButton.Play();
    }
    
}
