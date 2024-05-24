using System.Reflection;
using System.Text;
using System;
using UnityEngine;

[CreateAssetMenu]
public class Machine : ScriptableObject
{
    [field: Min(0)]
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
    [ExcludeFromToString]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: Min(0)]
    [field: SerializeField]
    public int Earn { get; private set; }
    [field: Min(0)]
    [field: SerializeField]
    public int ResaleValue { get; private set; }
    [field: Min(0)]
    [field: SerializeField]
    public float EarningInterval { get; private set; }
    [field: Range(0, 100)]
    [field: SerializeField]
    public int UpgradeSuccessChance { get; private set; }
    [field: SerializeField]
    [ExcludeFromToString]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    [ExcludeFromToString]
    public Texture2D Thumbnail { get; private set; }
    [field: SerializeField]
    [ExcludeFromToString]
    public Machine Upgrade { get; private set; }

    private string _cachedToString;

    private void OnEnable()
    {
        _cachedToString = CacheFormattedToString();
    }

    private string CacheFormattedToString()
    {
        StringBuilder sb = new StringBuilder();

        PropertyInfo[] properties =
            this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo property in properties)
        {
            if (Attribute.IsDefined(property, typeof(ExcludeFromToStringAttribute)))
                continue;
            sb.Append($"<b>{property.Name}</b>: {property.GetValue(this)}\n");
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        return _cachedToString;
    }
}
