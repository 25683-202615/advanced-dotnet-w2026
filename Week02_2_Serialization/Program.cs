using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Week02_2_Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonSerialization();
            XmlSerialization();
        }

        static void JsonSerialization()
        {
            // declare and initialize our json serializer
            // supply type of person to indicate what type of object
            // is to be serialized
            var serializer = new JsonSerializer();

            var stringWriter = new StringWriter();

            var person = new Person
            {
                Name = "Jane Doe",
                DateOfBirth = DateTimeOffset.Now,
                Id = Guid.NewGuid()
            };

            serializer.Serialize(stringWriter, person);

            var serializedContent = Encoding.UTF8.GetBytes(stringWriter.ToString());

            Console.WriteLine("write our serialized content to a file called 'output.json'");

            // write our serialized content to a file called 'output.json'
            File.WriteAllBytes("output.json", serializedContent);

            Console.WriteLine("read our serialized content from a file called 'output.json'");
            var bytes = File.ReadAllBytes("output.json");

            var jsonReader = new JsonTextReader(new StringReader(Encoding.UTF8.GetString(bytes)));

            var deserializedPerson = (Person)serializer.Deserialize(jsonReader, typeof(Person));

            Console.WriteLine(deserializedPerson.Name);

            Console.ReadKey();
        }

        static void XmlSerialization()
        {
            // declare and initialize our xml serializer
            // supply type of person to indicate what type of object
            // is to be serialized
            var serializer = new XmlSerializer(typeof(Person));

            var memoryStream = new MemoryStream();

            var person = new Person
            {
                Name = "John Doe",
                DateOfBirth = DateTimeOffset.Now,
                Id = Guid.NewGuid()
            };

            serializer.Serialize(memoryStream, person);

            var serializedContent = memoryStream.ToArray();

            Console.WriteLine("write our serialized content to a file called 'output.xml'");

            // write our serialized content to a file called 'output.xml'
            File.WriteAllBytes("output.xml", serializedContent);

            Console.WriteLine("read our serialized content from a file called 'output.xml'");
            var bytes = File.ReadAllBytes("output.xml");

            var deserializedPerson = (Person)serializer.Deserialize(new MemoryStream(bytes));

            Console.WriteLine(deserializedPerson.Name);

            Console.ReadKey();
        }
    }
}
