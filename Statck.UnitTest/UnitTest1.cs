using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statck.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testString = "a bc 测 试";
            var test = testString.Reverse();

            var count = testString.IncludeCount('c');
            count = testString.IncludeCount("c");

            var t1 = testString.SwapChar(1, 2);
            var t2 = testString.RemoveAllWhiteSpaces();
            test = "<a>这是一个html字符串</a>";
            var t3 = test.RemoveHtml();
            var t4 = test.HtmlSubString(1, "</a>");
            var t5 = test.ToBinEncodedString();
            test = "6812548796312145";
            var t6 = test.ReplaceWithSpecialChar(0,0);
            t6 = test.ReplaceWithSpecialChar(9, 8);
            t6 = test.ReplaceWithSpecialChar(-1, -2);
            t6 = test.ReplaceWithSpecialChar(10, 1);
            t6 = test.ReplaceWithSpecialChar(0, 12);
            t6 = test.ReplaceWithSpecialChar(12, 0);



            Assert.IsTrue(count == 1);
        }
    }
}
