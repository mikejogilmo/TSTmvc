using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TST.Data.EF
{
    #region Employee Status Meta Data

    public class EmployeeStatusMetaData
    {
        [Display(Name = "Status")]
        public int EmployeeStatusId { get; set; }

        [Required(ErrorMessage = "Please enter a status name")]
        [Display(Name = "Employee Status")]
        [StringLength(50, ErrorMessage = "Can not exceed 50 characters")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(EmployeeStatusMetaData))]
    public partial class TST_EmployeeStatuses { }

    #endregion


    #region Department Meta Data

    public class DepartmentMetaData
    {
        [Display(Name="Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter a department name")]
        [Display(Name = "Department Name")]
        [StringLength(100, ErrorMessage = "Can not exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(DepartmentMetaData))]
    public partial class TST_Departments { }

    #endregion


    #region Employees Meta Data

    public class EmployeesMetaData
    {
        [Required(ErrorMessage = "* Required")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,20}$", ErrorMessage = "Please use a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^\+?\(?\d+\)?(\s|\-|\.)?\d{1,3}(\s|\-|\.)?\d{4}$", ErrorMessage = " Use the form (555) 555-5555")]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [RegularExpression(@"^\+?\(?\d+\)?(\s|\-|\.)?\d{1,3}(\s|\-|\.)?\d{4}$", ErrorMessage = " Use the form (555) 555-5555")]
        [Display(Name = "Work Phone")]
        public string WorkPhone { get; set; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        //[Required(ErrorMessage = "* Required")]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        //[Required(ErrorMessage = "* Required")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"(^|\s)(00[1-9]|0[1-9]0|0[1-9][1-9]|[1-6]\d{2}|7[0-6]\d|77[0-2])(-?|[\. ])([1-9]0|0[1-9]|[1-9][1-9])\3(\d{3}[1-9]|[1-9]\d{3}|\d[1-9]\d{2}|\d{2}[1-9]\d)($|\s|[;:,!\.\?])", ErrorMessage = "Please use a proper SSN")]
        [Display(Name = "SSN")]
        public string SSN { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "can not be over 100 characters")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [StringLength(2, ErrorMessage="Please enter a 2 letter abbreviation")]
        [Display(Name = "State")]
        public string State { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
    }

    [MetadataType(typeof(EmployeesMetaData))]
    public partial class TST_Employees
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Address
        {
            get
            {
                return Street + " " + City + ", " + State + " " + Zip;
            }
        }
    }

    #endregion


    #region Support Ticket Meta Data

    public class SupportTicketsMetaData
    {
        [Display(Name = "Submitted By")]
        public int SubmittedByEmployeeId { get; set; }

        [Display(Name="Tech Assigned")]
        public int TechId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Subject")]
        [StringLength(50, ErrorMessage = "Can not exceed 50 characters")]
        public string Subject { get; set; }

        [Display(Name = "Date Submitted")]
        public DateTime DateSubmitted { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Date Closed")]
        public DateTime DateClosed { get; set; }
    }
    [MetadataType(typeof(SupportTicketsMetaData))]
    public partial class TST_SupportTickets { }

    #endregion


    #region Ticket Priorities Meta Data

    public class TicketPrioritiesMetaData
    {
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Priority Level")]
        [StringLength(20, ErrorMessage = "Can not exceed 20 characters")]
        public string Name { get; set; }
    }
    [MetadataType(typeof(TicketPrioritiesMetaData))]
    public partial class TST_TicketPriorities { }


    #endregion


    #region Ticket Statuses Meta Data

    public class TicketStatusesMetaData
    {
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Status")]
        [StringLength(20, ErrorMessage = "Can not exceed 20 characters")]
        public string Name { get; set; }
    }
    [MetadataType(typeof(TicketStatusesMetaData))]
    public partial class TST_TicketStatuses { }

    #endregion


    #region Tech Notes Meta Data

    public class TechNotesMetaData
    {
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Notes")]
        [StringLength(3000, ErrorMessage = "Can not exceed 3000 characters")]
        public string Note { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Date Submitted")]
        public DateTime NoteSubmittedOn { get; set; }
    }
    [MetadataType(typeof(TechNotesMetaData))]
    public partial class TST_TechNotes { }

    #endregion

}
