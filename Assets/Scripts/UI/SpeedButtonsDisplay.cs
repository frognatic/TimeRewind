using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace UI
{
    public class SpeedButtonsDisplay : MonoBehaviour
    {
        [Header("Speed buttons")]
        [SerializeField] private SpeedButton speedButtonPrefab;
        [SerializeField] private Transform speedButtonsContent;
        
        private readonly List<ISpeedData> speedDataList = new()
            { new SpeedData(0.25f), new SpeedData(0.5f), new SpeedData(1f), new SpeedData(2f) };
        private readonly List<SpeedButton> speedButtonsList = new();

        private void Start() => InitSpeedButtons();

        private void InitSpeedButtons()
        {
            Clear();
            foreach (ISpeedData speedData in speedDataList)
            {
                SpeedButton speedButton = Instantiate(speedButtonPrefab, speedButtonsContent);
                speedButton.name = $"Speed Button x{speedData.RewindSpeed}";
                
                speedButton.Init(this, speedData);
                speedButtonsList.Add(speedButton);
            }
        }

        private void Clear()
        {
            speedButtonsList.Clear();
            speedButtonsContent.DestroyImmediateAllChildren();
        }

        public void DeselectAll() => speedButtonsList.ForEach(x => x.Deselect());
    }
}