using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queueing_Theory_Homework_2
{
    public class Event
    {
        // arrival or depature
        public EventType myType;
        //The time the event is executed at
        public int executionTime;


        public Event(EventType type, int time)
        {
            myType = type;
            executionTime = time; 
        }
        public enum EventType
        {
            arrival,
            departure 
        }

    }
}
