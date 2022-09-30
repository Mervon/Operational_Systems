using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace Practic_1 {
    class Program {
        private class Person {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private static void Task1() {
            // Задание №1: вывести информацию в консоль о логических дисках,
            // именах, метке тома, размере и типе файловой системы
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives) {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady) {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }

                Console.WriteLine();
            }
        }

        private static void Task2() {
            // Задание №2: работа с файлами ( класс File, FileInfo, FileStream и другие)
            Console.WriteLine("Введите имя файла:");
            var path = Console.ReadLine() + ".txt";
            Console.WriteLine("Введите строку для записи в файл:");
            var text = Console.ReadLine();
            using var fstream = new FileStream(path, FileMode.OpenOrCreate);
            // преобразуем строку в байты
            var array = System.Text.Encoding.Default.GetBytes(text);
            // запись массива байтов в файл
            fstream.Write(array, 0, array.Length);
            Console.WriteLine("Текст записан в файл");
            // читаем с файла
            fstream.Seek(0, SeekOrigin.Begin);
            var output = new byte[fstream.Length];
            fstream.Read(output, 0, output.Length);
            var textFromFile = Encoding.Default.GetString(output);
            Console.WriteLine($"Текст из файла: {textFromFile}");
            fstream.Close();
            Console.WriteLine("Удалять файл? 0 = ДА");
            while (Console.ReadLine() != "0") {
                Console.WriteLine("Удалять файл? 0 = ДА");
            } 
            File.Delete(path);
        }

        private static void Task3() {
            // Задание №3: работа с форматом JSON
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };

            var filename = "user.json";

            using (var fs = new FileStream(filename, FileMode.OpenOrCreate)) {
                var tom = new Person() { Name = "Tom", Age = 35 };
                JsonSerializer.Serialize<Person>(fs, tom, options);
                Console.WriteLine("Data has been saved to file");
            }

            using (var fs = new FileStream(filename, FileMode.OpenOrCreate)) {
                fs.Seek(0, SeekOrigin.Begin);
                var output = new byte[fs.Length];
                fs.Read(output, 0, output.Length);
                var textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
            Console.WriteLine("Удалять файл? 0 = ДА");
            while (Console.ReadLine() != "0") {
                Console.WriteLine("Удалять файл? 0 = ДА");
            }
            File.Delete(filename);
        }

        private static void Task4() {
            // Задание №4: работа с форматом XML
            var xdoc = new XDocument(new XElement("phones",
                new XElement("phone",
                    new XAttribute("name", "iPhone 6"),
                    new XElement("company", "Apple"),
                    new XElement("price", "40000")),
                new XElement("phone",
                    new XAttribute("name", "Samsung Galaxy S5"),
                    new XElement("company", "Samsung"),
                    new XElement("price", "33000"))));
            xdoc.Save("phones.xml");

            using (var fs = new FileStream("phones.xml", FileMode.OpenOrCreate)) {
                fs.Seek(0, SeekOrigin.Begin);
                var output = new byte[fs.Length];
                fs.Read(output, 0, output.Length);
                var textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine($"Текст из файла до редактирования:\n{textFromFile}");
            }

            { // Редактирование xml
                var xDoc = new XmlDocument();
                xDoc.Load("phones.xml");
                var xRoot = xDoc.DocumentElement;
                var userElem = xDoc.CreateElement("phone");
                var nameAttr = xDoc.CreateAttribute("name");
                var companyElem = xDoc.CreateElement("company");
                var priceElem = xDoc.CreateElement("price");
                var nameText = xDoc.CreateTextNode("");
                var companyText = xDoc.CreateTextNode("");
                var priceText = xDoc.CreateTextNode("");
                for (var i = 0; i < 3; ++i) {
                    switch (i) {
                        case 0:
                            Console.WriteLine("Введите название смартфона");
                            nameText = xDoc.CreateTextNode(Console.ReadLine());
                            break;
                        case 1:
                            Console.WriteLine("Введите название компании");
                            companyText = xDoc.CreateTextNode(Console.ReadLine());
                            break;
                        case 2:
                            Console.WriteLine("Введите цену");
                            priceText = xDoc.CreateTextNode(Console.ReadLine());
                            break;
                        default:
                            break;
                    }
                }

                nameAttr.AppendChild(nameText);
                companyElem.AppendChild(companyText);
                priceElem.AppendChild(priceText);
                userElem.Attributes.Append(nameAttr);
                userElem.AppendChild(companyElem);
                userElem.AppendChild(priceElem);
                xRoot.AppendChild(userElem);
                xDoc.Save("phones.xml");
            }

            using (var fs = new FileStream("phones.xml", FileMode.OpenOrCreate)) {
                fs.Seek(0, SeekOrigin.Begin);
                var output = new byte[fs.Length];
                fs.Read(output, 0, output.Length);
                var textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine($"Текст из файла после редактирования:\n{textFromFile}");
            }
            
            Console.WriteLine("Удалять файл? 0 = ДА");
            while (Console.ReadLine() != "0") {
                Console.WriteLine("Удалять файл? 0 = ДА");
            }
            File.Delete("phones.xml");
        }

        private static void Task5() {
            // Задание №5: создание zip архива, добавление туда файла, определение размера архива
            var sourceFolder = @"C:\GitRepos\Практика_1\Практика_1\";
            const string zipFile = "TestToZip.zip";
            const string targetFolder = "TestToZipUnpacked";
            Console.WriteLine("Введите название файла для архивации");

            var filename = Console.ReadLine();
            const string tail = ".txt";
            var fileInf_2 = new FileInfo(filename + tail);
            while (!(fileInf_2.Exists)) {
                Console.WriteLine("Введите корректное название файла");
                filename = Console.ReadLine();
                fileInf_2 = new FileInfo(filename + tail);
            }

            Directory.CreateDirectory("tmp");
            File.Move(filename + tail, "tmp\\" + filename + tail);
            ZipFile.CreateFromDirectory("tmp", zipFile);
            Console.WriteLine($"Файл {filename + tail} архивирована в файл {zipFile}");
            ZipFile.ExtractToDirectory(zipFile, targetFolder, true);
            Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
            File.Move("tmp\\" + filename + tail, filename + tail);
            Directory.Delete("tmp", true);
            fileInf_2 = new FileInfo(zipFile);
            if (!fileInf_2.Exists) return;
            Console.WriteLine("Имя заархивированного файла: {0}", fileInf_2.Name);
            Console.WriteLine("Время создания: {0}", fileInf_2.CreationTime);
            Console.WriteLine("Размер: {0}", fileInf_2.Length);
            Console.WriteLine("Удалять файл? 0 = ДА");
            while (Console.ReadLine() != "0") {
                Console.WriteLine("Удалять файл? 0 = ДА");
            }
            File.Delete(zipFile);
            fileInf_2 = new FileInfo(targetFolder + "\\" + filename + tail);
            if (!fileInf_2.Exists) return;
            Console.WriteLine("Имя файла: {0}", fileInf_2.Name);
            Console.WriteLine("Время создания: {0}", fileInf_2.CreationTime);
            Console.WriteLine("Размер: {0}", fileInf_2.Length);
        }

        static void Main(string[] args) {
            Console.WriteLine("Задание номер 1:\n");
            Task1();
            Console.WriteLine("Задание номер 2:\n");
            Task2();
            Console.WriteLine("Задание номер 3:\n");
            Task3();
            Console.WriteLine("Задание номер 4:\n");
            Task4();
            Console.WriteLine("Задание номер 5:\n");
            Task5();
        }
    }
}