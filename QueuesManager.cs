using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queueing_Theory_Homework_2
{
    public class QueuesManager
    {
        // List of all exisiting MM1 Queues in my Solution
        //μόνος means single, as in single sever
        public List<MM1Queue> myMM1QueueGroup = new List<MM1Queue>();
        //εργαζόμενοι means workers in greek haha funny guy, I am really enjoying UTF-8 varible names too much
        //this is my MM1C queue
        public MM1C myMMCQueue;
        // Changed to back to english 
        //Basically to remember which is which MM1C is the long name one and MM1 is the short one

        public QueuesManager()
        {

        }


        /// <summary>
        /// Initialize my queues based on C
        /// MMC is created frist with C number of servers
        /// Then the MM1 Queues are created and added to a list
        /// </summary>
        /// 
        public List<int> MM1LQ = new List<int>();
        public List<int> MM1LS = new List<int>();
        public List<int> MM1WQ = new List<int>();
        public List<int> MM1WS = new List<int>();
        public void CreateQueues(int cCount)
        {        
            myMMCQueue = new MM1C(cCount);
            //Populate C number of M/M/1 Queues
            for (int i = 0; i < cCount; i++)
            {
                MM1Queue mm1Queue = new MM1Queue();
                myMM1QueueGroup.Add(mm1Queue);
            }
        }
    }
}
