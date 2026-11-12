namespace PL.Models
{
    public class StudentPL
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Course { get; set; }
        public string StudentId { get; set; }
        public bool ServedInArmy { get; set; }
        public string MilitaryId { get; set; }
        
        public override string ToString()
        {
            string army = ServedInArmy ? $"(Служив, квиток: {MilitaryId})" : "(Не служив)";
            return $"{FirstName} {LastName}, Курс: {Course}, ID: {StudentId} {army}";
        }
    }
}