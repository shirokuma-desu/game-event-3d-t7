using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "GameEvent", menuName = "Scriptable Objects/Game Event/Event", order = 1
)]
public class GameEvent : ScriptableObject
{
    #region Private Fields

    private readonly HashSet<GameEventListener> listeners = new();

    #endregion


    #region Public Callbacks

    public virtual void RegisterListener(GameEventListener listener) => listeners.Add(listener);

    public virtual void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);


    public void RaiseEvent()
    {
        foreach (var listener in listeners)
        {
            listener.Raise();
        }
    }

    public void ClearEvent()
    {
        foreach (var listener in listeners)
        {
            listener.ClearAll();
            UnregisterListener(listener);
        }
    }

    #endregion
}
