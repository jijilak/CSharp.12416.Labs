using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students
{
    class Program
    {
        static void Main(string[] args)
        {
            Student ivanov = new Student { Name = "Petr Ivanov" };
            Student petrov = new Student { Name = "Ivan Petrov" };
            List<Student> studentsList = new List<Student> { ivanov, petrov };

            //Act
            studentsList.Sort();

            //Assert
            ;
        }
    }

    public class Student: IEquatable<Student>, IComparable<Student>
    {
        private static int nextId = 1;

        private readonly int id = nextId++;

        public int Id
        {
            get { return id; }
        } 
        public string Name { get; set; }
        public Dictionary<string, int> Marks { get; private set; } = new Dictionary<string, int>();

        public void AddMark(string discipline, int mark)
        {
            if (Marks.ContainsKey(discipline)) Marks[discipline] = mark;
            else Marks.Add(discipline, mark);
        }

        public bool Equals(Student other)
        {
            if (other == null)
                return false;
            return Id == other.Id;
        }

        public static bool operator ==(Student student1, Student student2)
        {
            return student1?.Equals(student2) ?? false;
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return !(student1 == student2);
        }

        public int CompareTo(Student other)
        {
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}
