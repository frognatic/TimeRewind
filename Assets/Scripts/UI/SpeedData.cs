namespace UI
{
    public class SpeedData: ISpeedData
    {
        public float RewindSpeed { get; }
        
        public SpeedData(float speed)
        {
            RewindSpeed = speed;
        }
    }

    public interface ISpeedData
    {
        public float RewindSpeed { get; }
    }
}