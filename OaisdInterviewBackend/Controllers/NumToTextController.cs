using System.Net.Mime;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

using OaisdInterviewBackend.Models;
using OaisdInterviewBackend.Utils;

namespace OaisdInterviewBackend.Controllers
{
    [ApiController]
    [Route("numToText")]
    public class NumToTextController : ControllerBase
    {
        public NumToTextController() : base() { }

        /// <summary>
        ///     Accepts a string containing a list of numbers delimited by commas and returns a list of 
        ///     these numbers with their text representations.
        /// </summary>
        /// <param name="input">The string input containing all of the numbers delimited by commas</param>
        /// <response code="200">Returns an list containing each number and its text representation</response>
        /// <response code="400">Returns a BadRequest response if the input string contains any of the following:
        ///     <br /> 1. Nothing (empty string)
        ///     <br /> 2. Characters other than numbers, commas, and negative sign (-)
        ///     <br /> 3. More than one negative sign per number
        ///     <br /> 4. Numbers outside of the long range: -9223372036854775807 to 9223372036854775807
        /// </response>
        /// <returns>An object containing each number and its text representation</returns>
        [HttpGet("{input}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<ViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConvertToText(string input)
        {
            try
            {
                input = input.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    return BadRequest("Must provide an input string");
                }

                if (Regex.IsMatch(input, @"[^0-9,-]"))
                {
                    return BadRequest("Input may only contain numbers and commas");
                }

                List<long> numbers = new List<long>();
                string[] substrings = input.Split(',');

                foreach (string s in substrings)
                {
                    if(Regex.IsMatch(s, @"(?!^)-"))
                    {
                        return BadRequest($"Negative sign must be at beginning of number: {s}");
                    }

                    try
                    {
                        long n = long.Parse(s);

                        // NumToTextConverter.Convert() multiplies negative numbers by -1 and Int64.MinValue * -1 == Int64.MaxValue + 1 which overflows and results in a negative value
                        if (n == Int64.MinValue) throw new OverflowException();

                        numbers.Add(n);
                    }
                    catch (OverflowException)
                    {
                        return BadRequest($"Number is too large: {s}");
                    }
                }

                List<ViewModel> result = new List<ViewModel>();
                foreach (long n in numbers)
                {
                    result.Add(new ViewModel()
                    {
                        Num = n,
                        Text = NumToTextConverter.Convert(n)
                    });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
