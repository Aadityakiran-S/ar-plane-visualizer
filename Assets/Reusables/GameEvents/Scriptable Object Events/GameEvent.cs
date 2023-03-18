using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    public List<CallbackEvent> listeners;

    public void Subscribe(CallbackEvent listener)
    {
        listeners.Add(listener);
    }

    public void UnSubscribe(CallbackEvent listener)
    {
        listeners.Remove(listener);
    }

    public void InvokeEvent(Object data = null)
    {
        foreach (CallbackEvent listener in listeners.ToArray())
            listener.Invoke(data);
    }

  /*  public void InvokeEvent(string data)
    {
        foreach (CallbackEvent listener in listeners.ToArray())
            listener.Invoke(data);
    }

    public void InvokeEvent(GameObject data = null)
    {
        foreach (CallbackEvent listener in listeners.ToArray())
            listener.Invoke(data);
    }*/
}

