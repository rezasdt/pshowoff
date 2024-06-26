using UnityEngine;

public class StarterMachineController : MachineController
{
    public override int ResaleValue
    {
        get => Mathf.FloorToInt(Machine.Value * MachineBase.ResaleFactor);
    }

    private void Start()
    {
        AudioManager.Instance.PlaySoundeffect(AudioManager.Instance.Sounds.BuildSuccess);
        StartCoroutine(EarnCoroutine());
    }
}
