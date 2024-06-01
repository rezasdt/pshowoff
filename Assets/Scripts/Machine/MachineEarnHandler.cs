using UnityEngine;

public class MachineEarnHandler : MonoBehaviour
{
    [SerializeField]
    private Int64Variable _moneyVariable;
    [SerializeField]
    private RectTransform _container;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private EarnIndicatorUIController _indicatorPrefab;

    private void OnEnable()
    {
        MachineController.OnEarn += OnEarn;
    }

    private void OnDisable()
    {
        MachineController.OnEarn -= OnEarn;
    }

    private void OnEarn(int pEarnAmount, Vector3 pMachinePosition)
    {
        _moneyVariable.Value += pEarnAmount;
        var newIndicator = Instantiate(_indicatorPrefab, _container);
        newIndicator.Init(pEarnAmount, _camera.WorldToScreenPoint(pMachinePosition));
    }
}
