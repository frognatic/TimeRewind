using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeReverse
{
    public class TimeReverseController : MonoSingleton<TimeReverseController>
    {
        public static event Action<int> OnFrameUpdateAction;
        public static event Action<int, int> OnFrameRewindAction;
        public static event Action OnStopRecording;
    
        private List<TimeRecorder> timeRecorders = new();

        private float rewindFrames;
        private int currentFrame;

        public int RecordedFrames { get; private set; }

        private float RewindSpeed { get; set; } = 1f;

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
            RecordedFrames++;
            OnFrameUpdateAction?.Invoke(RecordedFrames);
        }

        private void RecordFrames()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.Record(RecordedFrames);
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
            int frames = GetAllRewindFrames;
            
            foreach (ITimeRewinder recorder in timeRecorders) 
                recorder.RewindFrame(frames);

            OnFrameRewindAction?.Invoke(frames, RecordedFrames);
        }

        public void SetToFrame(int frame)
        {
            foreach (ITimeRewinder recorder in timeRecorders) 
                recorder.RewindFrame(frame, frameByFrame: true);

            currentFrame = frame;
            
            OnFrameRewindAction?.Invoke(frame, RecordedFrames);
        }

        private void DecreaseFramesToRewindBySpeed() => rewindFrames -= RewindSpeed;

        #region UI Calls

        public void Record()
        {
            if (isRecording) 
                StopRecording();
            else
                StartRecording();
        }
        
        private void StartRecording()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.StartRecording();

            ResetRecordingStatus();
        }

        private void ResetRecordingStatus()
        {
            isRecording = true;
            rewindFrames = 0;
            RecordedFrames = 0;
        }
    
        private void StopRecording()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.StopRecording();
        
            isRecording = false;
            currentFrame = GetAllRewindFrames;
            
            OnStopRecording?.Invoke();
        }

        public void GoToFirstFrame() => SetToFrame(0);

        public void GoToLastFrame()
        {
            int frames = RecordedFrames;
            SetToFrame(frames);
        }

        public void GoToPreviousFrame()
        {
            currentFrame--;
            SetToFrame(ClampRecordedFrame(currentFrame));
        }

        public void GoToNextFrame()
        {
            currentFrame++;
            SetToFrame(ClampRecordedFrame(currentFrame));
        }

        private int ClampRecordedFrame(int frame) => Mathf.Clamp(frame, 0, RecordedFrames - 1);

        public void StartRewind()
        {
            foreach (ITimeRewinder rewinder in timeRecorders) 
                rewinder.StartRewind();
        
            isRewinding = true;
            rewindFrames = RecordedFrames;
        }

        public void StopRewind()
        {
            foreach (ITimeRewinder rewinder in timeRecorders) 
                rewinder.PauseRewind();
        
            isRewinding = false;
        }

        public void SetRewindSpeed(float speedToSet) => RewindSpeed = speedToSet;
        private int GetAllRewindFrames => (int)(rewindFrames - 1);

        #endregion
    }
}
