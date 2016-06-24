using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Presentation.Services;


namespace TestVk
{
    [TestFixture]
    public class TestVk
    {
        [Test]

      public  void TestParce()
        {
            var s = File.ReadAllText("t.txt");
            IParserPage parser = new ParserPage();
            var expectedResult = "https://vk.com/captcha.php?sid=827206270958&dif=1";
            var result = parser.ParseImg(s);

            Assert.That(expectedResult,Is.EqualTo(result));
        }
        [Test]
        public void TestSuccessAuth()
        {
            var vk = new Vk();

            var result = vk.Auth("79859161363", "3610Lornez");

            Assert.That(result,Is.EqualTo(new KeyValuePair<int,string>(0,"Успешно авторизовались")));
        }

        [Test]
        public void TestFailtAuth()
        {
            var vk = new Vk();
            
            var result =  vk.Auth("incorrect","incorrect");

            Assert.That(result, Is.False);


        }
    }
}
