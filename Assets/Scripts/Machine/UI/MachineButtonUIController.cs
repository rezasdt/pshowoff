using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineButtonUIController : MonoBehaviour
{
    public static event System.Action<MachineBase> OnMachineClicked = delegate { };

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private TextMeshProUGUI _stage;
    private Button _currentButton;
    private StarterMachine _starterMachine;

    private void Awake()
    {
        _currentButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _currentButton.onClick.AddListener(OnButtonClick);
    }
    private void OnDisable()
    {
        _currentButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(StarterMachine pStarterMachine, int pStage)
    {
        _starterMachine = pStarterMachine;
        _icon.sprite =
            Sprite.Create(_starterMachine.Thumbnail,
                          new Rect(0, 0, _starterMachine.Thumbnail.width, _starterMachine.Thumbnail.height),
                          Vector2.one * 0.5f);
        _cost.text = $"{_starterMachine.Cost.ToString()}$";
        _stage.text = $"Stage {pStage}";

        Lock();
    }

    private void OnButtonClick()
    {
        OnMachineClicked.Invoke(_starterMachine);
    }

    public void Lock()
    {
        _currentButton.interactable = false;
        _lockPanel.SetActive(true);
    }

    public void Unlock()
    {
        _currentButton.interactable = true;
        _lockPanel.SetActive(false);
    }
}
