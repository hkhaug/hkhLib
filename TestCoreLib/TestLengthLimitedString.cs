using hkhCoreLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCoreLib
{
    [TestClass]
    public class TestLengthLimitedString
    {
        #region Constructors

        [TestMethod]
        public void T0101_Construct_NoParameter_ReturnsObjectWithNullValue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.IsNotNull(lls);
            Assert.IsNull(lls.Value);
        }

        [TestMethod]
        public void T0102_Construct_Null_ReturnsObjectWithNullValue()
        {
            string s = null;
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
            Assert.IsNotNull(lls);
            Assert.IsNull(lls.Value);
        }

        [TestMethod]
        public void T0103_Construct_Empty_ReturnsObjectWithEmptyValue()
        {
            string s = string.Empty;
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
            Assert.IsNotNull(lls);
            Assert.AreEqual(string.Empty, lls.Value);
        }

        [TestMethod]
        public void T0104_Construct_AcceptableLengthValue_ReturnsObjectWithPassedValue()
        {
            string s = new string('X', LengthLimitedStringForTest.MaximumLength);
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
            Assert.IsNotNull(lls);
            Assert.AreEqual(s, lls.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void T0105_Construct_TooLong_ThrowsException()
        {
            string s = new string('X', LengthLimitedStringForTest.MaximumLength + 1);
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
        }

        [TestMethod]
        public void T0106_Construct_Copy_Identical()
        {
            string s = "Some arbitrary value (not too long, though)";
            LengthLimitedStringForTest original = new LengthLimitedStringForTest(s);
            LengthLimitedStringForTest copy = new LengthLimitedStringForTest(original);
            Assert.AreEqual(s, copy.Value);
        }

        #endregion Constructors

        #region Properties

        [TestMethod]
        public void T0107_MaxLength_CorrectSet()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.AreEqual(LengthLimitedStringForTest.MaximumLength, lls.MaxLength);
        }

        [TestMethod]
        public void T0108_Value_CorrectSet()
        {
            string s = "Some arbitrary value (not too long, though)";
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
            Assert.AreEqual(s, lls.Value);
        }

        #endregion Properties

        #region Methods

        [TestMethod]
        public void T0109_IsNullOrEmpty_Null_ReturnsTrue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.IsTrue(lls.IsNullOrEmpty());
        }

        [TestMethod]
        public void T0110_IsNullOrEmpty_Empty_ReturnsTrue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(string.Empty);
            Assert.IsTrue(lls.IsNullOrEmpty());
        }

        [TestMethod]
        public void T0111_IsNullOrEmpty_WhitespaceOnly_ReturnsFalse()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("\t\r\n ");
            Assert.IsFalse(lls.IsNullOrEmpty());
        }

        [TestMethod]
        public void T0112_IsNullOrEmpty_SomeValue_ReturnsFalse()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("Bla bla bla");
            Assert.IsFalse(lls.IsNullOrEmpty());
        }

        [TestMethod]
        public void T0113_IsNullOrWhiteSpace_Null_ReturnsTrue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.IsTrue(lls.IsNullOrWhiteSpace());
        }

        [TestMethod]
        public void T0114_IsNullOrWhiteSpace_Empty_ReturnsTrue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(string.Empty);
            Assert.IsTrue(lls.IsNullOrWhiteSpace());
        }

        [TestMethod]
        public void T0115_IsNullOrWhiteSpace_WhitespaceOnly_ReturnsTrue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("\t\r\n ");
            Assert.IsTrue(lls.IsNullOrWhiteSpace());
        }

        [TestMethod]
        public void T0116_IsNullOrWhiteSpace_SomeValue_ReturnsFalse()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("Bla bla bla");
            Assert.IsFalse(lls.IsNullOrWhiteSpace());
        }

        #endregion Methods

        #region Bread and butter

        [TestMethod]
        public void T0117_ToString_Null_ReturnsEmptyString()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.AreEqual(string.Empty, lls.ToString());
        }

        [TestMethod]
        public void T0118_ToString_Empty_ReturnsEmptyString()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(string.Empty);
            Assert.AreEqual(string.Empty, lls.ToString());
        }

        [TestMethod]
        public void T0119_ToString_SomeValue_ReturnsThatValue()
        {
            string s = "Some value";
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
            Assert.AreEqual(s, lls.ToString());
        }

        [TestMethod]
        public void T0120_GetHashCode_Null_ReturnsZero()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.AreEqual(0, lls.GetHashCode());
        }

        [TestMethod]
        public void T0121_GetHashCode_Empty_ReturnsSameAsForString()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(string.Empty);
            Assert.AreEqual(string.Empty.GetHashCode(), lls.GetHashCode());
        }

        [TestMethod]
        public void T0122_GetHashCode_SomeValue_ReturnsSameAsForString()
        {
            string s = "Some value";
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest(s);
            Assert.AreEqual(s.GetHashCode(), lls.GetHashCode());
        }

        [TestMethod]
        public void T0123_CompareTo_Null_ReturnsOne()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.AreEqual(1, lls.CompareTo(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void T0124_CompareTo_NotLengthLimitedString_ThrowsException()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            lls.CompareTo(new object());
        }

        [TestMethod]
        public void T0125_CompareTo_LengthLimitedString_Smaller_ReturnsPlusOne()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("2");
            LengthLimitedStringForTest other = new LengthLimitedStringForTest("1");
            Assert.AreEqual(1, lls.CompareTo(other));
        }

        [TestMethod]
        public void T0126_CompareTo_LengthLimitedString_Equal_ReturnsZero()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("1");
            LengthLimitedStringForTest other = new LengthLimitedStringForTest("1");
            Assert.AreEqual(0, lls.CompareTo(other));
        }

        [TestMethod]
        public void T0127_CompareTo_LengthLimitedString_Larger_ReturnsMinusOne()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("1");
            LengthLimitedStringForTest other = new LengthLimitedStringForTest("2");
            Assert.AreEqual(-1, lls.CompareTo(other));
        }

        [TestMethod]
        public void T0128_Equals_Null_ReturnsFalse()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.IsFalse(lls.Equals(null));
        }

        [TestMethod]
        public void T0129_Equals_NotLengthLimitedString_ReturnsFalse()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest();
            Assert.IsFalse(lls.Equals(new object()));
        }

        [TestMethod]
        public void T0130_Equals_LengthLimitedString_Different_ReturnsFalse()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("ABC");
            LengthLimitedStringForTest other = new LengthLimitedStringForTest("123");
            Assert.IsFalse(lls.Equals(other));
        }

        [TestMethod]
        public void T0131_Equals_LengthLimitedString_Equal_ReturnsTrue()
        {
            LengthLimitedStringForTest lls = new LengthLimitedStringForTest("ABC");
            LengthLimitedStringForTest other = new LengthLimitedStringForTest("ABC");
            Assert.IsTrue(lls.Equals(other));
        }

        #endregion Bread and butter
    }

    public sealed class LengthLimitedStringForTest : LengthLimitedString, IEquatable<LengthLimitedString>
    {
        public const int MaximumLength = 50;

        public LengthLimitedStringForTest() : base(MaximumLength)
        {
        }

        public LengthLimitedStringForTest(string value) : base(MaximumLength, value)
        {
        }

        public LengthLimitedStringForTest(LengthLimitedStringForTest other) : base(other)
        {
        }

        public new bool Equals(LengthLimitedString other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LengthLimitedStringForTest);
        }
    }
}
