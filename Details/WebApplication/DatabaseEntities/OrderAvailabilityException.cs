using System;
using System.Runtime.Serialization;
using WebApplication.Controllers.ControllersEntities;

namespace WebApplication.Controllers // think about it: где расположить?
{
    [Serializable]
    internal class OrderAvailabilityException : Exception
    {
        public OrderItem[] ProblemItems { get; set; }

        public OrderAvailabilityException(OrderItem[] problemItems)
        {
            ProblemItems = problemItems;
        }

        public OrderAvailabilityException(string message) : base(message)
        {
        }

        public OrderAvailabilityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderAvailabilityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}