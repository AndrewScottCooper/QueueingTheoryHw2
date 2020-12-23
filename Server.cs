using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queueing_Theory_Homework_2
{
    public class Server
    {
        public Customer CurrentCustomer; 

        public Server()
        {

        }

        public enum State
        {
            idle,
            busy
        }
    }
}
