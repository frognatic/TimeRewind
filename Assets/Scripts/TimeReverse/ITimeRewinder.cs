namespace TimeReverse
{
    public interface ITimeRewinder
    {
        public void StartRewind();
        public void RewindFrame(int frame, bool frameByFrame = false);
        public void PauseRewind();
    }
}
