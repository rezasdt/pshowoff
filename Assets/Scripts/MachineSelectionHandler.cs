using UnityEngine;

public class MachineSelectionHandler : MonoBehaviour
{
    [SerializeField]
    private PointInputHandler pointInputHandler;
    [SerializeField]
    private Tag freePlatfromTag;

    [SerializeField]
    private RectTransform _instantiatedTooltip;

    private MachineController _selectedMachine;

    private void OnEnable()
    {
        pointInputHandler.OnSelect += OnSelect;
    }

    private void OnDisable()
    {
        pointInputHandler.OnSelect -= OnSelect;
    }

    public void Upgrade()
    {
        Instantiate(_selectedMachine.Machine.Upgrade.Prefab, _selectedMachine.gameObject.transform.position, Quaternion.identity);
        Destroy(_selectedMachine.gameObject);
        _instantiatedTooltip.gameObject.SetActive(false);
    }

    public void Sell()
    {
        _selectedMachine.Platform.tag = freePlatfromTag;
        _selectedMachine.Platform.SetActive(true);
        Destroy(_selectedMachine.gameObject);
        _instantiatedTooltip.gameObject.SetActive(false);
    }

    private void OnSelect(GameObject pSelectedObject, Vector2 pTooltipPosition)
    {
        var selected = pSelectedObject.GetComponentInParent<MachineController>();
        if (selected == null) return;

        _selectedMachine = selected;
        _instantiatedTooltip.transform.position = pTooltipPosition;
        _instantiatedTooltip.gameObject.SetActive(true);
    }
}