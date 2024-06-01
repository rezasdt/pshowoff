using UnityEngine;

[CreateAssetMenu(menuName = "Machine/Starter")]
public class StarterMachine : MachineBase
{
    [field: SerializeField]
    public Texture2D Thumbnail { get; private set; }
    public override int Value => Cost;
}
