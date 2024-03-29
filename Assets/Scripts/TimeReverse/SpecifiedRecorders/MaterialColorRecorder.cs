using UnityEngine;

namespace TimeReverse.SpecifiedRecorders
{
    public class MaterialColorRecorder : TimeRecorder
    {
        private readonly FrameContainer<Color> colorsRecorder = new();
        private IMaterialColor materialColor;

        protected override void InitializeAction() => materialColor = GetComponent<IMaterialColor>();

        protected override void StartRecordingAction() => colorsRecorder.Reset();

        protected override void RecordingAction(int frame) => colorsRecorder.Record(frame, materialColor.GetMaterialColor());

        protected override void StopRecordingAction() {}

        protected override void StartRewindAction() => colorsRecorder.StartRewindAction();

        protected override void RewindAction(int frame, bool frameByFrame)
        {
            Color colorToSet = colorsRecorder.GetRewind(frame, frameByFrame);
            materialColor.SetMaterialColor(colorToSet);
        }

        protected override void StopRewindAction() {}
    }
}
