using System;
using UnityEngine;
using UnityEngine.Pool;


public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [Header("Pooling")]
    [SerializeField]
    [Range(1, 300)]
    protected int _MaxSize;

    protected IObjectPool<T> _pool;
    protected readonly bool _check = true;

    protected void Awake()
    {
        _pool = new LinkedPool<T>(
            OnCreateInstance, OnGetInstance, OnReleaseInstance, OnDestroyInstance,
            _check, _MaxSize
        );
    }

    public virtual T Get()
    {
        var instance = _pool.Get();
        instance.transform.localPosition = Vector2.zero;

        return instance;
    }

    public virtual T Get(Vector3 position)
    {
        var instance = _pool.Get();
        instance.transform.localPosition = position;

        return instance;
    }

    public virtual void Release(T instance)
    {
        try
        {
            _pool.Release(instance);
            instance.transform.localPosition = Vector2.zero;
        }
        catch (Exception)
        {
            Debug.LogWarning($"Object is already in the object pool.");
        }
    }

    protected abstract T OnCreateInstance();

    protected virtual void OnGetInstance(T gameComponent)
    {
        if (gameComponent.gameObject.activeInHierarchy) return;
        gameComponent.gameObject.SetActive(true);
    }

    protected virtual void OnReleaseInstance(T gameComponent)
    {
        if (!gameComponent.gameObject.activeInHierarchy) return;
        gameComponent.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyInstance(T gameComponent)
    {
        Destroy(gameComponent.gameObject);
    }
}
