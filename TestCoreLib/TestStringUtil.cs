using hkhCoreLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCoreLib
{
    [TestClass]
    public class TestStringUtil
    {
        [TestMethod]
        public void T0101_EmptyIfNull_Null_ReturnsEmpty()
        {
            string value = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            string result = value.EmptyIfNull();
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void T0102_EmptyIfNull_Empty_ReturnsEmpty()
        {
            string value = string.Empty;
            string result = value.EmptyIfNull();
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void T0103_EmptyIfNull_ArbitraryNonNullValue_ReturnsValue()
        {
            TestEmptyIfNull_ArbitraryNonNullValue_ReturnsValue(string.Empty);
            TestEmptyIfNull_ArbitraryNonNullValue_ReturnsValue("");
            TestEmptyIfNull_ArbitraryNonNullValue_ReturnsValue("This is just some arbitrary text.");
        }

        private static void TestEmptyIfNull_ArbitraryNonNullValue_ReturnsValue(string value)
        {
            string result = value.EmptyIfNull();
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void T0201_IfMissingReplaceWith_WasNull_ReturnsReplacementValue()
        {
            string destination = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            string result = destination.IfMissingReplaceWith("New value");
            Assert.AreEqual("New value", result);
        }

        [TestMethod]
        public void T0202_IfMissingReplaceWith_WasEmpty_SetToPassedValue()
        {
            string destination = string.Empty;
            string result = destination.IfMissingReplaceWith("New value");
            Assert.AreEqual("New value", result);
        }

        [TestMethod]
        public void T0203_IfMissingReplaceWith_WasWhitespaceOnly_SetToPassedValue()
        {
            const string destination = " \t\r\n";
            string result = destination.IfMissingReplaceWith("New value");
            Assert.AreEqual("New value", result);
        }

        [TestMethod]
        public void T0204_IfMissingReplaceWith_WasNotEmpty_KeepsOriginalValue()
        {
            const string destination = "Original value";
            string result = destination.IfMissingReplaceWith("New value");
            Assert.AreEqual("Original value", result);
        }

        [TestMethod]
        public void T0301_Left_WasNull_ReturnsEmpty()
        {
            string destination = null;
            string result = destination.Left(10);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void T0302_Left_WasEmpty_ReturnsEmpty()
        {
            string destination = string.Empty;
            string result = destination.Left(10);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void T0303_Left_WasShorter_ReturnsIdentical()
        {
            string destination = "abcde";
            string result = destination.Left(10);
            Assert.AreEqual(destination, result);
        }

        [TestMethod]
        public void T0304_Left_WasRequestedLength_ReturnsIdentical()
        {
            string destination = "abcde";
            string result = destination.Left(5);
            Assert.AreEqual(destination, result);
        }

        [TestMethod]
        public void T0305_Left_WasLonger_ReturnsOnlyLeftPartWithRequestedLength()
        {
            string destination = "abcde";
            string result = destination.Left(3);
            Assert.AreEqual("abc", result);
        }

        [TestMethod]
        public void T0401_Right_WasNull_ReturnsEmpty()
        {
            string destination = null;
            string result = destination.Right(10);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void T0402_Right_WasEmpty_ReturnsEmpty()
        {
            string destination = string.Empty;
            string result = destination.Right(10);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void T0403_Right_WasShorter_ReturnsIdentical()
        {
            string destination = "abcde";
            string result = destination.Right(10);
            Assert.AreEqual(destination, result);
        }

        [TestMethod]
        public void T0404_Right_WasRequestedLength_ReturnsIdentical()
        {
            string destination = "abcde";
            string result = destination.Right(5);
            Assert.AreEqual(destination, result);
        }

        [TestMethod]
        public void T0405_Right_WasLonger_ReturnsOnlyRightPartWithRequestedLength()
        {
            string destination = "abcde";
            string result = destination.Right(3);
            Assert.AreEqual("cde", result);
        }

        [TestMethod]
        public void T0501_SplitIntoChunks_Empty_ReturnsZeroChunks()
        {
            TestSplitIntoChunks_Empty_ReturnsZeroChunks(1);
            TestSplitIntoChunks_Empty_ReturnsZeroChunks(100);
        }

        private static void TestSplitIntoChunks_Empty_ReturnsZeroChunks(int chunkSize)
        {
            string value = string.Empty;
            IEnumerable<string> result = value.SplitIntoChunks(chunkSize);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void T0502_SplitIntoChunks_LengthMatchFullChunks()
        {
            TestSplitIntoChunks_LengthMatchFullChunks(1);
            TestSplitIntoChunks_LengthMatchFullChunks(5);
            TestSplitIntoChunks_LengthMatchFullChunks(10);
            TestSplitIntoChunks_LengthMatchFullChunks(37);
            TestSplitIntoChunks_LengthMatchFullChunks(60);
        }

        private static void TestSplitIntoChunks_LengthMatchFullChunks(int chunkSize)
        {
            string value = MakeString(chunkSize * 3);
            string reverse = new string(value.Reverse().ToArray());
            TestSplitIntoChunks(value, chunkSize);
            TestSplitIntoChunks(reverse, chunkSize);
        }

        [TestMethod]
        public void T0503_SplitIntoChunks_LengthMeansLastChunkIsShorter()
        {
            TestSplitIntoChunks_LengthMeansLastChunkIsShorter(2);
            TestSplitIntoChunks_LengthMeansLastChunkIsShorter(5);
            TestSplitIntoChunks_LengthMeansLastChunkIsShorter(10);
            TestSplitIntoChunks_LengthMeansLastChunkIsShorter(37);
            TestSplitIntoChunks_LengthMeansLastChunkIsShorter(60);
        }

        private static void TestSplitIntoChunks_LengthMeansLastChunkIsShorter(int chunkSize)
        {
            string value = MakeString(chunkSize * 3 - 1);
            string reverse = new string(value.Reverse().ToArray());
            TestSplitIntoChunks(value, chunkSize);
            TestSplitIntoChunks(reverse, chunkSize);
        }

        private static void TestSplitIntoChunks(string value, int chunkSize)
        {
            List<string> result = value.SplitIntoChunks(chunkSize).ToList();
            int pos = 0;
            int chunkNo = 0;
            while (chunkNo < result.Count)
            {
                string chunk = result[chunkNo];
                string expected = value.Substring(pos, Math.Min(chunkSize, value.Length - pos));
                Assert.AreEqual(expected, chunk);
                pos += chunkSize;
                ++chunkNo;
            }
        }

        private static string MakeString(int length)
        {
            const string source = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz.";
            StringBuilder sb = new StringBuilder(length);
            int soFar = 0;
            while (soFar < length)
            {
                sb.Append(source.Substring(0, Math.Min(source.Length, length - soFar)));
                soFar += source.Length;
            }
            return sb.ToString();
        }
    }
}
