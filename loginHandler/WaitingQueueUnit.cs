using System;
using System.Collections;
using DarkRift;

namespace loginHandler
{
    public class WaitingQueueUnit
    {
        //The connection server reference that is waiting
        private ConnectionService con;

        //flag To know if the Queue unit can be dequeued
        private bool canBeDequeued;

        //method to checkif the unit is able to be dequeued
        public bool checkDequeue()
        {
            return canBeDequeued;
        }

        //Method to get the connection reference
        public ConnectionService getConnectionRef()
        {
            return con;
        }

        //Method to set if the unit can be dequeued
        public void setDequeue()
        {
            canBeDequeued = true;
        }

        public WaitingQueueUnit( ConnectionService con )
        {
            //setting the connection reference
            this.con = con;
            //Since its being created and thus just entering the queue the default value is false
            this.canBeDequeued = false;
        }
    }
}
