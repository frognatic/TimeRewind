using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeReverse
{
    public class TimeReverseController : MonoSingleton<TimeReverseController>
    {
        #region Events
        
        public static event Action<int> OnFrameUpdateAction;
        public static event Action<int, int> OnFrameRewindAction;
        public static event Action OnStartRecording;
        public static event Action OnStopRecording;
        public static event Action OnPlayRewind;
        public static event Action OnPauseRewind;
        
        #endregion
        
        private List<TimeRecorder> timeRecorders = new();

        private float currentRewindFrames;
        private int totalRecordedFrames;

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

        private bool IsRewindFinished() => currentRewindFrames <= 0;
    
        private void RestoredFrames()
        {
            int frames = Mathf.CeilToInt(currentRewindFrames);
            
            foreach (ITimeRewinder recorder in timeRecorders) 
                recorder.RewindFrame(frames);

            OnFrameRewindAction?.Invoke(frames, RecordedFrames);
        }

        public void SetToFrame(int frame)
        {
            foreach (ITimeRewinder recorder in timeRecorders) 
                recorder.RewindFrame(frame, frameByFrame: true);

            currentRewindFrames = frame;
            
            OnFrameRewindAction?.Invoke(frame, RecordedFrames);
        }

        private void DecreaseFramesToRewindBySpeed() => currentRewindFrames -= RewindSpeed;

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
            StopRewind();
            
            OnStartRecording?.Invoke();
        }

        private void ResetRecordingStatus()
        {
            isRecording = true;
            currentRewindFrames = 0;
            RecordedFrames = 0;
        }
    
        private void StopRecording()
        {
            foreach (ITimeRecorder recorder in timeRecorders) 
                recorder.StopRecording();
        
            isRecording = false;
            currentRewindFrames = GetAllRewindFrames;
            
            OnStopRecording?.Invoke();
        }

        public void GoToFirstFrame() => SetToFrame(0);

        public void GoToLastFrame()
        {
            int frames = GetAllRecordedFrames;
            SetToFrame(frames);
        }

        public void GoToPreviousFrame()
        {
            currentRewindFrames--;
            SetToFrame((int)ClampRecordedFrame(currentRewindFrames));
        }

        public void GoToNextFrame()
        {
            currentRewindFrames++;
            SetToFrame((int)ClampRecordedFrame(currentRewindFrames));
        }

        private float ClampRecordedFrame(float frame) => Mathf.Clamp(frame, 0, GetAllRecordedFrames);

        public void Rewind()
        {
            if (isRewinding)
                StopRewind();
            else
                StartRewind();
        }
        
        private void StartRewind()
        {
            foreach (ITimeRewinder rewinder in timeRecorders) 
                rewinder.StartRewind();
        
            isRewinding = true;
            
            OnPlayRewind?.Invoke();
        }

        private void StopRewind()
        {
            foreach (ITimeRewinder rewinder in timeRecorders) 
                rewinder.PauseRewind();
        
            isRewinding = false;
            
            OnPauseRewind?.Invoke();
        }

        public void SetRewindSpeed(float speedToSet) => RewindSpeed = speedToSet;
        private int GetAllRewindFrames => (int)(currentRewindFrames - 1);
        private int GetAllRecordedFrames => RecordedFrames - 1;

        #endregion
    }
}
