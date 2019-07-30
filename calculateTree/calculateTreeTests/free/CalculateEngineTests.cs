using Microsoft.VisualStudio.TestTools.UnitTesting;
using calculateTree.free;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.Tests
{
    [TestClass()]
    public class CalculateEngineTests
    {


        [TestMethod()]
        public void ClearTest()
        {
            CalculateEngine engine = new CalculateEngine();
            engine.Clear();
        }

        [TestMethod()]
        public void SetVariblesTest()
        {
            CalculateEngine engine = new CalculateEngine();
            engine.Parse("a+b=5");
            engine.Parse("b=4");
            var val= engine.GetVarileValue("a");
            Assert.AreEqual(val,1);
        }


        [TestMethod()]
        public void SetVariblesTest2()
        {
            CalculateEngine engine = new CalculateEngine();
            engine.Parse("a+b=5");
            engine.SetContantVarible("b", 4);
            var val = engine.GetVarileValue("a");
            Assert.AreEqual(val, 1);
        }

        [TestMethod()]
        public void SetVariblesTest3()
        {
            int val = 0;
            Func<dynamic> getVal = () => { return val++; };
            CalculateEngine engine = new CalculateEngine();
            engine.Parse("a+b=5");
            engine.SetVaribles("a", getVal);
            var res = engine.GetVarileValue("b");
            Assert.AreNotEqual(res, 5);
            res = engine.GetVarileValue("b");
            Assert.AreNotEqual(res, 4);
        }

        [TestMethod()]
        public void SetContantVaribleTest()
        {
            
        }

        [TestMethod()]
        public void GetVaribleDescriptionTest()
        {
        }

        [TestMethod()]
        public void GetVarileValueTest()
        {
        }

        [TestMethod()]
        public void ParseTest()
        {
        }
    }
}