using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab = null;
    [SerializeField] private string _gameSceneName = null;

    private NetworkRunner _runnerInstance = null;

    // Attempts to start a new game session 
    public void StartHost()
    {
        StartGame(GameMode.AutoHostOrClient, "1.1.1.1", _gameSceneName);
    }

    public void StartClient()
    {
        StartGame(GameMode.Client, "1.1.1.1", _gameSceneName);
    }

    private async void StartGame(GameMode mode, string roomName, string sceneName)
    {
        _runnerInstance = FindObjectOfType<NetworkRunner>();
        if (_runnerInstance == null)
        {
            _runnerInstance = Instantiate(_networkRunnerPrefab);
        }

        // Let the Fusion Runner know that we will be providing user input
        _runnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
        };

        // GameMode.Host = Start a session with a specific name
        // GameMode.Client = Join a session with a specific name
        await _runnerInstance.StartGame(startGameArgs);

        _runnerInstance.SetActiveScene(sceneName);
    }
}