using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queueing_Theory_Homework_2
{
    public class MM1Queue
    {
        public Event nextArrivalEvent;

        public List<Customer> Customers = new List<Customer>();
        // All Customers in the system
        public List<Customer> CustomersInSystem = new List<Customer>();

        //Number of all customers arrived
        public int NumberOfCustomers = 0;



 
        public Server worker;

        public MM1Queue()
        {
           worker = new Server();

        }



    }
}
