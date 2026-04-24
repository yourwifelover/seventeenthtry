using thirteenth;
//First Task
Predator wolf = new Predator();
wolf.Hunt();
wolf.Running();
wolf.Sleep();
//Second Task
//Media something
Console.WriteLine("==================");
List<LearningMaterial> materials = new List<LearningMaterial>();


Article article1 = new Article(10,"The Art of Hunting", "Survive");
Article article2 = new Article(15,"The Science of Predation", "World of science");

VideoLesson video1 = new VideoLesson(20,"Hunting Techniques", "basic");
VideoLesson video2 = new VideoLesson(25,"Predator Behavior", "advance");


materials.Add(article1);
materials.Add(article2);

materials.Add(video1);
materials.Add(video2);

int lengthSum = 0;

int boringmaterials = 0;

foreach (var material in materials)
{
    material.GetInfo();

    if (material.IsLong())
    {
        boringmaterials++;
        Console.WriteLine("Too much time");
    }
    else
        Console.WriteLine("Not too much time");

    material.GetHash();        

    lengthSum += material.GetLength;

    Console.WriteLine("__  __  __  __  __  __  __  __  __  __");
}

Console.WriteLine("Total length " + lengthSum);
Console.WriteLine("Boring materials: " + boringmaterials);
LearningMaterial.CountMaterials();