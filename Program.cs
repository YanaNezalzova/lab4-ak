namespace lab_4
{
    internal class Program
    {
        public static void DirSize(DirectoryInfo directory, ref long size, string choise)
        {
            FileInfo[] files;
            if (choise == "1") files = directory.GetFiles();
            else files = directory.GetFiles("*.docx");
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            DirectoryInfo[] dirs = directory.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                DirSize(dir, ref size, choise);
            }
        }

        static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Console.WriteLine("Введіть шлях до каталогів: ");
            string ? tmp = Console.ReadLine();
            while (tmp == null)
            {
                Console.WriteLine("Некоректні дані, спробуйте ще: ");
                tmp = Console.ReadLine();
            }

            string[] dirNames = tmp.Split(',');

            if (dirNames[0] == "help")
            {
                Console.WriteLine("\nПідказка\nДовідка по використанню програми:\n" +
                    "Скрипт підраховує обсяг підкаталогів у вказаному каталозі.\n" +
                    "Передавайте довільну кількість параметрів, вказуючи шлях до них.\n" +
                    "Приклад: ");
                Console.Write(@"C:\Users\Admin\Лабораторні\");
                return 0;
            }
            else
            {
                Console.WriteLine("Ви бажаєте підрахувати обсяг усіх файлів(1) або тільки розширення .docx(2): ");
                tmp = Console.ReadLine();
                while (!(tmp == "1" || tmp == "2"))
                {
                    Console.WriteLine("Некоректні дані, спробуйте ще: ");
                    tmp = Console.ReadLine();
                }

                long size = 0;
                foreach (string dirName in dirNames)
                {
                    DirectoryInfo directory = new DirectoryInfo(dirName!);

                    if (directory.Exists)
                    {
                        DirSize(directory, ref size, tmp);
                        Console.WriteLine($"Розмір папки {dirName} - {size}");
                    }
                    else
                    {
                        Console.WriteLine("Каталог не знайдено");
                        return -1;
                    }
                }
            }

            return 0;
        }
    }
}
