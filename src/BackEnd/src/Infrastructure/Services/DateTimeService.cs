using GrowATree.Application.Common.Interfaces;
using System;

namespace GrowATree.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
