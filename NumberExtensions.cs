using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    public static class NumberExtensions
    {  

        private static string[] ones = {
    "صفر", "واحد", "اثنان", "ثلاثة", "اربع", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", 
    "عشرة", "احدى عشر", "اثنا عشر", "ثلاثة عشر", "اربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر",
};

private static string[] tens = { "صفر", "عشرة", "عشرون", "ثلاثين", "اربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعين" };

private static string[] thous = { "مئة", "الف", "مليون", "مليار", "ترليون", "كوالدريون" };

private static string fmt_negative = "negative {0}";
private static string fmt_dollars_and_cents = "{0} dollars and {1} cents";
private static string fmt_tens_ones = "{0}-{1}"; // e.g. for twenty-one, thirty-two etc. You might want to use an en-dash or em-dash instead of a hyphen.
private static string fmt_large_small = "{0} {1}"; // stitches together the large and small part of a number, like "{three thousand} {five hundred forty two}"
private static string fmt_amount_scale = "{0} {1}"; // adds the scale to the number, e.g. "{three} {million}";

public static string ToW(decimal number) {
    if (number < 0)
        return string.Format(fmt_negative, ToW(Math.Abs(number)));

    int intPortion = (int)number;
    int decPortion = (int)((number - intPortion) * (decimal) 100);

    return string.Format(fmt_dollars_and_cents, ToW(intPortion), ToW(decPortion));
}

private static string ToW(int number, string appendScale = "") {
    string numString = "";
    // if the number is less than one hundred, then we're mostly just pulling out constants from the ones and tens dictionaries
    if (number < 100) {
        if (number < 20)
            numString = ones[number];
        else {
            numString = tens[number / 10];
            if ((number % 10) > 0)
                numString = string.Format(fmt_tens_ones, numString, ones[number % 10]);
        }
    } else {
        int pow = 0; // we'll divide the number by pow to figure out the next chunk
        string powStr = ""; // powStr will be the scale that we append to the string e.g. "hundred", "thousand", etc.

        if (number < 1000) { // number is between 100 and 1000
            pow = 100; // so we'll be dividing by one hundred
            powStr = thous[0]; // and appending the string "hundred"
        } else { // find the scale of the number
            // log will be 1, 2, 3 for 1_000, 1_000_000, 1_000_000_000, etc.
            int log = (int)Math.Log(number, 1000);
            // pow will be 1_000, 1_000_000, 1_000_000_000 etc.
            pow = (int)Math.Pow(1000, log);
            // powStr will be thousand, million, billion etc.
            powStr = thous[log];
        }

        // we take the quotient and the remainder after dividing by pow, and call ToW on each to handle cases like "{five thousand} {thirty two}" (curly brackets added for emphasis)
        numString = string.Format(fmt_large_small, ToW(number / pow, powStr), ToW(number % pow)).Trim();
    }

    // and after all of this, if we were passed in a scale from above, we append it to the current number "{five} {thousand}"
    return string.Format(fmt_amount_scale, numString, appendScale).Trim();
}
    //      private const string negativeWord = "negative";
    // private static readonly Dictionary<ulong, string> _wordMap = new Dictionary<ulong, string>
    // {
    //     [1_000_000_000_000_000_000] = "كوانتليون",
    //     [1_000_000_000_000_000] = "كواردليون",
    //     [1_000_000_000_000] = "ترليون",
    //     [1_000_000_000] = "مليار",
    //     [1_000_000] = "مليون",
    //     [1_000] = "الف",
    //     [100] = "مئة",
    //     [90] = "تسعون",
    //     [80] = "ثمانون",
    //     [70] = "سبعون",
    //     [60] = "ستين",
    //     [50] = "خمسون",
    //     [40] = "اربعون",
    //     [30] = "ثلاثون",
    //     [20] = "عشرون",
    //     [19] = "تسعة عشر",
    //     [18] = "ثمانية عشر",
    //     [17] = "سبعة عشر",
    //     [16] = "ستة عشر",
    //     [15] = "خمسة عشر",
    //     [14] = "اربعة عشر",
    //     [13] = "ثلاثة عشر",
    //     [12] = "اثنا عشر",
    //     [11] = "احدى عشر",
    //     [10] = "عسرة",
    //     [9] = "تسعة",
    //     [8] = "ثمانية",
    //     [7] = "سبعة",
    //     [6] = "ستة",
    //     [5] = "خمسة",
    //     [4] = "اربعة",
    //     [3] = "ثلاثة",
    //     [2] = "اثنان",
    //     [1] = "واحد",
    //     [0] = "صفر"
    // };

    // public static string ToW(this short num)
    // {
    //     var words = ToW((ulong)Math.Abs(num));
    //     return num < 0 ? $"{negativeWord} {words}" : words;
    // }

    // public static string ToW(this ushort num)
    // {
    //     return ToW((ulong)num);
    // }

    // public static string ToW(this int num)
    // {
    //     var words = ToW((ulong)Math.Abs(num));
    //     return num < 0 ? $"{negativeWord} {words}" : words;
    // }

    // public static string ToW(this uint num)
    // {
    //     return ToW((ulong)num);
    // }

    // public static string ToW(this long num)
    // {
    //     var words = ToW((ulong)Math.Abs(num));
    //     return num < 0 ? $"{negativeWord} {words}" : words;
    // }

    // public static string ToW(this ulong num)
    // {
    //     var sb = new StringBuilder();
    //     var delimiter = String.Empty;

    //     void AppendWords(ulong dividend)
    //     {
    //         void AppendDelimitedWord(ulong key)
    //         {
    //             sb.Append(delimiter);
    //             sb.Append(_wordMap[key]);
    //             delimiter = 20 <= key && key < 100 ? "-" : " ";
    //         }

    //         if (_wordMap.ContainsKey(dividend))
    //         {
    //             AppendDelimitedWord(dividend);
    //         }
    //         else
    //         {
    //             var divisor = _wordMap.First(m => m.Key <= dividend).Key;
    //             var quotient = dividend / divisor;
    //             var remainder = dividend % divisor;

    //             if (quotient > 0 && divisor >= 100)
    //             {
    //                 AppendWords(quotient);
    //             }

    //             AppendDelimitedWord(divisor);

    //             if (remainder > 0)
    //             {   
    //                 AppendWords(remainder);
    //             }
    //         }
    //     }

    //     AppendWords(num);
    //     return sb.ToString();
    // }    
        
    }
}