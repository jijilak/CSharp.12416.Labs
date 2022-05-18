using System;
using System.Collections;
using System.Collections.Generic;
using Classes;

namespace Tigers
{
    
    interface IMoved
    {
        void Move();
    }

    class Program
    {
        static void Main(string[] args)
        {
            //UseDelegate.InvokeDelegate();
            List<Mammal> zoo = new List<Mammal>
            {
                new Tiger("Vasily"),
                new Lion("Leo"),
                new Feline(Color.Dotted, "Jagg")
            };
            foreach (Feline feline in zoo)
            {
                Tiger t = feline as Tiger;
                Console.WriteLine(t?.Name.ToString() ?? "Это - не тигр");
            }
        }
    }

    public enum Color
    {
        Striped,
        Dotted,
        OneColor
    }

    public abstract class Mammal: IMoved
    {
        public readonly int legs = 4;
        private string name;

        public string Name
        {
            get => name + " " + this.GetType(); 
            set => name = value + " The Animal"; 
        }

        protected Mammal() { }
        protected Mammal(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return this.GetType() + " " + name;
        }

        public void Move()
        {
            Console.WriteLine("I am moving...");
        }
    }

    public class Feline : Mammal
    {
        public readonly Color color;

        public Feline() : this(Color.OneColor, string.Empty)  { }
        public Feline(Color color, string name) : base(name)
        {
            this.color = color;
        }
    }

    public class Tiger : Feline
    {
        public int orderNumber;
        static private int nextNumber;

        static Tiger()
        {
            nextNumber = 1;
        }

        public Tiger(string name): base(Color.Striped, name)
        {
            orderNumber = nextNumber++;
        }

        public override string ToString()
        {
            return this.GetType().ToString() + orderNumber + " " + Name;
        }

        
    }

    public class Lion : Feline
    {
        public Lion(string name) : base(Color.OneColor, name) { }
    }    
}
