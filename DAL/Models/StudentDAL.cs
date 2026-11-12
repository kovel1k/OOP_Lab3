using System;
using System.Runtime.Serialization;

namespace DAL.Models
{
    [Serializable]
    public class StudentDAL : ISerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Course { get; set; }
        public string StudentId { get; set; }
        public bool ServedInArmy { get; set; }
        public string MilitaryId { get; set; }

        public StudentDAL() { }

        // Конструктор для Custom Serialization
        protected StudentDAL(SerializationInfo info, StreamingContext context)
        {
            FirstName = info.GetString("FirstName");
            LastName = info.GetString("LastName");
            Course = info.GetInt32("Course");
            StudentId = info.GetString("StudentId");
            ServedInArmy = info.GetBoolean("ServedInArmy");
            MilitaryId = info.GetString("MilitaryId");
        }

        // Метод для Custom Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FirstName", FirstName);
            info.AddValue("LastName", LastName);
            info.AddValue("Course", Course);
            info.AddValue("StudentId", StudentId);
            info.AddValue("ServedInArmy", ServedInArmy);
            info.AddValue("MilitaryId", MilitaryId);
        }
    }
}