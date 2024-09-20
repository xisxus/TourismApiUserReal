namespace DataAccessLayer.Entites.Employees
{
    public class EmployeeBank
    {
        public int EmployeeBankID { get; set; }         
        public int EmployeeID { get; set; }             
        public string BankName { get; set; }             
        public string AccountNumber { get; set; }              
        public string BranchName { get; set; }         
        public string AccountType { get; set; }      
        public DateTime CreatedAt { get; set; }     
        public DateTime UpdatedAt { get; set; }
        public Employee Employee { get; set; }
        public bool IsDeleted { get; set; }
    }
}