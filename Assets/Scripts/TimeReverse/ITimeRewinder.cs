namespace TimeReverse
{
    public interface ITimeRewinder
    {
        public void StartRewind();
        public void RewindFrame(int frame);
        public void PauseRewind();
    }
}
