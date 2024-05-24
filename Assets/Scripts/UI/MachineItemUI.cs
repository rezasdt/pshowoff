using UnityEngine;
using UnityEngine.UI;

public class MachineItemUI : MonoBehaviour
{
    public Machine machine;
    private Button _button;

    void Start()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        if (machine != null && _button != null && machine.Thumbnail != null)
        {
            Image image = _button.GetComponent<Image>();
            if (image != null)
                image.sprite = Sprite.Create(machine.Thumbnail, new Rect(0, 0, machine.Thumbnail.width, machine.Thumbnail.height), Vector2.one * 0.5f);
        }

        if (_button != null)
            _button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (machine != null)
            ObjectPlacer.SelectedMachine = machine;
    }
}
