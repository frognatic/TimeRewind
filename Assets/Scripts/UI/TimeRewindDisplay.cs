using System;
using TimeReverse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeRewindDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI frameCounterText;
        [SerializeField] private TextMeshProUGUI rewindSpeedText;

        [SerializeField] private Slider timelineSlider;
    
        private void OnEnable()
        {
            TimeReverseController.OnFrameUpdateAction += OnFrameUpdateAction;
            TimeReverseController.OnStopRecording += SetupSlider;
            
            timelineSlider.onValueChanged.AddListener(SliderValueChange);
        }

        private void OnDisable()
        {
            TimeReverseController.OnFrameUpdateAction -= OnFrameUpdateAction;
            TimeReverseController.OnStopRecording -= SetupSlider;
        }

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

        private void SetupSlider()
        {
            timelineSlider.minValue = 0;
            timelineSlider.maxValue = TimeReverseController.Instance.RecordedFrames - 1;

            timelineSlider.value = timelineSlider.maxValue;
        }

        private void SliderValueChange(float value) => TimeReverseController.Instance.SetToFrame((int)value);
    }
}
