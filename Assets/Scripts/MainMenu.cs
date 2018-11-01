using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mouledoux.Callback;
using Mouledoux.Components;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string MainScene;
    public Text CountdownText;
    private int _countDown;
    public Button StartButton, ExitButton;
    private Mediator.Subscriptions _subscriptions;
    private Callback _startGameCallback, _exitGameCallback;
    private AsyncOperation _asyncSceneLoad;

    void Awake()
    {
        CountdownText.gameObject.SetActive(false);
        _subscriptions = new Mediator.Subscriptions();
        _startGameCallback += StartGame;
        _exitGameCallback += ExitGame;
        _subscriptions.Subscribe(StartButton.name, _startGameCallback);
        _subscriptions.Subscribe(ExitButton.name, _exitGameCallback);

        StartButton.onClick.AddListener(Notify);
        ExitButton.onClick.AddListener(Notify);
    }

    void Start()
    {
        _asyncSceneLoad = SceneManager.LoadSceneAsync(MainScene);
        _asyncSceneLoad.allowSceneActivation = false;
    }

    public void Notify()
    {
        Mediator.instance.NotifySubscribers(EventSystem.current.currentSelectedGameObject.name, new Packet());
    }

    private void StartGame(Packet emptyPacket)
    {
        StartCoroutine(StartGameDelay(5));
    }

    private void ExitGame(Packet emptyPacket)
    {
        Application.Quit();
    }

    private IEnumerator StartGameDelay(int time)
    {
        _countDown = time;
        StartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        CountdownText.gameObject.SetActive(true);
        CountdownText.text = _countDown.ToString();
        while (_countDown > 0)
        {
            yield return new WaitForSeconds(1.0f);
            _countDown--;
            CountdownText.text = _countDown.ToString();
        }

        _asyncSceneLoad.allowSceneActivation = true;
    }

    public void OnDestroy()
    {
        _subscriptions.UnsubscribeAll();
    }
}
