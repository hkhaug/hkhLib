using hkhCoreLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCoreLib
{
    [TestClass]
    public class TestDateTimeUtil
    {
        [TestMethod]
        public void T0101_NowIfMinValue_MinValue_ReturnsNow()
        {
            DateTime value = DateTime.MinValue;
            DateTime lowerBound = DateTime.Now;
            DateTime result = value.NowIfMinValue();
            DateTime upperBound = DateTime.Now;
            Assert.IsTrue(result >= lowerBound);
            Assert.IsTrue(result <= upperBound);
        }

        [TestMethod]
        public void T0102_NowIfMinValue_NotMinValue_ReturnsThis()
        {
            DateTime value = DateTime.Now;
            DateTime result = value.NowIfMinValue();
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void T0201_Max_OtherSmaller_ReturnsThis()
        {
            DateTime value = new DateTime(2015, 1, 1);
            DateTime other = new DateTime(2010, 1, 1);
            DateTime result = value.Max(other);
            Assert.AreEqual(value, result);
            Assert.AreNotEqual(other, result);
        }

        [TestMethod]
        public void T0202_Max_OtherEqual_ReturnsBoth()
        {
            DateTime value = new DateTime(2015, 1, 1);
            DateTime other = value;
            DateTime result = value.Max(other);
            Assert.AreEqual(value, result);
            Assert.AreEqual(other, result);
        }

        [TestMethod]
        public void T0203_Max_OtherGreater_ReturnsOther()
        {
            DateTime value = new DateTime(2015, 1, 1);
            DateTime other = new DateTime(2020, 1, 1);
            DateTime result = value.Max(other);
            Assert.AreEqual(other, result);
            Assert.AreNotEqual(value, result);
        }

        [TestMethod]
        public void T0301_MaxOf_LeftSmallest_ReturnsRight()
        {
            DateTime left = new DateTime(2015, 1, 1);
            DateTime right = new DateTime(2020, 1, 1);
            DateTime result = DateTimeUtil.MaxOf(left, right);
            Assert.AreEqual(right, result);
            Assert.AreNotEqual(left, result);
        }

        [TestMethod]
        public void T0302_MaxOf_Equal_ReturnsBoth()
        {
            DateTime left = new DateTime(2015, 1, 1);
            DateTime right = left;
            DateTime result = DateTimeUtil.MaxOf(left, right);
            Assert.AreEqual(left, result);
            Assert.AreEqual(right, result);
        }

        [TestMethod]
        public void T0303_MaxOf_RightSmallest_ReturnsLeft()
        {
            DateTime left = new DateTime(2015, 1, 1);
            DateTime right = new DateTime(2010, 1, 1);
            DateTime result = DateTimeUtil.MaxOf(left, right);
            Assert.AreEqual(left, result);
            Assert.AreNotEqual(right, result);
        }

        [TestMethod]
        public void T0401_Min_OtherGreater_ReturnsThis()
        {
            DateTime value = new DateTime(2015, 1, 1);
            DateTime other = new DateTime(2020, 1, 1);
            DateTime result = value.Min(other);
            Assert.AreEqual(value, result);
            Assert.AreNotEqual(other, result);
        }

        [TestMethod]
        public void T0402_Min_OtherEqual_ReturnsBoth()
        {
            DateTime value = new DateTime(2015, 1, 1);
            DateTime other = value;
            DateTime result = value.Min(other);
            Assert.AreEqual(value, result);
            Assert.AreEqual(other, result);
        }

        [TestMethod]
        public void T0403_Min_OtherSmaller_ReturnsOther()
        {
            DateTime value = new DateTime(2015, 1, 1);
            DateTime other = new DateTime(2010, 1, 1);
            DateTime result = value.Min(other);
            Assert.AreEqual(other, result);
            Assert.AreNotEqual(value, result);
        }

        [TestMethod]
        public void T0501_MinOf_LeftGreatest_ReturnsRight()
        {
            DateTime left = new DateTime(2015, 1, 1);
            DateTime right = new DateTime(2010, 1, 1);
            DateTime result = DateTimeUtil.MinOf(left, right);
            Assert.AreEqual(right, result);
            Assert.AreNotEqual(left, result);
        }

        [TestMethod]
        public void T0502_MinOf_Equal_ReturnsBoth()
        {
            DateTime left = new DateTime(2015, 1, 1);
            DateTime right = left;
            DateTime result = DateTimeUtil.MinOf(left, right);
            Assert.AreEqual(left, result);
            Assert.AreEqual(right, result);
        }

        [TestMethod]
        public void T0503_MinOf_RightGreatest_ReturnsLeft()
        {
            DateTime left = new DateTime(2015, 1, 1);
            DateTime right = new DateTime(2020, 1, 1);
            DateTime result = DateTimeUtil.MinOf(left, right);
            Assert.AreEqual(left, result);
            Assert.AreNotEqual(right, result);
        }
    }
}
