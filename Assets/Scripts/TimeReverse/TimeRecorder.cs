using UnityEngine;

public abstract class TimeRecorder : MonoBehaviour
{
    private bool restoringFinished;
    private bool recordingFinished;
    
    protected void Update() => Record();

    private void Record()
    {
        if (recordingFinished)
            RecordingAction();
    }
    
    public void StartRecording()
    {
        StartRecordingAction();
        restoringFinished = false;
        recordingFinished = true;
    }
    
    public void StopRecording() => recordingFinished = false;
    
    public void RestoreFrame(int frame)
    {
        if (frame >= 0)
            RestoreAction(frame);
        else
            StopRestoring();
    }

    private void StopRestoring() => restoringFinished = true;

    #region To implement by specified recorder

    protected abstract void RecordingAction();
    protected abstract void StartRecordingAction();
    protected abstract void RestoreAction(int frame);

    #endregion
}
