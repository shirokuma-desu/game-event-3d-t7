using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(
    fileName = "GameEvent", menuName = "Scriptable Objects/Game Event/Event Callbacks", order = 2
)]
public class GameEventCallback : GameEvent
{
    #region Private Fields

    private readonly UnityEvent _Callbacks = new();

    #endregion


    #region Public Callbacks

    public void RegisterAction(UnityAction action) => _Callbacks.AddListener(action);

    public void UnregisterAction(UnityAction action) => _Callbacks.RemoveListener(action);

    public void RaiseCallbacks()
    {
        _Callbacks?.Invoke();
    }

    public void ClearCallbacks()
    {
        _Callbacks?.RemoveAllListeners();
    }

    #endregion
}
