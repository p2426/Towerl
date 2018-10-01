﻿using UnityEngine;
using UnityEngine.SceneManagement;


struct LevelData
{
    public string highScoreString;
    public string starsString;
    public string levelLockString;
    public int highScore;
    public int stars;
    public int IsUnlocked;
};

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    /** Store the count of levels*/
    private const int NUMBER_OF_LEVELS = 30;

    /** Array of level datas, store highscore, stars earned for each level */
    private LevelData[] m_levelData = new LevelData[NUMBER_OF_LEVELS];

    [SerializeField]
    private Sprite m_OneStarSprite; 
    [SerializeField]
    private Sprite m_TwoStarSprite;
    [SerializeField]
    private Sprite m_ThreeStarSprite;

    public static LevelManager Instance
    {
        get
        {
            // If we have not created one GameManager yet, create one
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public void LoadLevelData()
    {
        // Initialize
        for (int i = 0; i < NUMBER_OF_LEVELS; i++)
        {
            m_levelData[i].highScoreString = "HIGH_SCORE_LEVEL_" + i;
            m_levelData[i].starsString = "STARS_LEVEL_" + i;
            m_levelData[i].levelLockString = "IS_LEVEL_LOCK_" + i;

            m_levelData[i].stars = Random.Range(1, 4);
            //m_levelData[i].stars = PlayerPrefs.GetInt(m_levelData[i].starsString);
            m_levelData[i].highScore = PlayerPrefs.GetInt(m_levelData[i].highScoreString);
            m_levelData[i].IsUnlocked = PlayerPrefs.GetInt(m_levelData[i].levelLockString);
        }
    }
    void Awake()
    {
        _instance = this;
        LoadLevelData();
    }

    // Just for testing purpose - Print highscores and stars
    public void PrintHighScores()
    {
        // Just for testing purpose - Print highscores and stars
        for (int i = 0; i < NUMBER_OF_LEVELS; i++) Debug.Log("Level: " + i + " " + m_levelData[i].highScoreString + " " + m_levelData[i].highScore + " " + m_levelData[i].starsString + " " + m_levelData[i].stars);
    }
    
    // Just for testing purpose - Set and print highscores and stars
    public void SetNewHigschores()
    {
        // Just for testing purpose - Set and print highscores and stars
        for (int i = 0; i < NUMBER_OF_LEVELS; i++)
        {
            PlayerPrefs.SetInt(m_levelData[i].highScoreString, i);
            PlayerPrefs.SetInt(m_levelData[i].starsString,  i * 2);
            PlayerPrefs.SetInt(m_levelData[i].levelLockString, i * 3);

            m_levelData[i].highScore = PlayerPrefs.GetInt(m_levelData[i].highScoreString);
            m_levelData[i].stars = PlayerPrefs.GetInt(m_levelData[i].starsString);
            m_levelData[i].IsUnlocked = PlayerPrefs.GetInt(m_levelData[i].levelLockString);
           // Debug.Log("Level: " + i + " " + m_levelData[i].highScoreString + " " + m_levelData[i].highScore + " " + m_levelData[i].starsString + " " + m_levelData[i].stars);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("P_Scene");
    }

    // Just for testing purpose - Reset to 0 and print highscores and stars
    public void ClearHighScores()
    { 
        // Just for testing purpose - Reset to 0 and print highscores and stars
        for (int i = 0; i < NUMBER_OF_LEVELS; i++)
        {
            PlayerPrefs.SetInt(m_levelData[i].highScoreString, 0);
            PlayerPrefs.SetInt(m_levelData[i].starsString, 0);
            PlayerPrefs.SetInt(m_levelData[i].levelLockString, 0);
            m_levelData[i].highScore = PlayerPrefs.GetInt(m_levelData[i].highScoreString);
            m_levelData[i].stars = PlayerPrefs.GetInt(m_levelData[i].starsString);
            m_levelData[i].IsUnlocked = PlayerPrefs.GetInt(m_levelData[i].levelLockString);
           // Debug.Log("Level: " + i + " " + m_levelData[i].highScoreString + " " + m_levelData[i].highScore + " " + m_levelData[i].starsString + " " + m_levelData[i].stars);
        }     
    }

    // Testing stufss DELETEEEEE
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P)) PrintHighScores();
        if (Input.GetKeyUp(KeyCode.S)) SetNewHigschores();
        if (Input.GetKeyUp(KeyCode.R)) ClearHighScores();
    }

    // Just for testing DELETEEEEE
    void OnGUI()
    {

       // ShowLevelGraphics();

    }

    // Just for testing purpose DELETEEEEE
    public void ShowLevelGraphics()
    {
        for (int i = 0; i < NUMBER_OF_LEVELS; i++)
        {
            GUI.Box(new Rect(50, (50 * i) + 10, 500, 50),   "Level " + i + " HS: " + m_levelData[i].highScore +
                                                            " Stars: " + m_levelData[i].stars +
                                                            " IsUnlocked?: " + m_levelData[i].IsUnlocked);
        }
    }
    

    // Set new level highscore
    public void SetLevelHighScore(int requestedLevel, int newHighScore)
    {
        PlayerPrefs.SetInt(m_levelData[requestedLevel].highScoreString, newHighScore);
        m_levelData[requestedLevel].highScore = newHighScore;
        PlayerPrefs.Save();
    }

    // Set the how many stars has been achieved in the level
    public void SetLevelStars(int requestedLevel, int starsEearned)
    {
        PlayerPrefs.SetInt(m_levelData[requestedLevel].starsString, starsEearned);
        m_levelData[requestedLevel].stars = starsEearned;
        PlayerPrefs.Save();
    }

    // Set the requested level status, whenever is locked or unlocked
    public void SetLevelLockStatus(int requestedLevel, int _IsLocked)
    {
        PlayerPrefs.SetInt(m_levelData[requestedLevel].levelLockString, _IsLocked);
        m_levelData[requestedLevel].IsUnlocked = _IsLocked;
        PlayerPrefs.Save();
    }

    // Set the requested level status, whenever is locked or unlocked
    public void SetLevelLockStatus(int requestedLevel, bool _IsLocked)
    {
        switch (_IsLocked)
        {
            case false:
                PlayerPrefs.SetInt(m_levelData[requestedLevel].levelLockString, 0);
                m_levelData[requestedLevel].IsUnlocked = 0;
                PlayerPrefs.Save();
                break;
            case true:
                PlayerPrefs.SetInt(m_levelData[requestedLevel].levelLockString, 1);
                m_levelData[requestedLevel].IsUnlocked = 1;
                PlayerPrefs.Save();
                break;
        }
    }

    // Return the highscore of requested level
    public int GetLevelHighScore(int requestedLevel)
    {
        return m_levelData[requestedLevel].highScore;
    }

    // Return the how many stars has been achieved in the level
    public int GetLevelStars(int requestedLevel)
    {
        return m_levelData[requestedLevel].stars;
    }

    // Return the requested level status, whenever is locked or unlocked
    public int GetLevelLockStatus(int requestedLevel)
    {
        return m_levelData[requestedLevel].IsUnlocked;
    }

    // Return image with one star
    public Sprite GetOneStarSprite()
    {
        return m_OneStarSprite;
    }

    // Return image with two stars
    public Sprite GetTwoStarsSprite()
    {
        return m_TwoStarSprite;
    }

    // Return image with three stars
    public Sprite GetThreeStarsSprite()
    {
        return m_ThreeStarSprite;
    }

}
