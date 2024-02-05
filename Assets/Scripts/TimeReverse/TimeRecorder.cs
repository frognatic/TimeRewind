using UnityEngine;

namespace TimeReverse
{
    public abstract class TimeRecorder : MonoBehaviour, ITimeRecorder, ITimeRewinder, ITimeInitializer
    {
        private bool recordingFinished;
        
        #region ITimeInitializer

        public void Initialize() => InitializeAction();

        #endregion
        
        #region ITimeRecorder

        public void Record(int frame)
        {
            if (recordingFinished)
                RecordingAction(frame);
        }
    
        public void StartRecording()
        {
            StartRecordingAction();
            recordingFinished = true;
        }
    
        public void StopRecording()
        {
            StopRecordingAction();
            recordingFinished = false;
        }

        #endregion

        #region ITimeRewinder

        public void StartRewind() => StartRewindAction();

        public void RewindFrame(int frame, bool frameByFrame)
        {
            if (frame >= 0)
                RewindAction(frame, frameByFrame);
            else
                StopRewindAction();
        }

        public void PauseRewind() {}

        #endregion
    
        #region To override by specified recorder

        protected abstract void InitializeAction();
        
        protected abstract void StartRecordingAction();
        protected abstract void RecordingAction(int frame);
        protected abstract void StopRecordingAction();

        protected abstract void StartRewindAction();
        protected abstract void RewindAction(int frame, bool frameByFrame);
        protected abstract void StopRewindAction();

        #endregion
    }
}
