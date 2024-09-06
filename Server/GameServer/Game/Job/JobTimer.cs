﻿using System;
using System.Collections.Generic;
using System.Text;
using ServerCore;

namespace GameServer
{
    struct JobTimerElem : IComparable<JobTimerElem>
    {
        public long execTick; // 실행 시간
        public IJob job;

        public int CompareTo(JobTimerElem other)
        {
            return (int)(other.execTick - execTick);
        }
    }

    public class JobTimer
    {
        PriorityQueue<JobTimerElem> _pq = new PriorityQueue<JobTimerElem>();
        object _lock = new object();

        public int Count { get { lock (_lock) { return _pq.Count; } } }

        public void Push(IJob job, int tickAfter = 0)
        {
            JobTimerElem jobElement;
            jobElement.execTick = Utils.TickCount + tickAfter;
            jobElement.job = job;

            lock (_lock)
            {
                _pq.Push(jobElement);
            }
        }

        public void Flush()
        {
            while (true)
            {
                long now = Utils.TickCount;

                JobTimerElem jobElement;

                lock (_lock)
                {
                    if (_pq.Count == 0)
                        break;

                    jobElement = _pq.Peek();
                    if (jobElement.execTick > now)
                        break;

                    _pq.Pop();
                }

                jobElement.job.Execute();
            }
        }
    }
}
