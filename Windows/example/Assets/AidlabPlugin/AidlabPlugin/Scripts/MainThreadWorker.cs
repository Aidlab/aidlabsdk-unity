using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class MainThreadWorker
{
    public readonly static Queue<UnityEvent> ExecuteOnMainThread = new Queue<UnityEvent>();


    public void Update()
    {
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();
        }
    }
}
