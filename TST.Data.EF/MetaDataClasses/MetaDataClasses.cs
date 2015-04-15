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


    #region Employee Meta Data

    public class EmployeeMetaData
    {
        [Required(ErrorMessage = "* Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,20}$", ErrorMessage = "Please use a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^\+?\(?\d+\)?(\s|\-|\.)?\d{1,3}(\s|\-|\.)?\d{4}$", ErrorMessage = " Use the form (555) 555-5555")]
        public string CellPhone { get; set; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Status?")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(40, ErrorMessage = "can not be over 40 characters")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"(^|\s)(00[1-9]|0[1-9]0|0[1-9][1-9]|[1-6]\d{2}|7[0-6]\d|77[0-2])(-?|[\. ])([1-9]0|0[1-9]|[1-9][1-9])\3(\d{3}[1-9]|[1-9]\d{3}|\d[1-9]\d{2}|\d{2}[1-9]\d)($|\s|[;:,!\.\?])", ErrorMessage = "Please use a proper SSN")]
        [Display(Name = "Social Security Number")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "can not be over 100 characters")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
    }

    [MetadataType(typeof(EmployeeMetaData))]
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
}
