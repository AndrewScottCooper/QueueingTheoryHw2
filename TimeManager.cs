using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Queueing_Theory_Homework_2
{
    public class TimeManager
    {
        //This is used later for random number generations
        Random random = new Random();
        //Set the time when the simulation ends 
        public int MaxTime;
        public QueuesManager QueueManager;
        //This changing to false will trigger the Mainwindow to gather the stats and the UI to update 
        private bool simRunning = true;

        //arival rate
        public double λ;
        //service rate
        public double μ;

        public int mmcNumCompleted = 0;
        public int mm1NumCompleted = 0;

        //Stats for Graphs
        public float mmcLQ;
        public float mmcLS;
        public float mmcWQ;
        public float mmcWS;
        public float mm1LQ;
        public float mm1LS;
        public float mm1WQ;
        public float mm1WS;

        //cCount is parsed from the user to here to the simmanger
        public TimeManager(int desiredTime, int cCount, int lambda, int mu)
        {
            MaxTime = desiredTime;
            QueueManager = new QueuesManager();
            QueueManager.CreateQueues(cCount);
            λ = lambda;
            μ = mu; 
        }

        //This is the time loop that drives my entire simulation
        //Should probably simply be called Update 
        public void TheUnstoppableMarchOfTime()
        {
            //Init an arrival for both MMC and MM1 Queues 
            QueueManager.myMMCQueue.nextArrivalEvent = new Event(Event.EventType.arrival,  (int)NextArrivalEventTime());
            for(int i =0; i < QueueManager.myMM1QueueGroup.Count; i++)
            {
                QueueManager.myMM1QueueGroup[i].nextArrivalEvent = new Event(Event.EventType.arrival, (int)NextArrivalEventTime());
            }

          //  MessageBox.Show("Sim Started First Arrivals planned");
            for (int cnt = 0; cnt < MaxTime; cnt++)
            {
                //Check if MMC has an arrival event, if yes add it and schedule next arrival
                if(QueueManager.myMMCQueue.nextArrivalEvent.executionTime == cnt)
                {
                    QueueManager.myMMCQueue.NumberOfCustomers++; 
                    //customer class takes the current time as an input to trak wait time
                    Customer tempCust = new Customer(cnt);
                    //Use this as a customer ID to track for generating stats
                    tempCust.custNum = QueueManager.myMMCQueue.NumberOfCustomers;
                    //Add the customer to the system
                    QueueManager.myMMCQueue.CustomersInSystem.Add(tempCust);
                    //Add the last customer added in to the system to the queue the one on top is always the newest
                    QueueManager.myMMCQueue.Customers.Add(QueueManager.myMMCQueue.CustomersInSystem.Last());
                    //Replace current event with the next one   Time for next event is cnt + random arrival time
                    QueueManager.myMMCQueue.nextArrivalEvent = new Event(Event.EventType.arrival,  cnt + (int)NextArrivalEventTime());
                }
                //add MMC's Current number of waiting customers to the Lq list
                QueueManager.myMMCQueue.LQ.Add(QueueManager.myMMCQueue.Customers.Count);
                //Minus all from system and all in queue for LS
                QueueManager.myMMCQueue.LS.Add(QueueManager.myMMCQueue.CustomersInSystem.Count - QueueManager.myMMCQueue.Customers.Count);


                //Check MMC Queue severs' for customer departure events
                //move next cust to queue and add service time start to get the wait time in queue
                for (int worker = 0; worker < QueueManager.myMMCQueue.Workers.Count; worker++)
                {
                    //If the server is empty move the customer over and set the serveice time start
                    //Generate a departure event Set service start time and use those two times to measure service time
                    if(QueueManager.myMMCQueue.Workers[worker].CurrentCustomer != null)
                    {
                        //The server has a customer so check if this is its departure event time
                        if(QueueManager.myMMCQueue.Workers[worker].CurrentCustomer.serviceOverEvent.executionTime == cnt)
                        {
                            // Update
                            // Add to completed data set for stats
                            //Remove the customer from the system
                            //Update number of completetions
                            for (int index = 0; index < QueueManager.myMMCQueue.CustomersInSystem.Count; index++)
                            {
                                if(QueueManager.myMMCQueue.CustomersInSystem[index].custNum == QueueManager.myMMCQueue.Workers[worker].CurrentCustomer.custNum)
                                {
                                    //WQ added to the list based on when it arrived and when it started service
                                    //Service start - arrival time = wait time
                                    QueueManager.myMMCQueue.WQ.Add(QueueManager.myMMCQueue.CustomersInSystem[index].serviceStartTime - 
                                        QueueManager.myMMCQueue.CustomersInSystem[index].timeArrived);
                                    //WS Average time spent in service is when service started to now when the customer is removed
                                    QueueManager.myMMCQueue.WS.Add(cnt - QueueManager.myMMCQueue.CustomersInSystem[index].serviceStartTime);
                                    QueueManager.myMMCQueue.CustomersInSystem.RemoveAt(index);
                                }
                            }
                            QueueManager.myMMCQueue.Workers[worker].CurrentCustomer = null;
                            mmcNumCompleted++;
                            
                        }
                    }
                    // The server is empty so shift a customer over if one exist
                    else
                    {
                        if(QueueManager.myMMCQueue.Customers.Count > 0)
                        {
                            QueueManager.myMMCQueue.Workers[worker].CurrentCustomer = QueueManager.myMMCQueue.Customers[0];
                            QueueManager.myMMCQueue.Customers.RemoveAt(0); 
                            //Find the higher level refernce of the customer in the main list
                            for(int index = 0; index < QueueManager.myMMCQueue.CustomersInSystem.Count; index++)
                            {
                                //confirm it with the customer number
                               if(QueueManager.myMMCQueue.Workers[worker].CurrentCustomer.custNum == QueueManager.myMMCQueue.CustomersInSystem[index].custNum)
                                {
                                    //set the service start time for its exit stats
                                    QueueManager.myMMCQueue.CustomersInSystem[index].serviceStartTime = cnt;
                                    //create the service finished event to exit the system
                                    QueueManager.myMMCQueue.CustomersInSystem[index].serviceOverEvent = new Event(Event.EventType.departure, cnt + (int)ServiceCompleteTime());
                                   
                                  //  continue; // this should break from the current forloop and keep the main loop going
                                }
                            }
                        }

                    }
                }


                //***********************//

                // MM1 Queue Section

                //**********************//
                QueueManager.MM1LQ.Add(0);
                QueueManager.MM1LS.Add(0);
                for (int u = 0; u < QueueManager.myMM1QueueGroup.Count; u++)
                {
                    //Adds each number of customers for each queue to C MM1's LQ
                    QueueManager.MM1LQ[QueueManager.MM1LQ.Last()] += QueueManager.myMM1QueueGroup[u].Customers.Count;
                    //Adds the number of customers in service across the system
                    QueueManager.MM1LQ[QueueManager.MM1LQ.Last()] += (QueueManager.myMM1QueueGroup[u].CustomersInSystem.Count - QueueManager.myMM1QueueGroup[u].Customers.Count);
                }
                //For loop goes through each MM1 queue the user generated 
                for(int currQueue =0; currQueue < QueueManager.myMM1QueueGroup.Count; currQueue++)
                {
                    //Check if any MM1 queue has an arrival, and so add to queue and schedule next arrival time for that queue
                    if(QueueManager.myMM1QueueGroup[currQueue].nextArrivalEvent.executionTime == cnt)
                    {
                        QueueManager.myMM1QueueGroup[currQueue].NumberOfCustomers++;
                        //customer class takes the current time as an input to trak wait time
                        Customer tempCust = new Customer(cnt);
                        //Use this as a customer ID to track for generating stats
                        tempCust.custNum = QueueManager.myMM1QueueGroup[currQueue].NumberOfCustomers;
                        //Add the customer to the system
                        QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem.Add(tempCust);
                        //Add the last customer added in to the system to the queue the one on top is always the newest
                        QueueManager.myMM1QueueGroup[currQueue].Customers.Add(QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem.Last());
                        //Replace current event with the next one   Time for next event is cnt + random arrival time
                        QueueManager.myMM1QueueGroup[currQueue].nextArrivalEvent = new Event(Event.EventType.arrival, cnt + (int)NextArrivalEventTime());
                    }
                    //Check all MM1 Queue customers for events and the sever's customer 
                    if (QueueManager.myMM1QueueGroup[currQueue].worker.CurrentCustomer != null)
                    {
                        if(QueueManager.myMM1QueueGroup[currQueue].worker.CurrentCustomer.serviceOverEvent.executionTime == cnt){

                            // Update
                            // Add to completed data set for stats
                            //Remove the customer from the system
                            //Update number of completetions
                            for (int index = 0; index < QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem.Count; index++)
                            {
                                if (QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[index].custNum == QueueManager.myMM1QueueGroup[currQueue].worker.CurrentCustomer.custNum)
                                {
                                    //WQ added to the list based on when it arrived and when it started service
                                    //Service start - arrival time = wait time
                                    QueueManager.MM1WQ.Add(QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[index].serviceStartTime -
                                        QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[index].timeArrived);
                                    //WS Average time spent in service is when service started to now when the customer is removed
                                    QueueManager.MM1WS.Add(cnt - QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[index].serviceStartTime);

                                    QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem.RemoveAt(index);
                                }
                            }
                            QueueManager.myMM1QueueGroup[currQueue].worker.CurrentCustomer = null;
                            mm1NumCompleted++;

                        }
                    }
                    //Else the server is idle and no one is in it
                    else
                    {
                        if (QueueManager.myMM1QueueGroup[currQueue].Customers.Count > 0)
                        {
                            //Set service start time
                            QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[0].serviceStartTime = cnt;
                            //create the service finished event to exit the system
                            QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[0].serviceOverEvent = new Event(Event.EventType.departure, cnt + (int)ServiceCompleteTime());
                            QueueManager.myMM1QueueGroup[currQueue].worker.CurrentCustomer = QueueManager.myMM1QueueGroup[currQueue].CustomersInSystem[0];
                            QueueManager.myMM1QueueGroup[currQueue].Customers.RemoveAt(0);
                            
                            
                        }
                    }


                }



            }

            simRunning = false;
            CalculateStats();
            
        }
        public double NextArrivalEventTime()
        {
            //Set an upper and lower bounds
            //pull two random numbers one high and one low
            //final random number based on those two

            //in C# random.next lets you pick between two integers, but NextDouble takes no inputs and only returns a random double between 0-1
            //So add 1 and multply by λ to multply it by a random percentage increase, and same of the lower bound, reducing it by a certain percentage
            double upperbound = (( 1 + random.NextDouble()) * λ);
            double lowerbound = ((random.NextDouble()) * λ);
            //Multiplies our random number by the upper bound minus the lower bound and then adds the lowerbound to ensure the minimum 
            return random.NextDouble() * (upperbound - lowerbound) + lowerbound;
        }
        //Same as above method, but with μ for service time
        public double ServiceCompleteTime()
        {
            double upperbound = ((1 + random.NextDouble()) * μ);
            double lowerbound = ((random.NextDouble()) * μ);
            return random.NextDouble() * (upperbound - lowerbound) + lowerbound;
        }
        public void CalculateStats()
        {
           //Calcuates the final MMC LQ to display on the graph
          for(int i = 0; i < QueueManager.myMMCQueue.LQ.Count; i++)
            {
                mmcLQ += QueueManager.myMMCQueue.LQ[i];
            }
            
            //Calcuates the final MMC LS to display on the graph
            for (int i = 0; i < QueueManager.myMMCQueue.LS.Count; i++)
            {
                mmcLS += QueueManager.myMMCQueue.LS[i];
            }
            

            //Calcuates the final MMC WQ to display on the graph
            for (int i = 0; i < QueueManager.myMMCQueue.WQ.Count; i++)
            {
                mmcWQ += QueueManager.myMMCQueue.WQ[i];
            }
            

            //Calcuates the final MMC WS to display on the graph
            for (int i = 0; i < QueueManager.myMMCQueue.WS.Count; i++)
            {
                mmcWS += QueueManager.myMMCQueue.WS[i];
            }

            mmcLQ /= QueueManager.myMMCQueue.LQ.Count;
            mmcLS /= QueueManager.myMMCQueue.LS.Count;
            mmcWQ /= QueueManager.myMMCQueue.WQ.Count;
            mmcWS /= QueueManager.myMMCQueue.WS.Count;

            //MM1 BELOW


            //Calcuates the final MM1 LQ to display on the graph
            for (int i = 0; i < QueueManager.MM1LQ.Count; i++)
            {
                mm1LQ += QueueManager.MM1LQ[i];
            }
            
            //Calcuates the final MM1 LS to display on the graph
            for (int i = 0; i < QueueManager.MM1LS.Count; i++)
            {
                mm1LS += QueueManager.MM1LS[i];
            }
            

            //Calcuates the final MM1 WQ to display on the graph
            for (int i = 0; i < QueueManager.MM1WQ.Count; i++)
            {
                mm1WQ += QueueManager.MM1WQ[i];
            }
            

            //Calcuates the final MM1 WS to display on the graph
            for (int i = 0; i < QueueManager.MM1WS.Count; i++)
            {
                mm1WS += QueueManager.MM1WS[i];
            }

            mm1LQ /= QueueManager.MM1LQ.Count;
            mm1LS /= QueueManager.MM1LS.Count;
            mm1WQ /= QueueManager.MM1WQ.Count;
            mm1WS /= QueueManager.MM1WS.Count;

           // MessageBox.Show(mm1WS.ToString()); 
        }




    }
}
