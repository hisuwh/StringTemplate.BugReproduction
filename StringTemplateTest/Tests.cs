namespace StringTemplateTest
{
    using System.IO;

    using Antlr4.StringTemplate;

    using Xunit;

    public class TestBase
    {
        protected TemplateRawGroupDirectory templateGroup;

        public TestBase()
        {
            this.templateGroup = new TemplateRawGroupDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Templates"), '#', '#');
        }
    }

    public class With_template_with_named_include : TestBase
    {
        [Fact]
        public void Should_return_rendered_model()
        {
            var template = this.templateGroup.GetInstanceOf("namedInclude");
            template.Add("model", new
            {
                Name = "test name",
                Value = "test value"
            });

            var result = template.Render();

            Assert.Equal("name: test name\nvalue: test value\n", result.Replace("\r\n", "\n"));
        }
    }

    public class With_template_with_dynamic_include : TestBase
    {
        [Fact]
        public void Should_return_rendered_model()
        {
            var template = this.templateGroup.GetInstanceOf("dynamicInclude");
            template.Add("model", new
            {
                Include = "includedTemplate",
                Name = "test name",
                Value = "test value"
            });

            var result = template.Render();

            Assert.Equal("name: test name\nvalue: test value\n", result.Replace("\r\n", "\n"));
        }

    }
}