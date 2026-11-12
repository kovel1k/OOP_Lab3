using System;
using System.Collections.Generic;
using BLL;
using BLL.Models;
using BLL.Exceptions;
using PL.Models;
using PL.Part1;

namespace PL
{
    public class Menu
    {
        public static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("============= ГОЛОВНЕ МЕНЮ =============");
                Console.WriteLine("1. Частина 1: Демонстрація серіалізації (Клас Коло)");
                Console.WriteLine("2. Частина 2: Трирівнева архітектура (Студенти)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть дію: ");
                
                string choice = Console.ReadLine();
                if (choice == "0") break;
                else if (choice == "1") CircleSerializationDemo.Run();
                else if (choice == "2") RunPart2();
            }
        }

        private static void RunPart2()
        {
            EntityService service = new EntityService();
            Console.WriteLine("\n=== ЧАСТИНА 2: ТРИРІВНЕВА АРХІТЕКТУРА (DAL, BLL, PL) ===");
            
            Console.Write("Оберіть тип серіалізації (binary / xml / json / custom): ");
            string type = Console.ReadLine().Trim();
            service.SetSerializationType(type);

            Console.Write("Введіть назву файлу для збереження (напр., data.xml): ");
            string fileName = Console.ReadLine().Trim();

            // Створення сутностей на рівні представлення (PL)
            List<StudentPL> plStudents = new List<StudentPL>
            {
                new StudentPL { FirstName = "Олександр", LastName = "Іванов", Course = 5, StudentId = "КВ123456", ServedInArmy = true, MilitaryId = "МК111" },
                new StudentPL { FirstName = "Марія", LastName = "Коваленко", Course = 1, StudentId = "КВ987654", ServedInArmy = false },
                new StudentPL { FirstName = "Дмитро", LastName = "Максименко", Course = 5, StudentId = "КВ111111", ServedInArmy = true, MilitaryId = "МК222" },
                new StudentPL { FirstName = "Андрій", LastName = "Шевченко", Course = 3, StudentId = "КВ333333", ServedInArmy = false }
            };

            // Мапінг PL -> BLL
            List<StudentBLL> bllStudentsToSave = new List<StudentBLL>();
            foreach (var p in plStudents)
            {
                bllStudentsToSave.Add(new StudentBLL { FirstName = p.FirstName, LastName = p.LastName, Course = p.Course, StudentId = p.StudentId, ServedInArmy = p.ServedInArmy, MilitaryId = p.MilitaryId });
            }

            try
            {
                // Запис через BLL
                service.SaveStudents(fileName, bllStudentsToSave);
                Console.WriteLine($"\nДані успішно збережено у файл {fileName} (Формат: {type}).");

                // Читання через BLL
                List<StudentBLL> loadedBll = service.LoadStudents(fileName);
                
                Console.WriteLine("\n--- ЗАВДАННЯ ВАРІАНТУ 4 ---");
                Console.WriteLine("Студенти 5-го курсу, які служили в армії:");
                
                var targetStudents = service.GetFifthCourseArmyStudents(loadedBll);
                
                // Мапінг BLL -> PL для виводу
                foreach (var b in targetStudents)
                {
                    StudentPL pl = new StudentPL { FirstName = b.FirstName, LastName = b.LastName, Course = b.Course, StudentId = b.StudentId, ServedInArmy = b.ServedInArmy, MilitaryId = b.MilitaryId };
                    Console.WriteLine($"- {pl.ToString()}");
                }
                Console.WriteLine($"Загальна кількість: {targetStudents.Count}\n");
            }
            catch (StudentValidationException ex)
            {
                Console.WriteLine($"Помилка бізнес-логіки: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка: {ex.Message}");
            }
        }
    }
}