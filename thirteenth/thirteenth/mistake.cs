using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thirteenth
{
    class Predator
    {
        Claw longteeth = new Claw();
        Fang sharpfang = new Fang();
        public void Hunt()
        {
            longteeth.Scratch();
            sharpfang.Bite();
            sharpfang.Growl();
            Console.WriteLine("The predator is hunting.");
        }
        public void Running()
        {
            Console.WriteLine("I`m running");
        }
        public void Sleep()
        {
            Console.WriteLine("The predator is sleeping.");
        }
    }
    class Claw
    {
        public void Scratch()
        {
            Console.WriteLine("The claw is scratching.");
        }       
    }
    class Fang
    {
        public void Bite()
        {
            Console.WriteLine("The fang is biting.");
        }
        public void Growl()
        {
            Console.WriteLine("Raaaar! Grrrr!");
        }
    }

    //______________________________________________________________//
    //______________________________________________________________//

    //Media something
    class LearningMaterial
    {
        private readonly int Hash;

        private string Name;
        private int Length;

        public static int count=0;

        public static int totalwasteoftime = 0; 
        
        public virtual void GetInfo()
        {
            Console.WriteLine($"This is a learning material {Name}, {Length}");
        }

        public bool IsLong()
        {
            if (Length > 5)
            {                
                return true;
            }
            else
            {
                Console.WriteLine("This learning material is short.");
                return false;
            }
        }

        public int GetLength
        {
            get { return Length; }
        }

        public static void CountMaterials()
        {
            Console.WriteLine($"Total learning materials: {count}");
        }

        public void GetHash()
        {
            Console.WriteLine(Hash); ;
        }

        public LearningMaterial(string name, int length)
        {
           Length = length;
           Name = name;
           Hash = Name.GetHashCode();
            count++;
        }
    }

    class Article : LearningMaterial 
    {

        private protected string Topic;

        public override void GetInfo()
        {
            base.GetInfo();
            Console.WriteLine(Topic);
        }
        public Article(int length, string topic, string name) : base(name, length)
        {
            
            Topic = topic;            
        }
    }


    class VideoLesson : LearningMaterial
    {
        public string Level { get; set; }

        public VideoLesson(int length, string name, string level): base(name, length)
        {
            
            this.Level = level;
            
        }
        public override void GetInfo()
        {
            base.GetInfo();
            Console.WriteLine(Level);
        }
    }
}
