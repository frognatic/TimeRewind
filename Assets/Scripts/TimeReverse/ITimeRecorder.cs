namespace TimeReverse
{
    public interface ITimeRecorder
    {
        public void StartRecording();
        public void Record(int frame);
        public void StopRecording();
    }
}
