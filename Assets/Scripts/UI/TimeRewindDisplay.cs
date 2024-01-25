using TMPro;
using UnityEngine;

public class TimeRewindDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frameCounterText;
    [SerializeField] private TextMeshProUGUI rewindSpeedText;
    
    private void OnEnable() => TimeReverseController.OnFrameUpdateAction += OnFrameUpdateAction;

    private void OnDisable() => TimeReverseController.OnFrameUpdateAction -= OnFrameUpdateAction;

    private void Start() => SetRewindSpeedText();

    private void SetRewindSpeedText() =>
        rewindSpeedText.text = $"Rewind speed x{TimeReverseController.Instance.RewindSpeed}";

    private void OnFrameUpdateAction(int frameCounter) => frameCounterText.text = $"Frame: {frameCounter}";
}
