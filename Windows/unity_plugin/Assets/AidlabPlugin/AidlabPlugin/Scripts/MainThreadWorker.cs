using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// The MainThreadWorker class facilitates execution of Unity events on the main thread.
/// </summary>
/// <remarks>
/// It maintains a queue of Unity events that need to be executed on the main thread.
/// The Update method should be called regularly to process the queued events.
/// </remarks>
public class MainThreadWorker : MonoBehaviour
{
    private static MainThreadWorker _instance;

    public static MainThreadWorker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("MainThreadWorker").AddComponent<MainThreadWorker>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private readonly Queue<UnityAction> actions = new Queue<UnityAction>();

    public void QueueOnMainThread(UnityEvent unityEvent)
    {
        lock (actions)
        {
            UnityAction action = () => unityEvent.Invoke();
            actions.Enqueue(action);
        }
    }

    public void Update()
    {
        lock (actions)
        {
            while (actions.Count > 0)
            {
                actions.Dequeue().Invoke();
            }
        }
    }
}