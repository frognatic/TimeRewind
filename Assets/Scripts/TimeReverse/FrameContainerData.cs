
namespace TimeReverse
{
    public struct FrameContainerData<T>
    {
        public int Id { get; private set; }
        public int Frame { get; private set; }
        public T ContainerElement { get; private set; }

        public FrameContainerData(int id, int frame, T containerElement)
        {
            Id = id;
            Frame = frame;
            ContainerElement = containerElement;
        }

        public void SetFrameAndContainer(int frame, T containerElement)
        {
            SetFrame(frame);
            SetContainerElement(containerElement);
        }

        private void SetFrame(int frame) => Frame = frame;
        private void SetContainerElement(T containerElement) => ContainerElement = containerElement;
    }
}