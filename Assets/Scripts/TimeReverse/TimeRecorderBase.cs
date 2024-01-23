using UnityEngine;

public abstract class TimeRecorderBase : MonoBehaviour
{
    public abstract bool Finished { get; protected set; }
    public abstract void RestoreFrame(int frame);
}

public abstract class TimeRecorder : TimeRecorderBase
{
    protected void Update()
    {
        Record();
    }

    public abstract void StartRecording();
    public abstract void StopRecording();

    protected abstract void Record();
    protected abstract void StopRestoring();
}
