using UnityEngine;

public abstract class Variable<T> : ScriptableObject
{
    [SerializeField]
    private T _initialValue;
    [field: SerializeField]
    public T Value { get; set; }

    protected virtual void OnEnable()
    {
        Value = _initialValue;
    }
}
