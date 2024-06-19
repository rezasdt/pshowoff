using UnityEngine;
using UnityEngine.Pool;

public class MachineEarnHandler : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private RectTransform container;
    [SerializeField] private EarnIndicatorUIController indicatorPrefab;
    [SerializeField] private Camera _camera;
    [Header("SO")]
    [SerializeField] private Int64Variable moneyVariable;

    private ObjectPool<EarnIndicatorUIController> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<EarnIndicatorUIController>(
            CreateIndicator,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    private EarnIndicatorUIController CreateIndicator()
    {
        return Instantiate(indicatorPrefab, container);
    }

    private void OnGetFromPool(EarnIndicatorUIController indicator)
    {
        indicator.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(EarnIndicatorUIController indicator)
    {
        indicator.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EarnIndicatorUIController indicator)
    {
        Destroy(indicator.gameObject);
    }

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
        moneyVariable.Value += pEarnAmount;
        uiManager.OnEarn();
        var newIndicator = _pool.Get();
        newIndicator.Init(pEarnAmount, _camera.WorldToScreenPoint(pMachinePosition));
    }

    public void ReleaseIndicator(EarnIndicatorUIController indicator)
    {
        _pool.Release(indicator);
    }
}