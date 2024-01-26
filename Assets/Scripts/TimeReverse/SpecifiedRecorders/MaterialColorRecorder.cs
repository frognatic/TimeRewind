using System.Collections.Generic;
using UnityEngine;

namespace TimeReverse.SpecifiedRecorders
{
    public class MaterialColorRecorder : TimeRecorder
    {
        private readonly List<Color> recordedColors = new();
        private IMaterialColor materialColor;

        protected override void InitializeAction() => materialColor = GetComponent<IMaterialColor>();

        protected override void StartRecordingAction() => recordedColors.Clear();

        protected override void RecordingAction() => recordedColors.Add(materialColor.GetMaterialColor());

        protected override void StopRecordingAction() {}

        protected override void StartRewindAction() {}

        protected override void RewindAction(int frame)
        {
            Color colorToSet = recordedColors[frame];
            materialColor.SetMaterialColor(colorToSet);
        }

        protected override void StopRewindAction() {}
    }
}
