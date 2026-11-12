using System.Collections.Generic;
using DAL;
using DAL.Models;
using DAL.Providers;
using BLL.Models;
using BLL.Exceptions;

namespace BLL
{
    public class EntityService
    {
        private readonly EntityContext<StudentDAL> _context;

        public EntityService()
        {
            _context = new EntityContext<StudentDAL>();
        }

        public void SetSerializationType(string type)
        {
            switch (type.ToLower())
            {
                case "xml": _context.SetProvider(new XmlProvider<StudentDAL>()); break;
                case "json": _context.SetProvider(new JsonProvider<StudentDAL>()); break;
                case "custom": _context.SetProvider(new CustomProvider<StudentDAL>()); break;
                case "binary": 
                default: _context.SetProvider(new BinaryProvider<StudentDAL>()); break;
            }
        }

        public void SaveStudents(string filePath, List<StudentBLL> bllStudents)
        {
            if (bllStudents == null || bllStudents.Count == 0)
                throw new StudentValidationException("Немає даних для збереження.");

            List<StudentDAL> dalStudents = new List<StudentDAL>();
            foreach (var s in bllStudents)
            {
                dalStudents.Add(new StudentDAL
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Course = s.Course,
                    StudentId = s.StudentId,
                    ServedInArmy = s.ServedInArmy,
                    MilitaryId = s.MilitaryId
                });
            }
            _context.Save(filePath, dalStudents);
        }

        public List<StudentBLL> LoadStudents(string filePath)
        {
            var dalStudents = _context.Load(filePath);
            List<StudentBLL> bllStudents = new List<StudentBLL>();
            
            foreach (var s in dalStudents)
            {
                bllStudents.Add(new StudentBLL
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Course = s.Course,
                    StudentId = s.StudentId,
                    ServedInArmy = s.ServedInArmy,
                    MilitaryId = s.MilitaryId
                });
            }
            return bllStudents;
        }

        // Завдання за варіантом 4: Обчислити кількість студентів 5-го курсу, які служили в армії
        public List<StudentBLL> GetFifthCourseArmyStudents(List<StudentBLL> allStudents)
        {
            List<StudentBLL> result = new List<StudentBLL>();
            foreach (var s in allStudents)
            {
                if (s.Course == 5 && s.ServedInArmy)
                {
                    result.Add(s);
                }
            }
            return result;
        }
    }
}