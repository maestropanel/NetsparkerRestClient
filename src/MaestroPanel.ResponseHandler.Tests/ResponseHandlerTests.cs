using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient.Response;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace MaestroPanel.ResponseHandler.Tests
{
    [TestClass]
    public class ResponseHandlerTests
    {
        Foo f;

        [TestInitialize]
        public void Setup()
        {
            f = new Foo
            {
                Id = 1,
                Name = "bar"
            };
        }

        [TestMethod]
        public void response_handler_byte_array_response_handler_test_should_be_buffer_length_223()
        {
            IResponseHandler handler = new ByteArrayResponseHandler();

            BinaryFormatter formatter = new BinaryFormatter();

            var memoryStream = new MemoryStream();

            formatter.Serialize(memoryStream, f);

            var data = handler.Handle(memoryStream);

            var binaryData = (byte[])data.Data;

            Assert.AreEqual(memoryStream.Length, binaryData.Length);
        }

        [TestMethod]
        public void response_handler_xml_response_handler_test()
        {
            IResponseHandler handler = new XmlResponseHandler();

            XmlSerializer serializer = new XmlSerializer(typeof(Foo));

            var memoryStream = new MemoryStream();

            serializer.Serialize(memoryStream, f);

            var data = handler.Handle<Foo>(memoryStream);

            Assert.AreEqual(f.Id, data.Data.Id);
        }
    }

    [Serializable]
    public class Foo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
