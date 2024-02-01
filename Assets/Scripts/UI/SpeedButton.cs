using TimeReverse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpeedButton : MonoBehaviour
    {
        [SerializeField] private Image buttonBackgroundImage;
        [SerializeField] private TextMeshProUGUI speedText;

        [Header("Colors")] 
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color deselectedColor;
        
        private SpeedButtonsDisplay speedButtonsDisplay;
        private ISpeedData speedData;

        public void Init(SpeedButtonsDisplay speedButtonsDisplay, ISpeedData speedData)
        {
            this.speedButtonsDisplay = speedButtonsDisplay;
            this.speedData = speedData;

            if (speedData.RewindSpeed == 1f)
                Select();
            else
                Deselect();

            SetSpeedText();
        }
        
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
