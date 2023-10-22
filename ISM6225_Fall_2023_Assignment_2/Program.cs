/* 

YOU ARE NOT ALLOWED TO MODIFY ANY FUNCTION DEFINATION's PROVIDED.
WRITE YOUR CODE IN THE RESPECTIVE QUESTION FUNCTION BLOCK


*/

using System.Text;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            //Question2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "69";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1 };
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        /*
        
        Question 1:
        You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range. A number x is considered missing if x is in the range [lower, upper] and x is not in nums. Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is included in any of the ranges, and each missing number is covered by one of the ranges.
        Example 1:
        Input: nums = [0,1,3,50,75], lower = 0, upper = 99
        Output: [[2,2],[4,49],[51,74],[76,99]]  
        Explanation: The ranges are:
        [2,2]
        [4,49]
        [51,74]
        [76,99]
        Example 2:
        Input: nums = [-1], lower = -1, upper = -1
        Output: []
        Explanation: There are no missing ranges since there are no missing numbers.

        Constraints:
        -109 <= lower <= upper <= 109
        0 <= nums.length <= 100
        lower <= nums[i] <= upper
        All the values of nums are unique.

        Time complexity: O(n), space complexity:O(1)
        */

        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            try
            {
                // Create a HashSet for efficient number lookup in the 'nums' array.
                HashSet<int> inputs = new HashSet<int>(nums);

                // Initialize a list to store the missing range(s).
                List<int> result = new List<int>();
                IList<IList<int>> output = new List<IList<int>>();

                for (int i = 0; i < nums.Length - 1; i++)
                {
                    // Exception Handling: Check if the 'nums' array is sorted in ascending order.
                    if (nums[i] > nums[i + 1])
                    {
                        throw new ArgumentException("Invalid Input: The given numbers array is not sorted in Ascending order.");
                    }
                }

                // Iterate through the range from 'lower' to 'upper'.
                for (int i = lower; i <= upper; i++)
                {
                    if (!inputs.Contains(i))
                    {
                        // If 'i' is not in 'nums', add it to the result list.
                        if (result.Count == 0)
                        {
                            result.Add(i);
                        }
                    }
                    else
                    {
                        if (result.Count == 1)
                        {
                            // If 'i' is in 'nums' and there's one missing number in the result list,
                            // set the second missing number to 'i - 1'.
                            result.Add(i - 1);
                        }
                    }

                    if (result.Count == 2)
                    {
                        // If there are two missing numbers in the result list, add this range to the output and clear the result list.
                        output.Add(result.ToList());
                        result.Clear();
                    }
                    else if (result.Count == 1 && i == upper)
                    {
                        // If there's one missing number and we've reached the upper bound, add this range to the output and clear the result list.
                        result.Add(i);
                        output.Add(result.ToList());
                        result.Clear();
                    }
                }

                // Return the list of missing ranges.
                return output;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return new List<IList<int>>(); // Return an empty list in case of an exception.
            }
        }

        /*
         
        Question 2

        Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.An input string is valid if:
        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Every close bracket has a corresponding open bracket of the same type.
 
        Example 1:

        Input: s = "()"
        Output: true
        Example 2:

        Input: s = "()[]{}"
        Output: true
        Example 3:

        Input: s = "(]"
        Output: false

        Constraints:

        1 <= s.length <= 104
        s consists of parentheses only '()[]{}'.

        Time complexity:O(n^2), space complexity:O(1)
        */

        public static bool IsValid(string s)
        {
            try
            {
                // Create a stack to hold opening brackets.
                Stack<char> brackets = new Stack<char>();

                // Create a Dictionary with Valid Bracket Pairs.
                Dictionary<char, char> Valid_brackets = new Dictionary<char, char>
                {
                    {')','(' },
                    {'}','{' },
                    {']','[' }
                };

                // Iterate through each character in the input string.
                foreach (char c in s)
                {
                    if (c == '{' || c == '(' || c == '[' || c == '}' || c == ')' || c == ']')
                    {
                        // Check if the character is an opening bracket.
                        if (c == '{' || c == '(' || c == '[')
                        {
                            brackets.Push(c); // Insert opening brackets into the stack.
                        }
                        else
                        {
                            // Check if the character is a closing bracket and if it has a corresponding opening bracket.
                            if (Valid_brackets.ContainsKey(c))
                            {
                                // Pop the top element from the stack and check if it matches the current closing bracket.
                                if (Valid_brackets[c] != brackets.Pop())
                                {
                                    return false; // Not a valid bracket pair.
                                }
                            }
                        }
                    }

                    else
                    {
                        // Exception Handling: Check whether the input string only contains brackets.
                        throw new Exception("Invalid Input: Enter the string only with Brackets.");
                    }
                }
                // Check if the stack is empty after processing all characters.
                if (brackets.Count != 0)
                {
                    return false; // Some opening brackets were not closed.
                }

                return true; // The input string contains valid bracket pairs.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return false; // Return false in case of an exception.
            }
        }

        /*

        Question 3:
        You are given an array prices where prices[i] is the price of a given stock on the ith day.You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
        Example 1:
        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.

        Example 2:
        Input: prices = [7,6,4,3,1]
        Output: 0
        Explanation: In this case, no transactions are done and the max profit = 0.
 
        Constraints:
        1 <= prices.length <= 105
        0 <= prices[i] <= 104

        Time complexity: O(n), space complexity:O(1)
        */

        public static int MaxProfit(int[] prices)
        {
            try
            {
                // Find the minimum price of the stock in the given array.
                int minimum = prices.Min();
                int min_position = 0;

                // Iterate through the 'prices' array to find the position (day) of the minimum price.
                for (int i = 0; i < prices.Length; i++)
                {
                    if (prices[i] == minimum)
                    {
                        min_position = i; // Store the position of the minimum value (the day of the minimum price) in the array.
                    }
                }

                int profit = 0;

                // Iterate from the minimum value position to the end of the array and check if the profit is maximum for each day.
                for (int i = min_position; i < prices.Length; i++)
                {
                    if (prices[i] - minimum > profit)
                    {
                        profit = prices[i] - minimum; // Update the profit if a higher profit is found for a different selling day.
                    }
                }

                return profit; // Return the maximum profit that can be obtained.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return 0; // Handle exceptions and return 0 in case of an exception.
            }
        }

        /*
        
        Question 4:
        Given a string num which represents an integer, return true if num is a strobogrammatic number.A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).
        Example 1:

        Input: num = "69"
        Output: true
        Example 2:

        Input: num = "88"
        Output: true
        Example 3:

        Input: num = "962"
        Output: false

        Constraints:
        1 <= num.length <= 50
        num consists of only digits.
        num does not contain any leading zeros except for zero itself.

        Time complexity:O(n), space complexity:O(1)
        */

        public static bool IsStrobogrammatic(string s)
        {
            try
            {
                if (int.TryParse(s, out int value))
                {
                    // Check if the given input string is a valid number by attempting to parse it as an integer.

                    for (int i = 0; i < s.Length / 2; i++)
                    {
                        // Check for the combinations of numbers at their respective positions to determine if it's a valid Strobogrammatic number.
                        if (!((s[i] == '0' && s[s.Length - 1 - i] == '0') ||
                              (s[i] == '1' && s[s.Length - 1 - i] == '1') ||
                              (s[i] == '6' && s[s.Length - 1 - i] == '9') ||
                              (s[i] == '8' && s[s.Length - 1 - i] == '8') ||
                              (s[i] == '9' && s[s.Length - 1 - i] == '6')))
                        {
                            return false; // The combination is not valid for a Strobogrammatic number.
                        }
                    }
                }
                else
                {
                    // Exception Handling: Throw an exception if the input string is not a valid number.
                    throw new Exception("Invalid Input : Enter Valid Number");
                }

                return true; // The input is a valid Strobogrammatic number.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return false; // Return false in case of an exception.
            }
        }

        /*

        Question 5:
        Given an array of integers nums, return the number of good pairs.A pair (i, j) is called good if nums[i] == nums[j] and i < j. 

        Example 1:

        Input: nums = [1,2,3,1,1,3]
        Output: 4
        Explanation: There are 4 good pairs (0,3), (0,4), (3,4), (2,5) 0-indexed.
        Example 2:

        Input: nums = [1,1,1,1]
        Output: 6
        Explanation: Each pair in the array are good.
        Example 3:

        Input: nums = [1,2,3]
        Output: 0

        Constraints:

        1 <= nums.length <= 100
        1 <= nums[i] <= 100

        Time complexity:O(n), space complexity:O(n)

        */

        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                Dictionary<int, int> Contants_Frequency = new Dictionary<int, int>();
                int count = 0;

                // Iterate through the 'nums' array to count the number of identical pairs.
                foreach (int i in nums)
                {
                    // Create a frequency Dictionary for each element in the given array.
                    if (Contants_Frequency.ContainsKey(i))
                    {
                        // If the element is already in the dictionary, increment 'count' with the frequency of the element.
                        count += Contants_Frequency[i];
                        Contants_Frequency[i]++;
                    }
                    else
                    {
                        Contants_Frequency[i] = 1;
                        // If the element is not in the dictionary, add it with a frequency of 1.
                    }
                }

                return count; // Return the count of identical pairs.

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return 0; // Handle exceptions and return 0 in case of an exception.

            }
        }

        /*
        Question 6

        Given an integer array nums, return the third distinct maximum number in this array. If the third maximum does not exist, return the maximum number.

        Example 1:

        Input: nums = [3,2,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2.
        The third distinct maximum is 1.
        Example 2:

        Input: nums = [1,2]
        Output: 2
        Explanation:
        The first distinct maximum is 2.
        The second distinct maximum is 1.
        The third distinct maximum does not exist, so the maximum (2) is returned instead.
        Example 3:

        Input: nums = [2,2,3,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2 (both 2's are counted together since they have the same value).
        The third distinct maximum is 1.
        Constraints:

        1 <= nums.length <= 104
        -231 <= nums[i] <= 231 - 1

        Time complexity:O(nlogn), space complexity:O(n)
        */

        public static int ThirdMax(int[] nums)
        {
            try
            {
                Dictionary<int, int> maximums = new Dictionary<int, int>();

                // Sorting the array in ascending order to find the maximum values.
                Array.Sort(nums);

                int position = 0;
                int value = 0;
                // Create a dictionary with the top three distinct maximum elements in the array and their positions.
                for (int i = nums.Length - 1; i >= 0; i--)
                {
                    if (!maximums.ContainsKey(nums[i]))
                    {
                        maximums[nums[i]] = ++position;
                    }
                    if (position == 3)
                    {
                        break;
                    }
                }
                // Set 'val' to either 3 (if three distinct maximums exist) or 1 (if only one or two distinct maximums exist).
                int val = maximums.Count == 3 ? 3 : 1;

                foreach (var i in maximums)
                {
                    if (i.Value == val)
                    {
                        value = i.Key; // Set 'value' to the key with the corresponding value equal to 'val'.
                    }
                }

                return value; // Return the third maximum (or maximum) value.

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return 0; // Handle exceptions and return 0 in case of an exception.
            }
        }

        /*
        
        Question 7:

        You are playing a Flip Game with your friend. You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].
        Example 1:
        Input: currentState = "++++"
        Output: ["--++","+--+","++--"]
        Example 2:

        Input: currentState = "+"
        Output: []
 
        Constraints:
        1 <= currentState.length <= 500
        currentState[i] is either '+' or '-'.

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                List<string> possibleMoves = new List<string>();

                // Iterate through the input string to find consecutive '+' characters and create possible next moves.
                for (int i = 0; i < currentState.Length - 1; i++)
                {
                    // Exception handling: Check if the input string only contains characters '+' or '-'.
                    if (currentState[i] != '+' && currentState[i] != '-')
                    {
                        throw new Exception("Enter the string with inputs '+' or '-'");
                    }
                    else
                    {
                        if (currentState[i] == '+' && currentState[i + 1] == '+')
                        {
                            // Create a new string with the '+' characters flipped to '-' as a possible next move and add it to the list.
                            char[] nextState = currentState.ToCharArray();
                            nextState[i] = '-';
                            nextState[i + 1] = '-';
                            possibleMoves.Add(new string(nextState));
                        }
                    }
                }
                return possibleMoves; // Return a list of possible next moves.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // print an error message in case of exception.
                return new List<string>(); // Return an empty list in case of an exception.
            }
        }

        /*

        Question 8:

        Given a string s, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.
        Example 1:

        Input: s = "leetcodeisacommunityforcoders"
        Output: "ltcdscmmntyfrcdrs"

        Example 2:

        Input: s = "aeiou"
        Output: ""

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static string RemoveVowels(string s)
        {
            try
            {
                // Create a HashSet containing vowels.
                HashSet<string> vowels = new HashSet<string>
                {
                    "a", "e", "i", "o", "u"
                };

                string output = "";

                // Iterate through each character in the input string 's'.
                for (int i = 0; i < s.Length; i++)
                {
                    // Check if the lowercase string representation of the character is not in the set of vowels.
                    if (!vowels.Contains(s[i].ToString().ToLower()))
                    {
                        // Add characters from the given string to 'output' if they are not vowels.
                        output += s[i];
                    }
                }
                return output; // Return the resulting string with vowels removed.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());  // print an error message in case of exception.
                return ""; // Return an empty string in case of an exception.
            }
        }

        /* Inbuilt Functions - Don't Change the below functions */
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }


        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }
    }
}