using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeReverse
{
    public class TimeReverseController : MonoSingleton<TimeReverseController>
    {
        public static event Action<int> OnFrameUpdateAction;
    
        private List<TimeRecorder> timeRecorders = new();

        private int recordedFrames;
        private float rewindFrames;

        public float RewindSpeed { get; private set; } = 1f;

        private bool isRecording;
        private bool isRewinding;
    
        private void Start()
        {
            FillRecorders();
            InitRecorders();
        }

        private void FillRecorders() => timeRecorders = GetComponentsInChildren<TimeRecorder>().ToList();

        private void InitRecorders()
        {
            foreach (ITimeInitializer initializer in timeRecorders) 
                initializer.Initialize();
        }

        private void Update()
        {
            TryRecordFrames();
            TryRewindFrames();
        }

        private void TryRecordFrames()
        {
            if (!isRecording) return;
        
            IncreaseFrames();
            RecordFrames();
        }

        private void IncreaseFrames()
        {
            recordedFrames++;
            OnFrameUpdateAction?.Invoke(recordedFrames);
        }

        private void RecordFrames()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.Record(recordedFrames);
        }

        private void TryRewindFrames()
        {
            if (!isRewinding || IsRewindFinished()) return;
        
            RestoredFrames();
            DecreaseFramesToRewindBySpeed();
        }

        private bool IsRewindFinished() => rewindFrames <= 0;
    
        private void RestoredFrames()
        {
            foreach (ITimeRewinder recorder in timeRecorders)
            {
                int frames = (int)(rewindFrames - 1);
                recorder.RewindFrame(frames);
            }
        }

        private void DecreaseFramesToRewindBySpeed() => rewindFrames -= Instance.RewindSpeed;

        #region UI Calls

        public void StartRecording()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.StartRecording();

            ResetRecordingStatus();
        }

        private void ResetRecordingStatus()
        {
            isRecording = true;
            rewindFrames = 0;
            recordedFrames = 0;
        }
    
        public void StopRecording()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.StopRecording();
        
            isRecording = false;
        }

        public void StartRewind()
        {
            foreach (ITimeRewinder rewinder in timeRecorders) 
                rewinder.StartRewind();
        
            isRewinding = true;
            rewindFrames = recordedFrames;
        }

        public void StopRewind()
        {
            foreach (ITimeRewinder rewinder in timeRecorders) 
                rewinder.PauseRewind();
        
            isRewinding = false;
        }

        public void SetRewindSpeed(float speedToSet) => RewindSpeed = speedToSet;

        #endregion
    }
}
