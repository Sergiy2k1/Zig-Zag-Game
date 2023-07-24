using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{   
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text[] _scoreTexts;
    [SerializeField] private TMP_Text _bestScoreText;
    private int _score = 0;
    private bool _isDead = false;

    private void Awake()
    {
        Time.timeScale = 0f;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        StartCoroutine(nameof(Score));
    }

    void Update()
    {
        UpdateScore();
        BestScore();


        if(_isDead) {
            GameOver();
        }
    }

    private void UpdateScore() {
        for(int i = 0; i < _scoreTexts.Length; i++) {
            _scoreTexts[i].text = _score.ToString("00");
        }
    }

    private void BestScore() {
        _bestScoreText.text = PlayerPrefs.GetInt("_bestScore").ToString("00");

        if(_score > PlayerPrefs.GetInt("_bestScore")) {
            PlayerPrefs.SetInt("_bestScore", _score);
        }
    }

    public bool GetDead() {
        return _isDead;
    }

    public void SetDead(bool _value) {
        _isDead = _value;
    }

    IEnumerator Score() {
        while(!_isDead) {
            yield return new WaitForSeconds(1f);
            _score++;
        }
    }

    private void GameOver() {
        _gameOverPanel.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }
}
