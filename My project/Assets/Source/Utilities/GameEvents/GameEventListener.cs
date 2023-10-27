using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    protected GameEvent _event;
    [SerializeField]
    protected UnityEvent responses;

    #endregion


    #region MonoBehaviour Callbacks

    protected void OnEnable() => _event.RegisterListener(this);

    protected void OnDisable() => _event.UnregisterListener(this);

    #endregion


    #region Public Callbacks

    public virtual void Raise() => responses?.Invoke();

    public virtual void ClearAll() => responses?.RemoveAllListeners();

    #endregion
}
