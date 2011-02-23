using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace java.asap.core
{
    public class TimerController : TimerSource
    {
        public const int MINIMUM_HEAP_SIZE = 32;

        public const int UNSCHEDULED = -1;

        public const long TIME_BEFORE_CREATION = -1;

        public Timer[] timers;

        public long[] times;

        public int size;

        public int capacity;

        public long timesweep;

        public long instant;

        public TimerController()
        {
            size = 0;
            timesweep = instant = 0;
            Reallocate(MINIMUM_HEAP_SIZE);
        }

        public virtual void Reallocate(int changedCapacity)
        {
            Debug.Assert(changedCapacity >= (MINIMUM_HEAP_SIZE));
            Debug.Assert((changedCapacity & (changedCapacity - 1)) == 0);
            Timer[] newTimers = new Timer[changedCapacity];
            long[] newTimes = new long[changedCapacity];
            if ((size) > 0)
            {
                Array.Copy(timers, 1, newTimers, 1, size);
                Array.Copy(times, 1, newTimes, 1, size);
            }
            timers = newTimers;
            times = newTimes;
            capacity = changedCapacity;
        }

        public virtual void RegisterTimer(Timer timer)
        {
            Debug.Assert((timer != null) && (!(timer.IsScheduled())), "timer null or already scheduled");
            Debug.Assert(((timer.delay) > 0) || (((timer.delay) == 0) && (!(timer.repeated))), "incorrect timer delay");
            if (((size) + 1) == (capacity))
                Reallocate(((capacity) * 2));

            ++(size);
            timers[size] = timer;
            times[size] = (instant) + (timer.delay);
            timer.heapPosition = size;
            FloatUp(size);
        }

        public virtual void UnregisterTimer(Timer timer)
        {
            Debug.Assert((timer != null) && (timer.IsScheduled()), "timer null or not already scheduled");
            int position = timer.heapPosition;
            times[position] = TIME_BEFORE_CREATION;
            FloatUp(position);
            Debug.Assert((timer.heapPosition) == 1);
            timer.heapPosition = UNSCHEDULED;
            Debug.Assert((size) >= 1);
            if ((size) == 1)
            {
                size = 0;
                timers[1] = null;
            }
            else
            {
                timers[1] = timers[size];
                times[1] = times[size];
                timers[(size)--] = null;
                timers[1].heapPosition = 1;
                if (((size) < ((capacity) / 3)) && (((capacity) / 2) >= (MINIMUM_HEAP_SIZE)))
                    Reallocate(((capacity) / 2));

                DiveDown(1);
            }
        }

        public virtual void FloatUp(int index)
        {
            while (index > 1)
            {
                int parentIndex = index >> 1;
                if ((times[parentIndex]) <= (times[index]))
                    break;

                Swap(parentIndex, index);
                index = parentIndex;
            }
        }

        public virtual void DiveDown(int index)
        {
            for (; ; )
            {
                int minIndex = index;
                if (((index * 2) <= (size)) && ((times[(index * 2)]) < (times[minIndex])))
                    minIndex = index * 2;

                if (((index * 2) <= ((size) - 1)) && ((times[((index * 2) + 1)]) < (times[minIndex])))
                    minIndex = (index * 2) + 1;

                if (minIndex == index)
                    return;

                Swap(minIndex, index);
                index = minIndex;
            }
        }

        public virtual void Swap(int index1, int index2)
        {
            long longXchg = times[index1];
            times[index1] = times[index2];
            times[index2] = longXchg;
            Timer timerXchg = timers[index1];
            timers[index1] = timers[index2];
            timers[index2] = timerXchg;
            timers[index1].heapPosition = index1;
            timers[index2].heapPosition = index2;
        }

        public virtual void CleanupDeadTimers()
        {
            bool iterFlag = true;
            while (iterFlag)
            {
                int i;
                for (i = 1; i <= size; ++i)
                {
                    if (!timers[i].listener.IsAlive)
                    {
                        Debug.Assert((timers[i].heapPosition) == i);
                        UnregisterTimer(timers[i]);
                        break;
                    }
                }

                iterFlag = i == size;
            }
        }

        public virtual void Tick(long delta)
        {
            CleanupDeadTimers();
            timesweep += delta;
            while (((size) > 0) && ((times[1]) <= (timesweep)))
            {
                instant = times[1];
                Timer timer = timers[1];
                if (!(timer.repeated))
                    UnregisterTimer(timer);

                TimerListener listener = ((TimerListener)(timer.listener.Target));
                if (listener != null)
                    listener.OnTimer(timer);

                if ((((timer.heapPosition) != (UNSCHEDULED)) && ((instant) == (times[timer.heapPosition]))) && (timer.repeated))
                {
                    times[timer.heapPosition] = (instant) + (timer.delay);
                    DiveDown(timer.heapPosition);
                }
            }
            instant = timesweep;
            CleanupDeadTimers();
        }

        public virtual TimerController GetTimerController()
        {
            return this;
        }

        public virtual void KillTimers()
        {
            for (int i = 0; i < (timers.Length); i++)
            {
                if ((timers[i]) != null)
                    UnregisterTimer(timers[i]);

            }
        }

    }
}