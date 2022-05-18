using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public delegate void OurEventHandler(double input);    

    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Add(new Animal { SpecieName = "Cat", RealName = "Vasia" });
            zoo.Add(new Animal { SpecieName = "Dog", RealName = "Sharik" });
            zoo.Add(new Animal { SpecieName = "Dog", RealName = "Vasia" });
            Animal vasiaCat = zoo["Cat", "Vasia"];
            Animal animal3 = zoo[2];
            Animal fish = zoo["Fish", ""];

            System.Reflection.MemberInfo typeInfo;
            typeInfo = typeof(Zoo);
            object[] attrs = typeInfo.GetCustomAttributes(false);

            List<Animal> animals = new List<Animal>();
            animals.Add(new Animal { SpecieName = "Cat", RealName = "Vasia" });
            animals.Add(new Animal { SpecieName = "Dog", RealName = "Sharik" });
            animals.Add(new Animal { SpecieName = "Dog", RealName = "Vasia" });
            IEnumerable<Animal> dogs = animals.Where(a => a.SpecieName == "Dog").OrderBy(a => a.RealName);
            animals.Add(new Animal { SpecieName = "Dog", RealName = "" });
            Animal[] listOfDogs = dogs.ToArray();
        }            
    }

    class Animal
    {
        public string SpecieName { get; set; }
        public string RealName { get; set; }
    }

    class Zoo
    {
        private List<Animal> animals = new List<Animal>();

        public void Add(Animal a)
        {
            animals.Add(a);
        }

        public void Remove(Animal a)
        {
            animals.Remove(a);
        }

        public Animal this [int index] {
            get => animals.ElementAt(index);
            set => animals[index] = value;
        }

        public Animal this[string specie, string name] => animals.Where(a => a.SpecieName == specie && a.RealName == name).FirstOrDefault();    
    }
}
