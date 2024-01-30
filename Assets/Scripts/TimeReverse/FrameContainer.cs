using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeReverse
{
    public class FrameContainer<T>
    {
        private readonly List<FrameContainerData<T>> containerDataList = new();

        private FrameContainerData<T> currentElement;
        private FrameContainerData<T> currentFrameByFrameElement;
        private int counter;
        
        public void Reset()
        {
            containerDataList.Clear();
            counter = 0;
        }

        public void StartRewindAction()
        {
            currentElement = GetLastElement();
            currentFrameByFrameElement = GetLastElement();
        }

        public void Record(int frame, T element)
        {
            if (containerDataList.Count == 0)
            {
                containerDataList.Add(new FrameContainerData<T>(counter, frame, element));
                counter++;
            }

            else
            {
                FrameContainerData<T> lastElement = GetLastElement();

                if (Equals(lastElement.ContainerElement, element))
                {
                    lastElement.SetFrame(frame);
                    lastElement.SetContainerElement(element);
                    containerDataList[counter - 1] = lastElement;
                }
                else
                {
                    containerDataList.Add(new FrameContainerData<T>(counter, frame, element));
                    counter++;
                }
            }
        }

        public T GetRewind(int frame, bool frameByFrame) => frameByFrame ? GetRewindFrameByFrame(frame) : GetRewind(frame);

        private T GetRewind(int frame)
        {
            for (int i = containerDataList.Count - 1; i >= 0; i--)
            {
                if (containerDataList[i].Frame - 1 != frame) 
                    continue;
                
                currentElement = containerDataList[i];
                break;
            }
            return currentElement.ContainerElement;
        }

        private T GetRewindFrameByFrame(int frame)
        {
            int distanceByFrame = containerDataList.Count;
            for (int i = containerDataList.Count - 1; i >= 0; i--)
            {
                int distanceByFrameTemp = Mathf.Abs(containerDataList[i].Frame - frame);

                if (distanceByFrameTemp > distanceByFrame) continue;
                
                distanceByFrame = distanceByFrameTemp;
                currentFrameByFrameElement = containerDataList[i];

            }
            return currentFrameByFrameElement.ContainerElement;
        }

        private FrameContainerData<T> GetLastElement() => containerDataList.Last();
    }
}