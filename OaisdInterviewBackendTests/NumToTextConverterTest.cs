using OaisdInterviewBackend.Utils;

namespace OaisdInterviewBackendTests
{
    public class NumToTextConverterTest
    {
        [Fact]
        public void Convert_NormalNumber_IsCorrect()
        {
            Assert.StartsWith("one thousand two hundred thirty four", NumToTextConverter.Convert(1234));
        }

        [Fact]
        public void Convert_NegativeNumber_IsCorrect()
        {
            Assert.StartsWith("negative one thousand one hundred eleven", NumToTextConverter.Convert(-1111));
        }

        [Fact]
        public void Convert_Zero_IsCorrect()
        {
            Assert.Equal("zero", NumToTextConverter.Convert(0));
        }

        [Fact]
        public void Convert_OnesPlace_IsCorrect()
        {
            Assert.Equal("zero", NumToTextConverter.Convert(0));
            Assert.Equal("one", NumToTextConverter.Convert(1));
            Assert.Equal("two", NumToTextConverter.Convert(2));
            Assert.Equal("three", NumToTextConverter.Convert(3));
            Assert.Equal("four", NumToTextConverter.Convert(4));
            Assert.Equal("five", NumToTextConverter.Convert(5));
            Assert.Equal("six", NumToTextConverter.Convert(6));
            Assert.Equal("seven", NumToTextConverter.Convert(7));
            Assert.Equal("eight", NumToTextConverter.Convert(8));
            Assert.Equal("nine", NumToTextConverter.Convert(9));
        }

        [Fact]
        public void Convert_TensPlace_IsCorrect()
        {
            Assert.Equal("ten", NumToTextConverter.Convert(10));
            Assert.Equal("twenty", NumToTextConverter.Convert(20));
            Assert.Equal("thirty", NumToTextConverter.Convert(30));
            Assert.Equal("fourty", NumToTextConverter.Convert(40));
            Assert.Equal("fifty", NumToTextConverter.Convert(50));
            Assert.Equal("sixty", NumToTextConverter.Convert(60));
            Assert.Equal("seventy", NumToTextConverter.Convert(70));
            Assert.Equal("eighty", NumToTextConverter.Convert(80));
            Assert.Equal("ninety", NumToTextConverter.Convert(90));
        }

        [Fact]
        public void Convert_Teens_IsCorrect()
        {
            Assert.Equal("eleven", NumToTextConverter.Convert(11));
            Assert.Equal("twelve", NumToTextConverter.Convert(12));
            Assert.Equal("thirteen", NumToTextConverter.Convert(13));
            Assert.Equal("fourteen", NumToTextConverter.Convert(14));
            Assert.Equal("fifteen", NumToTextConverter.Convert(15));
            Assert.Equal("sixteen", NumToTextConverter.Convert(16));
            Assert.Equal("seventeen", NumToTextConverter.Convert(17));
            Assert.Equal("eighteen", NumToTextConverter.Convert(18));
            Assert.Equal("nineteen", NumToTextConverter.Convert(19));
        }

        [Fact]
        public void Convert_DifferentMagnitudes_TextContainsMagnitude()
        {
            Assert.Contains("hundred", NumToTextConverter.Convert(100));
            Assert.Contains("thousand", NumToTextConverter.Convert(1000));
            Assert.Contains("million", NumToTextConverter.Convert(1000000));
            Assert.Contains("billion", NumToTextConverter.Convert(1000000000));
            Assert.Contains("trillion", NumToTextConverter.Convert(1000000000000));
            Assert.Contains("quadrillion", NumToTextConverter.Convert(1000000000000000));
            Assert.Contains("quintillion", NumToTextConverter.Convert(1000000000000000000));
        }

        [Fact]
        public void Convert_ZeroInHundredsPlace_IsCorrect()
        {
            Assert.Equal("one thousand ten", NumToTextConverter.Convert(1010));
        }

        [Fact]
        public void Convert_ZeroInTensPlace_IsCorrect()
        {
            Assert.Equal("one thousand one", NumToTextConverter.Convert(1001));
        }

        [Fact]
        public void Convert_ZeroInOnesPlace_IsCorrect()
        {
            Assert.Equal("one thousand", NumToTextConverter.Convert(1000));
        }

        [Fact]
        public void Convert_ZeroInHundredAndTensPlace_IsCorrect()
        {
            Assert.Equal("one thousand one", NumToTextConverter.Convert(1001));
        }

        [Fact]
        public void Convert_ZeroInHundredAndOnesPlace_IsCorrect()
        {
            Assert.Equal("one thousand twenty", NumToTextConverter.Convert(1020));
        }

        [Fact]
        public void Convert_ZeroInTensAndOnesPlace_IsCorrect()
        {
            Assert.Equal("one thousand one hundred", NumToTextConverter.Convert(1100));
        }
    }
}