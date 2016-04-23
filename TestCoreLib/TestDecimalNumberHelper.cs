using hkhCoreLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCoreLib
{
    [TestClass]
    public class TestDecimalNumberHelper
    {
        [TestMethod]
        public void T0101_TryParseDouble_Null_Fails()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse(null, out result);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void T0102_TryParseDouble_Empty_Fails()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse(string.Empty, out result);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void T0103_TryParseDouble_WhitespaceOnly_Fails()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("\t\r\n ", out result);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void T0104_TryParseDouble_IntegerNumber_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("-1234567890", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(-1234567890.0d, result);
        }

        [TestMethod]
        public void T0105_TryParseDouble_DecimalPoint_NoThousand_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("1234567890.123", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(1234567890.123d, result);
        }

        [TestMethod]
        public void T0106_TryParseDouble_DecimalComma_NoThousand_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("-1234567890,123", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(-1234567890.123d, result);
        }

        [TestMethod]
        public void T0107_TryParseDouble_DecimalPoint_ThousandSpace_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("-1 234 567 890.123", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(-1234567890.123d, result);
        }

        [TestMethod]
        public void T0108_TryParseDouble_DecimalPoint_ThousandComma_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("-1,234,567,890.123", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(-1234567890.123d, result);
        }

        [TestMethod]
        public void T0109_TryParseDouble_DecimalComma_ThousandSpace_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("1 234 567 890,123", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(1234567890.123d, result);
        }

        [TestMethod]
        public void T0110_TryParseDouble_DecimalComma_ThousandPoint_Succeed()
        {
            double result;
            bool success = DecimalNumberHelper.TryParse("1.234.567.890,123", out result);
            Assert.IsTrue(success);
            Assert.AreEqual(1234567890.123d, result);
        }
    }
}
