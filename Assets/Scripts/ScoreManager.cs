using System.Collections;
using Mouledoux.Callback;
using Mouledoux.Components;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ScoreManager : MonoBehaviour
{
    public int Score, TargetScore;
    public string Message;
    private Mediator.Subscriptions _subscriptions;
    private Callback _incrementScoreCallback;

    private void Awake()
    {
        _subscriptions = new Mediator.Subscriptions();
        _incrementScoreCallback += IncrementScore;
        _subscriptions.Subscribe(gameObject.name, _incrementScoreCallback);
    }

    /// <summary>
    ///     Reset Score and then wait until target score is it to notify subs
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        ResetScore();
        yield return new WaitUntil(() => GetScore() >= TargetScore);
        Mediator.instance.NotifySubscribers(Message, new Packet());
    }

    /// <summary>
    ///     Returns the current score
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return Score;
    }

    /// <summary>
    ///     Sets the current score
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int SetScore(int value)
    {
        return Score = value;
    }

    /// <summary>
    ///     Resets the current score
    /// </summary>
    public void ResetScore()
    {
        SetScore(0);
    }

    /// <summary>
    ///     Increment the score by 1
    /// </summary>
    public void IncrementScore()
    {
        Score++;
    }

    public void IncrementScore(Packet emptyPacket)
    {
        Score++;
    }

    private void OnDestroy()
    {
        _subscriptions.UnsubscribeAll();
    }
}
