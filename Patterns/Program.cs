using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartStudent smartStudent = new SmartStudent(); //экземпляр умного студента
            MiddleStudent middleStudent = new MiddleStudent(); //экземпляр среднего студента

            smartStudent.Successor = middleStudent; //если умный студент не сможет решить задачу, то передаст ее среднему
            middleStudent.Successor = smartStudent; //если средний студент не сможет решить задачу, то передаст ее умному

            smartStudent.HandleRequest(1); // умный студент решает задачу
            middleStudent.HandleRequest(2); // средний студент решает задачу

            middleStudent.HandleRequest(1); // средний студент не может решить задачу, поэтому ее решает умный студент
            smartStudent.HandleRequest(2); // умный студент не может решить задачу, поэтому ее решает средний студент

            Console.WriteLine("Нажмите Enter для продолжения");
            Console.ReadLine(); // ожидание нажатия Enter
            Console.Clear(); // очистка консоли
            EmployeeWithWork employeeWithWork = new EmployeeWithWork(); // экземпляр занятого работника
            employeeWithWork.DoWork(); // работник который занят попросит свободного работника выполнить работу
            Console.ReadLine(); // ожидание нажатия Enter
        }
    }


    // -----------------------Цепочка отвественности-----------------------
    abstract class Student
    {
        public Student Successor { get; set; } // студент который решит задачу, если у текущего не получится
        public abstract void HandleRequest(int condition); // метод решения задачи куда мы передаем число, которое определяет, может ли студент решить задачу
    }

    class SmartStudent : Student // умный студент
    {
        public override void HandleRequest(int condition)
        {
            if (condition == 1)
            {
                Console.WriteLine("Ответом будет x^2+2x-4!"); // если знает ответ
            }
            else if (Successor != null) // если не знает ответ, но есть студент, который может решить, то перекидывает задачу ему
            {
                Successor.HandleRequest(condition);
            }
        }
    }

    class MiddleStudent : Student // средний студент
    {
        public override void HandleRequest(int condition)
        {
            if (condition == 2) // если знает ответ
            {
                Console.WriteLine("Ответом будет 4!");
            }
            else if (Successor != null) // если не знает ответ, но есть студент, который может решить, то перекидывает задачу ему
            {
                Successor.HandleRequest(condition);
            }
        }
    }

    // -----------------------Делегирование-----------------------
    class FreeEmployee // Свободный работник
    {
        public void DoWork() // сделать работу
        {
            Console.WriteLine("Работа была сделана!");
        }
    }
    class EmployeeWithWork // Работник, которому дали работу, но он занят
    {
        private readonly FreeEmployee freeEmployee; // экземпляр свободного работника
        public EmployeeWithWork()
        {
            freeEmployee = new FreeEmployee(); // инициализация экземпляра свободного работника
        }
        public void DoWork()
        {
            freeEmployee.DoWork(); // свободный работник выполняет работу, которую дали работнику, который занят
        }
    }
}
