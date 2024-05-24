using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MachineTooltipUI _machineInfoTtooltip;

    private void OnEnable()
    {
        _inputManager.Select += ShowTooltip;
        _inputManager.Discard += HideTooltip;
        _inputManager.Pan += HideTooltip;
    }

    private void OnDisable()
    {
        _inputManager.Select -= ShowTooltip;
        _inputManager.Discard -= HideTooltip;
        _inputManager.Pan -= HideTooltip;
    }

    private void ShowTooltip(GameObject selected, Vector2 position)
    {
        MachineController selectedMachine = selected.GetComponentInParent<MachineController>();
        if (selectedMachine == null) return;

        _machineInfoTtooltip.MachineController = selectedMachine;
        _machineInfoTtooltip.gameObject.transform.position = position;
        _machineInfoTtooltip.gameObject.SetActive(true);
    }

    private void HideTooltip()
    {
        _machineInfoTtooltip.gameObject.SetActive(false);
    }
}
