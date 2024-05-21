using UnityEngine;

[CreateAssetMenu]
public class Machine : ScriptableObject
{
    [field: Min(0)]
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
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
    public float UpgradeSuccessChance { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public Texture2D Thumbnail { get; private set; }
    [field: SerializeField]
    public Machine Upgrade { get; private set; }
}
