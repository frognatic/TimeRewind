﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeReverse
{
    public class FrameContainer<T>
    {
        private readonly List<FrameContainerData<T>> containerDataList = new();

        private FrameContainerData<T> currentElement;
        private int counter;
        
        public void Reset()
        {
            containerDataList.Clear();
            counter = 0;
        }

        public void StartRewindAction() => currentElement = GetLastElement();

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

        public T GetRewind(int frame)
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

        private FrameContainerData<T> GetLastElement() => containerDataList.Last();
    }

    [Serializable]
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

        public void SetFrame(int frame) => Frame = frame;
        public void SetContainerElement(T containerElement) => ContainerElement = containerElement;
    }
}