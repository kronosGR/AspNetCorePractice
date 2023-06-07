﻿using FlightPlanAPI.Models;

namespace FlightPlanAPI.Data
{
    public interface IDatabaseAdapter
    {
        Task<List<FlightPlan>> GetAllFlightPlans();

        Task<FlightPlan> GetFlightPlanById(string flightPlanId);
        Task<bool> FileFlightPlan(FlightPlan flightPlan);
        Task<bool> UpdateFlightPlan(string flightPlanId, FlightPlan flightPlan);
        Task<bool> DeleteFlightPlan(string flightPlanId);
    }
}
