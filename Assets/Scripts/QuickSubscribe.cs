using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class QuickSubscribe : MonoBehaviour
{
    public bool setActive;
    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();
    private Mouledoux.Callback.Callback m_subCallback;

    public bool isZone;
    public string m_subMessage;
    public UnityEngine.Events.UnityEvent m_event;

    void Awake()
    {
        m_subCallback = InvokeUnityEvent;
        if (isZone)
        {
            var message = GetComponent<ScoreManager>().Message;
            var subString = message.Substring(4, 1);
            int sum;
            if (int.TryParse(subString, out sum))
            {
                m_subMessage = "Zone" + (sum - 1) + " Complete";
            }
        }
        m_subscriptions.Subscribe(m_subMessage, m_subCallback);
        gameObject.SetActive(setActive);
    }

    private void InvokeUnityEvent(Mouledoux.Callback.Packet emptyPacket)
    {
        m_event.Invoke();
    }

    private void OnDestroy()
    {
        m_subscriptions.UnsubscribeAll();
    }
}