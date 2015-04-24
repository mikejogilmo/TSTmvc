using System;
using System.ComponentModel.DataAnnotations;

namespace TST.Data.EF.Results
{
    class TicketResults
    {
        [Display(Name="Ticket Priority")]
        public int PriorityId { get; set; }

        [Display(Name="Ticket Status")]
        public int StatusId { get; set; }

        [Display(Name="Date Submitted")]
        public DateTime DateSubmitted { get; set; }

        [Display(Name = "Submitted By")]
        public int SubmittedByEmployeeId { get; set; } 

    }
}
