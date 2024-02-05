using TimeReverse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeRewindDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI frameCounterText;

        [SerializeField] private Slider timelineSlider;
    
        private void OnEnable()
        {
            TimeReverseController.OnFrameUpdateAction += OnFrameUpdateAction;
            TimeReverseController.OnStopRecording += SetupSlider;
            TimeReverseController.OnFrameRewindAction += DisplayFrameAndRefreshSlider;
            
            timelineSlider.onValueChanged.AddListener(SliderValueChange);
        }

        private void OnDisable()
        {
            TimeReverseController.OnFrameUpdateAction -= OnFrameUpdateAction;
            TimeReverseController.OnStopRecording -= SetupSlider;
            TimeReverseController.OnFrameRewindAction -= DisplayFrameAndRefreshSlider;
        }

        private void OnFrameUpdateAction(int frameCounter) => frameCounterText.text = $"Frame: {frameCounter}";
        
        private void SetupSlider()
        {
            timelineSlider.minValue = 0;
            timelineSlider.maxValue = TimeReverseController.Instance.RecordedFrames - 1;

            timelineSlider.value = timelineSlider.maxValue;
        }

        private void SliderValueChange(float value) => TimeReverseController.Instance.SetToFrame((int)value);

        private void DisplayFrameAndRefreshSlider(int currentFrame, int totalFrame)
        {
            DisplayFrame(currentFrame, totalFrame);
            SetSliderValue(currentFrame);
        }
        
        private void DisplayFrame(int currentFrame, int totalFrame) => frameCounterText.text = $"Frames: {currentFrame + 1}/{totalFrame}";
        private void SetSliderValue(int value) => timelineSlider.value = value;
    }
}
