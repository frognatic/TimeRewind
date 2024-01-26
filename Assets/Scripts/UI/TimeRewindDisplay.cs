using TimeReverse;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimeRewindDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI frameCounterText;
        [SerializeField] private TextMeshProUGUI rewindSpeedText;
    
        private void OnEnable() => TimeReverseController.OnFrameUpdateAction += OnFrameUpdateAction;

        private void OnDisable() => TimeReverseController.OnFrameUpdateAction -= OnFrameUpdateAction;

        private void Start()
        {
            float rewindSpeed = TimeReverseController.Instance.RewindSpeed;
            SetRewindSpeedText(rewindSpeed);
        }

        private void SetRewindSpeedText(float rewindSpeed) =>
            rewindSpeedText.text = $"Rewind speed x{rewindSpeed}";

        private void OnFrameUpdateAction(int frameCounter) => frameCounterText.text = $"Frame: {frameCounter}";

        public void SetRewindSpeed(float rewindSpeedToSet)
        {
            TimeReverseController.Instance.SetRewindSpeed(rewindSpeedToSet);
            SetRewindSpeedText(rewindSpeedToSet);
        }
    }
}
