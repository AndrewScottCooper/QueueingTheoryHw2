using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queueing_Theory_Homework_2
{
    public class MM1C
    {
        //Scalable list of servers 
        public List<Server> Workers = new List<Server>();
        public Event nextArrivalEvent; 
        //Queue of Customers 
        public List<Customer> Customers = new List<Customer>();
        // All Customers in the system
        public List<Customer> CustomersInSystem = new List<Customer>();
        //Number of all customers arrived
        public int NumberOfCustomers = 0; 
        //Queue Stats
        public float averageQueueLength;
        public List<int> LQ;
        public List<int> LS;
        public List<int> WQ;
        public List<int> WS; 

        public MM1C(int numServers)
        {
            for(int i =0; i < numServers; i++)
            {
                Server worker = new Server();
                Workers.Add(worker);
            }
            LQ = new List<int>();
            LS = new List<int>();
            WQ = new List<int>();
            WS = new List<int>();
    } 




    }
}
