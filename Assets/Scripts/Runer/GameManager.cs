using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level levelPrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private GameObject mainCamera;

    private Level _gameLevel;
    private GameObject _levelCache;
    private int _coins = 0;
    private Vector3 camTarget = Vector3.zero;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        NewGame();
        uiManager.UpdateCoinText(_coins.ToString());
        _gameLevel.player.StartCoins(_coins);
    }

    private void Update()
    {
        Time.timeScale = 1f;
        if (!inputHandler.IsHold && _gameLevel.player.IsActive)
        {
           Time.timeScale = .3f;
        }
        _gameLevel.player.SetMoveOffset = inputHandler.HorizontalAxis;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 camNewPos = new Vector3(0, 
            _gameLevel.player.transform.position.y + 5.5f, 
            _gameLevel.player.transform.position.z - 7f);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camNewPos, Time.deltaTime * 5f);

        camTarget = new Vector3(_gameLevel.player.transform.position.x,
            _gameLevel.player.transform.position.y + .5f,
            _gameLevel.player.transform.position.z + 10f);

        Vector3 direction = camTarget - mainCamera.transform.position;
        
        Quaternion toRotation = Quaternion.FromToRotation(mainCamera.transform.forward, direction);
        
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, toRotation, Time.deltaTime * 2f);
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(camTarget, .2f);
    }

    public void NewGame()
    {
        if (_gameLevel != null) Destroy(_gameLevel.gameObject);
        _gameLevel = Instantiate(levelPrefab);
        _gameLevel.GenerateLevel();
        if(_levelCache != null) Destroy(_levelCache);
        _levelCache = Instantiate(_gameLevel.gameObject);
        _levelCache.gameObject.name = "LevelBuffer(To restart)";
        _levelCache.SetActive(false);
        UnSubPlayer();
    }

    public void StartGame()
    {
        _gameLevel.StartGame();
        uiManager.UpdateCoinText(_coins.ToString());
        _gameLevel.player.OnDied += OnPlayerDead;
        _gameLevel.player.OnFinish += OnPlayerFinish;
        _gameLevel.player.OnTakeCoin += OnPlayerTakeCoin;
    }

    private void UnSubPlayer()
    {
        _gameLevel.player.OnDied -= OnPlayerDead;
        _gameLevel.player.OnFinish -= OnPlayerFinish;
        _gameLevel.player.OnTakeCoin -= OnPlayerTakeCoin;
    }

    public void RestartGame()
    {
        Destroy(_gameLevel.gameObject);
        GameObject levelCopy = Instantiate(_levelCache);
        levelCopy.SetActive(true);
        levelCopy.name = "Level";
        _gameLevel = levelCopy.GetComponent<Level>();
        _gameLevel.StartGame();
    }

    private void OnPlayerTakeCoin()
    {
        uiManager.UpdateCoinText((_coins + _gameLevel.player.GetCoins).ToString());
    }

    private void OnPlayerFinish()
    {
        _coins += _gameLevel.player.GetCoins;
        uiManager.UpdateCoinText(_coins.ToString());
        uiManager.ShowWin();
        Time.timeScale = 1;
    }

    private void OnPlayerDead()
    {
        uiManager.ShowFail();
    }
}
