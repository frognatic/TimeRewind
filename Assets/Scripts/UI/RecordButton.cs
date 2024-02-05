using TimeReverse;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RecordButton : MonoBehaviour
    {
        [Header("Button settings")]
        [SerializeField] private Image buttonBackgroundImage;
    
        [Header("Colors")] 
        [SerializeField] private Color recordColor;
        [SerializeField] private Color stopRecordColor;

        private void OnEnable()
        {
            TimeReverseController.OnStartRecording += SetRecordColor;
            TimeReverseController.OnStopRecording += SetStopRecordColor;
        }
        
        private void OnDisable()
        {
            TimeReverseController.OnStartRecording -= SetRecordColor;
            TimeReverseController.OnStopRecording -= SetStopRecordColor;
        }
        
        private void Start() => SetStopRecordColor();

        private void SetRecordColor() => SetColor(recordColor);
        private void SetStopRecordColor() => SetColor(stopRecordColor);
        private void SetColor(Color colorToSet) => buttonBackgroundImage.color = colorToSet;
    }
}