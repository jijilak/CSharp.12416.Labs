using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classes
{
    public delegate void OurEventHandler(double input);    

    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int degreeOfParallelism = Environment.ProcessorCount;
            int[] arr = new int[512_000_000];
            Parallel.For(0, degreeOfParallelism - 1, workerId =>
            {
                var max = arr.Length * (workerId + 1) / degreeOfParallelism;
                for (int i = arr.Length * workerId / degreeOfParallelism; i < max; i++)
                {
                    arr[i] = r.Next();
                }
            });
            int[] arr2 = new int[512_000_000];
            Parallel.For(0, degreeOfParallelism - 1, workerId =>
            {
                var max = arr2.Length * (workerId + 1) / degreeOfParallelism;
                for (int i = arr2.Length * workerId / degreeOfParallelism; i < max; i++)
                {
                    arr2[i] = r.Next();
                }
            });
            int[] arr3 = new int[512_000_000];
            Parallel.For(0, degreeOfParallelism - 1, workerId =>
            {
                var max = arr3.Length * (workerId + 1) / degreeOfParallelism;
                for (int i = arr3.Length * workerId / degreeOfParallelism; i < max; i++)
                {
                    arr3[i] = r.Next();
                }
            });
            int[] arr4 = new int[512_000_000];
            Parallel.For(0, degreeOfParallelism - 1, workerId =>
            {
                var max = arr4.Length * (workerId + 1) / degreeOfParallelism;
                for (int i = arr4.Length * workerId / degreeOfParallelism; i < max; i++)
                {
                    arr4[i] = r.Next();
                }
            });
            int[] arr5 = new int[512_000_000];
            Parallel.For(0, degreeOfParallelism - 1, workerId =>
            {
                var max = arr5.Length * (workerId + 1) / degreeOfParallelism;
                for (int i = arr5.Length * workerId / degreeOfParallelism; i < max; i++)
                {
                    arr5[i] = r.Next();
                }
            });


            Console.ReadKey();
        }



        static void Zoo()
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

            //IEnumerable<Animal> dogs =
            //    from a in animals
            //    where a.SpecieName == "Dog"
            //    orderby a.RealName
            //    select a;

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

        public Animal this[string specie, string name] 
            => animals.FirstOrDefault(a => a.SpecieName == specie && a.RealName == name);    
    }
}
