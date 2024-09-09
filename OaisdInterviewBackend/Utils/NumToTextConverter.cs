namespace OaisdInterviewBackend.Utils
{
    public static class NumToTextConverter
    {
        private static readonly string[] DIGITS = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        private static readonly string[] MAGNITUDES = ["thousand", "million", "billion", "trillion", "quadrillion", "quintillion"];

        private static readonly Dictionary<byte, string> SPECIAL_TENS_PLACES = new Dictionary<byte, string>()
        {
            { 2, "twenty" },
            { 3, "thirty" },
            { 5, "fifty" },
            { 8, "eighty" }
        };

        private static readonly Dictionary<byte, string> SPECIAL_TEENS = new Dictionary<byte, string>()
        {
            { 0, "ten" },
            { 1, "eleven" },
            { 2, "twelve" },
            { 3, "thirteen" },
            { 5, "fifteen" },
            { 8, "eighteen" }
        };

        /*
         * This method takes a numeric value as a parameter and returns the English language representation 
         * of that number. The algorithm used by this method can be simplified as:
         *   1. If the number is 0, return "zero"
         *   2. If the number is negative, use the absolute value of that number and add "negative" to the result text
         *   3. Separate the number into individual digits (1234567890 => [1,2,3,4,5,6,7,8,9,0])
         *   4. Group these digits into chunks of three or less based on their "magnitude" ([1,2,3,4,5,6,7,8,9,0] => [[1],[234],[567],[890]])
         *     * As if the original number was separated by commas (1,234,567,890) 
         *   5. For each chunk, construct the "hundred" representation of the number ([234] => "two hundred thirty four") 
         *      and then add the magnitude
         *     a. For each digit in the chunk, identify if it is in the hundreds place (2), the tens place (3), or the ones places (4):
         *         hundreds: 
         *             if the digit is 0, then ignore it and move to the next number 
         *             else, add the digit's text to the result and the string "hundred" (two hundred)
         *         tens:
         *             if the digit is 0, then ignore it and move to the next number
         *             else if the digit is 1, then it is a "teen"
         *                 if the next digit has a special teen representation (twelve), then add the special word to the result and ignore the next digit
         *                 else, add the digit's text with the suffix "teen" (fourteen) to the result and ignore the next digit
         *             else if the digit has a special tens place word (twenty), then add the special word to the result
         *             else, add the digit's text with the suffix "ty" (thirty) to the result
         *         ones:
         *             if the digit is 0, then ignore it and move to the next number
         *             else, add the digit's text to the result (four)
         *         * If the digit is 0, then do not include it in the result text unless it is the only digit in the input number
         *     b. Add the magnitude of the chunk to the result text or ignore if the final chunk ([1] => "billion"; [234] => "million"; [890] => ignore)
         */
        public static string Convert(long value)
        {
            List<string> numStrings = new List<string>();
            Stack<byte> digits = new Stack<byte>();

            // the only time we should include the word "zero" is if the number is 0, so we check here to not have to complicate the code later
            if (value == 0)
            {
                return DIGITS[value];
            }

            // if value is negative, add "negative" and use absolute value to reduce complexity
            if (value < 0)
            {
                numStrings.Add("negative");
                value *= -1;
            }

            // seperate the number out into a stack of digits
            for (long i = value; i != 0; i /= 10)
            {
                Console.WriteLine(i % 10);
                digits.Push(System.Convert.ToByte(i % 10));
            }

            // loop through the digits, removing them from the top of the stack as we go
            while (digits.Count > 0)
            {
                byte d = digits.Pop();

                // digit is in hundreds place, evaluate it and move to the next digit
                if (digits.Count % 3 == 2)
                {
                    if (d != 0)
                    {
                        numStrings.Add(DIGITS[d]);
                        numStrings.Add("hundred");
                    }

                    d = digits.Pop();
                }

                // digit is in tens place, evaluate it and move to the next digit
                if (digits.Count % 3 == 1)
                {
                    if (d != 0)
                    {
                        // if the number is a "teen", get the next digit and use it to get text
                        if (d == 1)
                        {
                            byte nextDigit = digits.Pop();
                            numStrings.Add(SPECIAL_TEENS.ContainsKey(nextDigit) ? SPECIAL_TEENS[nextDigit] : $"{DIGITS[nextDigit]}teen");

                            // if this was the last digit, break out of the loop
                            if (digits.Count == 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            numStrings.Add(SPECIAL_TENS_PLACES.ContainsKey(d) ? SPECIAL_TENS_PLACES[d] : $"{DIGITS[d]}ty");
                        }

                        d = digits.Pop();
                    }
                }

                // digit is in ones place
                if (digits.Count % 3 == 0)
                {
                    if (d != 0)
                    {
                        numStrings.Add(DIGITS[d]);
                    }
                }

                // add the magnitude if appropriate
                int magIndex = digits.Count / 3 - 1;
                if (magIndex >= 0)
                {
                    numStrings.Add(MAGNITUDES[magIndex]);
                }
            }

            return string.Join(' ', numStrings);
        }
    }
}
