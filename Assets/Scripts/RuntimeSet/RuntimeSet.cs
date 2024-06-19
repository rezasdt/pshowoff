using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    protected HashSet<T> Items { get; private set; } = new();

    public virtual void Add(T item)
    {
        if (!Items.Contains(item))
        {
            Items.Add(item);
        }
    }

    public virtual void Remove(T item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
        }
    }

    public virtual bool Contains(T item)
    {
        return Items.Contains(item);
    }

    public virtual void Clear()
    {
        Items.Clear();
    }
}