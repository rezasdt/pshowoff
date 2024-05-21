using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MachinesDatabase : ScriptableObject
{
    public List<Tier> tiers = new List<Tier>();
}
