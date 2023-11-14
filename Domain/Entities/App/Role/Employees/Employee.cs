namespace Domain.Entities.App.Role.Employees;

public class Employee : UserRole
{
    public Employee(string login, uint id, decimal salaryPerHour, DateTime dateOfEmployment) : base(login, id)
    {
        SalaryPerHour = salaryPerHour;
        DateOfEmployment = dateOfEmployment;
    }
    public DateTime DateOfEmployment { get; }
    public int GetWorkExpirienceYears()
    {
        var currentDate = DateTime.Now;
        int yearsWorked = currentDate.Year - DateOfEmployment.Year;

        if (currentDate.Month < DateOfEmployment.Month || (currentDate.Month == DateOfEmployment.Month && currentDate.Day < DateOfEmployment.Day))
        {
            yearsWorked--;
        }
        return yearsWorked;
    }
    public decimal SalaryPerHour { get; set; }

    public int WorkedHoursThisMonth { get; private set; }

    public void IncrementWorkedHours() { WorkedHoursThisMonth++; }

}