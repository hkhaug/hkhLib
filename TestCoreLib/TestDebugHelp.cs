using hkhCoreLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCoreLib
{
    [TestClass]
    public class TestDebugHelp
    {
        [TestMethod]
        public void T0101_DebugString_Bool_False()
        {
            string result = false.DebugString();
            Assert.AreEqual("False/No/Off/0", result);
        }

        [TestMethod]
        public void T0102_DebugString_Bool_True()
        {
            string result = true.DebugString();
            Assert.AreEqual("True/Yes/On/1", result);
        }

        [TestMethod]
        public void T0201_DebugString_Byte_Minimum()
        {
            byte value = 0;
            string result = value.DebugString();
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void T0202_DebugString_Byte_Middle()
        {
            byte value = 128;
            string result = value.DebugString();
            Assert.AreEqual("128", result);
        }

        [TestMethod]
        public void T0203_DebugString_Byte_Maximum()
        {
            byte value = 255;
            string result = value.DebugString();
            Assert.AreEqual("255", result);
        }

        [TestMethod]
        public void T0301_DebugString_Char_Plain()
        {
            for (char value = (char)32; value < (char)127; ++value)
            {
                AssertCharPlain(value);
            }
            for (char value = (char)160; value < (char)255; ++value)
            {
                AssertCharPlain(value);
            }
            foreach (char value in "ÆØÅæøå")
            {
                AssertCharPlain(value);
            }
        }

        private static void AssertCharPlain(char value)
        {
            string result = value.DebugString();
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual('\'', result[0]);
            Assert.AreEqual(value, result[1]);
            Assert.AreEqual('\'', result[2]);
        }

        [TestMethod]
        public void T0302_DebugString_Char_Control()
        {
            for (char value = (char)0; value < (char)32; ++value)
            {
                AssertCharControl(value);
            }
            for (char value = (char)127; value < (char)160; ++value)
            {
                AssertCharControl(value);
            }
        }

        private static void AssertCharControl(char value)
        {
            string result = value.DebugString();
            ushort unicodeValue = value;
            string hexExpected = unicodeValue.ToString("x4");
            string hexResult = result.Substring(2);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual('\\', result[0]);
            Assert.AreEqual('u', result[1]);
            Assert.AreEqual(hexExpected, hexResult);
        }

        [TestMethod]
        public void T0401_DebugString_DateTime_Auto_DateOnly()
        {
            DateTime value = new DateTime(2016, 4, 20);
            string result = value.DebugString();
            Assert.AreEqual("20.04.2016", result);
        }

        [TestMethod]
        public void T0402_DebugString_DateTime_Auto_TimeOnly()
        {
            DateTime value = new DateTime(1, 1, 1, 13, 45, 20);
            string result = value.DebugString();
            Assert.AreEqual("13:45:20", result);
        }

        [TestMethod]
        public void T0403_DebugString_DateTime_Auto_DateAndTime()
        {
            DateTime value = new DateTime(2016, 4, 20, 13, 45, 20);
            string result = value.DebugString();
            Assert.AreEqual("20.04.2016 13:45:20", result);
        }

        [TestMethod]
        public void T0404_DebugString_DateTime_ForceDateOnly_OnlyDatePresent()
        {
            DateTime value = new DateTime(2016, 4, 20);
            string result = value.DebugString(DateTimeContent.DateOnly);
            Assert.AreEqual("20.04.2016", result);
        }

        [TestMethod]
        public void T0405_DebugString_DateTime_ForceDateOnly_OnlyTimePresent()
        {
            DateTime value = new DateTime(1, 1, 1, 13, 45, 20);
            string result = value.DebugString(DateTimeContent.DateOnly);
            Assert.AreEqual("01.01.0001", result);
        }

        [TestMethod]
        public void T0406_DebugString_DateTime_ForceDateOnly_DateAndTimePresent()
        {
            DateTime value = new DateTime(2016, 4, 20, 13, 45, 20);
            string result = value.DebugString(DateTimeContent.DateOnly);
            Assert.AreEqual("20.04.2016", result);
        }

        [TestMethod]
        public void T0407_DebugString_DateTime_ForceTimeOnly_OnlyDatePresent()
        {
            DateTime value = new DateTime(2016, 4, 20);
            string result = value.DebugString(DateTimeContent.TimeOnly);
            Assert.AreEqual("00:00:00", result);
        }

        [TestMethod]
        public void T0408_DebugString_DateTime_ForceTimeOnly_OnlyTimePresent()
        {
            DateTime value = new DateTime(1, 1, 1, 13, 45, 20);
            string result = value.DebugString(DateTimeContent.TimeOnly);
            Assert.AreEqual("13:45:20", result);
        }

        [TestMethod]
        public void T0409_DebugString_DateTime_ForceTimeOnly_DateAndTimePresent()
        {
            DateTime value = new DateTime(2016, 4, 20, 13, 45, 20);
            string result = value.DebugString(DateTimeContent.TimeOnly);
            Assert.AreEqual("13:45:20", result);
        }

        [TestMethod]
        public void T0410_DebugString_DateTime_ForceDateAndTime_OnlyDatePresent()
        {
            DateTime value = new DateTime(2016, 4, 20);
            string result = value.DebugString(DateTimeContent.DateAndTime);
            Assert.AreEqual("20.04.2016 00:00:00", result);
        }

        [TestMethod]
        public void T0411_DebugString_DateTime_ForceDateAndTime_OnlyTimePresent()
        {
            DateTime value = new DateTime(1, 1, 1, 13, 45, 20);
            string result = value.DebugString(DateTimeContent.DateAndTime);
            Assert.AreEqual("01.01.0001 13:45:20", result);
        }

        [TestMethod]
        public void T0412_DebugString_DateTime_ForceDateAndTime_DateAndTimePresent()
        {
            DateTime value = new DateTime(2016, 4, 20, 13, 45, 20);
            string result = value.DebugString(DateTimeContent.DateAndTime);
            Assert.AreEqual("20.04.2016 13:45:20", result);
        }

        [TestMethod]
        public void T0501_DebugString_Decimal_Minimum()
        {
            decimal value = decimal.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N2");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0502_DebugString_Decimal_Zero()
        {
            decimal value = 0m;
            string result = value.DebugString();
            string expected = value.ToString("N2");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0503_DebugString_Decimal_Maximum()
        {
            decimal value = decimal.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N2");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0601_DebugString_Double_Minimum()
        {
            double value = double.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N4");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0602_DebugString_Double_Zero()
        {
            double value = 0d;
            string result = value.DebugString();
            string expected = value.ToString("N4");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0603_DebugString_Double_Maximum()
        {
            double value = double.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N4");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0701_DebugString_Float_Minimum()
        {
            float value = float.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N2");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0702_DebugString_Float_Zero()
        {
            float value = 0f;
            string result = value.DebugString();
            string expected = value.ToString("N2");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0703_DebugString_Float_Maximum()
        {
            float value = float.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N2");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0801_DebugString_Short_Minimum()
        {
            short value = short.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0802_DebugString_Short_Zero()
        {
            short value = 0;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0803_DebugString_Short_Maximum()
        {
            short value = short.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0901_DebugString_Int_Minimum()
        {
            int value = int.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0902_DebugString_Int_Zero()
        {
            int value = 0;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T0903_DebugString_Int_Maximum()
        {
            int value = int.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1001_DebugString_Long_Minimum()
        {
            long value = long.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1002_DebugString_Long_Zero()
        {
            long value = 0;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1003_DebugString_Long_Maximum()
        {
            long value = long.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1101_DebugString_Sbyte_Minimum()
        {
            sbyte value = sbyte.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1102_DebugString_Sbyte_Zero()
        {
            sbyte value = 0;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1103_DebugString_Sbyte_Maximum()
        {
            sbyte value = sbyte.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1201_DebugString_String_Null()
        {
            string value = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            string result = value.DebugString();
            Assert.AreEqual("<null>", result);
        }

        [TestMethod]
        public void T1202_DebugString_String_Empty()
        {
            string value = string.Empty;
            string result = value.DebugString();
            Assert.AreEqual("<empty>", result);
        }

        [TestMethod]
        public void T1203_DebugString_String_RegularContent()
        {
            string value = "Just plain characters";
            string result = value.DebugString();
            string expected = '"' + value + '"';
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1204_DebugString_String_EscapedContent()
        {
            string value = "Zero: \0 Bell: \a Backspace: \b Formfeed: \f Linefeed: \n Carriage return: \r Horizontal tab: \t Vertical tab: \v Quote: \" Backslash: \\ Unicode: \u007f";
            string result = value.DebugString();
            string expected = @"""Zero: \0 Bell: \a Backspace: \b Formfeed: \f Linefeed: \n Carriage return: \r Horizontal tab: \t Vertical tab: \v Quote: \"" Backslash: \\ Unicode: \u007f""";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1301_DebugString_Ushort_Minimum()
        {
            ushort value = ushort.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1302_DebugString_Ushort_Middle()
        {
            ushort value = 32768;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1303_DebugString_Ushort_Maximum()
        {
            ushort value = ushort.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1401_DebugString_Uint_Minimum()
        {
            uint value = uint.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1402_DebugString_Uint_Middle()
        {
            uint value = 2147483648;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1403_DebugString_Uint_Maximum()
        {
            uint value = uint.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1501_DebugString_Ulong_Minimum()
        {
            ulong value = ulong.MinValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1502_DebugString_Ulong_Middle()
        {
            ulong value = 9223372036854775808;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void T1503_DebugString_Ulong_Maximum()
        {
            ulong value = ulong.MaxValue;
            string result = value.DebugString();
            string expected = value.ToString("N0");
            Assert.AreEqual(expected, result);
        }
    }
}
