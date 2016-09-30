namespace Twitter.Tests
{
    using NUnit.Framework;
    using Services.Infrastructure;

    [TestFixture]
    public class TagsReplaceServiceTests
    {
        [Test]
        public void TagsServiceReplaces_Tags_Correctly()
        {
            var tagReplaceService = new TagsReplaceService();
            string text = "#Tag should become link";
            string expected = "<a href=\"/Tag/Index/%23Tag> </a> should become link";

            var result = tagReplaceService.ReplaceHashtagsWithLinks(text);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void TagsServiceReplaces_Tags_Correctly_MultipleTags()
        {
            var tagReplaceService = new TagsReplaceService();
            string text = "#Tag should #become #link";
            string expected = "<a href=\"/Tag/Index/%23Tag> </a> should <a href=\"/Tag/Index/%23become> </a> <a href=\"/Tag/Index/%23link> </a>";

            var result = tagReplaceService.ReplaceHashtagsWithLinks(text);

            Assert.AreEqual(result, expected);
        }
    }
}
