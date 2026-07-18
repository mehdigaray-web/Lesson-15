string textpath = "students.txt";
string binarypath = "students.bin";

List<Student> students = new List<Student>
{
    new Student(1,  "Tural",   "Mammadov",  19, 88.5),
    new Student(2,  "Aysel",   "Hasanova",  20, 95.0),
    new Student(3,  "Rauf",    "Guliyev",   21, 72.3),
    new Student(4,  "Nigar",   "Aliyeva",   18, 97.1),
    new Student(5,  "Kamran",  "Huseynov",  22, 91.4),
    new Student(6,  "Leyla",   "Jafarova",  19, 60.8),
    new Student(7,  "Murad",   "Rzayev",    20, 83.2),
    new Student(8,  "Sevinj",  "Quliyeva",  21, 78.9),
    new Student(9,  "Elvin",   "Ismayilov", 18, 90.0),
    new Student(10, "Gunay",   "Babayeva",  22, 65.5)
};


while (true)
{
    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine("     STUDENT DATABASE         ");
    Console.WriteLine("==============================");
    Console.WriteLine("1. Add student");
    Console.WriteLine("2. Show all students");
    Console.WriteLine("3. Save to text file");
    Console.WriteLine("4. Save to binary file");
    Console.WriteLine("5. Load from text file");
    Console.WriteLine("6. Load from binary file");
    Console.WriteLine("0. Exit");
    Console.WriteLine("------------------------------");
    Console.Write("Choose: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        try
        {
            Console.Write("First name: ");
            string firstname = Console.ReadLine();

            Console.Write("Last name: ");
            string lastname = Console.ReadLine();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Average mark: ");
            double mark = double.Parse(Console.ReadLine());

            int newid = students.Count > 0 ? students[students.Count - 1].Id + 1 : 1;
            students.Add(new Student(newid, firstname, lastname, age, mark));
            Console.WriteLine("Student added.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding student: {ex.Message}");
        }
    }
    else if (choice == "2")
    {
        Console.WriteLine("--- All students ---");
        if (students.Count == 0) Console.WriteLine("No students.");
        else foreach (Student s in students) Console.WriteLine(s);
    }
    else if (choice == "3")
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(textpath))
            {
                foreach (Student s in students)
                {
                    writer.WriteLine(s.Id);
                    writer.WriteLine(s.FirstName);
                    writer.WriteLine(s.LastName);
                    writer.WriteLine(s.Age);
                    writer.WriteLine(s.AverageMark);
                }
            }
            Console.WriteLine($"Saved to text file: {textpath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to text: {ex.Message}");
        }
    }
    else if (choice == "4")
    {
        try
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(binarypath, FileMode.Create)))
            {
                writer.Write(students.Count);
                foreach (Student s in students)
                {
                    writer.Write(s.Id);
                    writer.Write(s.FirstName);
                    writer.Write(s.LastName);
                    writer.Write(s.Age);
                    writer.Write(s.AverageMark);
                }
            }
            Console.WriteLine($"Saved to binary file: {binarypath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to binary: {ex.Message}");
        }
    }
    else if (choice == "5")
    {
        try
        {
            if (!File.Exists(textpath)) Console.WriteLine("File not found.");
            else
            {
                students = new List<Student>();
                using (StreamReader reader = new StreamReader(textpath))
                {
                    while (!reader.EndOfStream)
                    {
                        int id = int.Parse(reader.ReadLine());
                        string firstname = reader.ReadLine();
                        string lastname = reader.ReadLine();
                        int age = int.Parse(reader.ReadLine());
                        double mark = double.Parse(reader.ReadLine());
                        students.Add(new Student(id, firstname, lastname, age, mark));
                    }
                }
                Console.WriteLine("Loaded from text file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from text: {ex.Message}");
        }
    }
    else if (choice == "6")
    {
        try
        {
            if (!File.Exists(binarypath)) Console.WriteLine("File not found.");
            else
            {
                students = new List<Student>();
                using (BinaryReader reader = new BinaryReader(File.Open(binarypath, FileMode.Open)))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        int id = reader.ReadInt32();
                        string firstname = reader.ReadString();
                        string lastname = reader.ReadString();
                        int age = reader.ReadInt32();
                        double mark = reader.ReadDouble();
                        students.Add(new Student(id, firstname, lastname, age, mark));
                    }
                }
                Console.WriteLine("Loaded from binary file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from binary: {ex.Message}");
        }
    }
    else if (choice == "0") break;
}

class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public double AverageMark { get; set; }

    public Student(int id, string firstname, string lastname, int age, double averagemark)
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
        Age = age;
        AverageMark = averagemark;
    }

    public override string ToString()
    {
        return $"[{Id}] {FirstName} {LastName} | Age: {Age} | Mark: {AverageMark}";
    }
}
