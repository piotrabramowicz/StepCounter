﻿namespace StepCounterWebApi.Models
{
    public class Team 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SumOfSteps { get; set; }
    }
}   