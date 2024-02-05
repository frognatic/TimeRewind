using TimeReverse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedButton : MonoBehaviour
    {
        [Header("Button settings")]
        [SerializeField] private Image buttonBackgroundImage;
        [SerializeField] private TextMeshProUGUI speedText;

        [Header("Colors")] 
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color deselectedColor;
        
        private SpeedButtonsDisplay speedButtonsDisplay;
        private ISpeedData speedData;

        private const float DefaultSpeedValue = 1f;

        public void Init(SpeedButtonsDisplay speedDisplay, ISpeedData speedDataElement)
        {
            speedButtonsDisplay = speedDisplay;
            speedData = speedDataElement;

            if (HasDefaultSpeed())
                Select();
            else
                Deselect();

            SetSpeedText();
        }

        private bool HasDefaultSpeed() => speedData.RewindSpeed == DefaultSpeedValue;
        
        public void Select()
        {
            speedButtonsDisplay.DeselectAll();
            SetRewindSpeed();
            SetColor(selectedColor);
        }

        public void Deselect() => SetColor(deselectedColor);
        private void SetSpeedText() => speedText.text = $"x{speedData.RewindSpeed}";
        private void SetColor(Color colorToSet) => buttonBackgroundImage.color = colorToSet;
        private void SetRewindSpeed() => TimeReverseController.Instance.SetRewindSpeed(speedData.RewindSpeed);
    }
}
