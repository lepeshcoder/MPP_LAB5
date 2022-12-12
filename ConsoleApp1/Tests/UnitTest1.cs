using ConsoleApp1;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {

        [Fact]
        public void InvalidPropertyNameEx()
        {
            User user = new(1, 19, "ARTEM", "ZHURAUSKI");
            StringFormatter formatter = new();
            Assert.Throws<InvalidPropertyNameException>(() => formatter.Format("id = {id1}", user));
            Assert.Throws<InvalidPropertyNameException>(() => formatter.Format("name = {kame}", user));
            Assert.Throws<InvalidPropertyNameException>(() => formatter.Format("surname = {surmame}", user));
            Assert.Throws<InvalidPropertyNameException>(() => formatter.Format("age = {2age}", user));
        }


        [Fact]
        public void InvalidTemplateEx()
        {
            User user = new(1, 19, "ARTEM", "ZHURAUSKI");
            StringFormatter formatter = new();
            Assert.Throws<InvalidTemplateException>(() => formatter.Format("}{", user));
            Assert.Throws<InvalidTemplateException>(() => formatter.Format("{{}", user));
            Assert.Throws<InvalidTemplateException>(() => formatter.Format("{}}", user));
            Assert.Throws<InvalidTemplateException>(() => formatter.Format("{", user));
        }



        [Fact]
        public void NormalTests()
        {
            User user = new(1, 19, "ARTEM", "ZHURAUSKI");
            StringFormatter formatter = new();
            Assert.Equal("id = 1, name = ARTEM, surname = ZHURAUSKI, age = 19",
                         formatter.Format("id = {Id}, name = {Name}, surname = {Surname}, age = {Age}", user));
            Assert.Equal("{id} = 1",
                         formatter.Format("{{id}} = {id}", user));
        }
    }
}