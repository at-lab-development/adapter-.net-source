using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace TMNAdapter.Core.Common.Validation
{
    /// <summary>
    /// Helper class that provides a set of validation methods
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates whether a specified <paramref name="argument"/> is null
        /// </summary>
        /// <param name="argument">The argument to test</param>
        /// <param name="parameterName">The name of tested argument</param>
        /// <exception cref="ArgumentNullException">Thrown when argument test fails</exception>
        [ContractArgumentValidator]
        public static void NotNull(object argument, string parameterName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(parameterName, "The parameter cannot be null.");
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Validates whether a specified <paramref name="argument"/> is null or empty
        /// </summary>
        /// <param name="argument">The argument to test</param>
        /// <param name="parameterName">The name of tested argument</param>
        /// <exception cref="ArgumentNullException">Thrown when argument test fails</exception>
        [ContractArgumentValidator]
        public static void NotNullOrEmpty(string argument, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(parameterName, "The parameter cannot be null or empty.");
            }
            
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Validates whether a specified <paramref name="argument"/> is matching the <paramref name="pattern"/>
        /// </summary>
        /// <param name="argument">The argument to test</param>
        /// <param name="parameterName">The name of tested argument</param>
        /// <param name="pattern">The pattern string for argument test</param>
        /// <exception cref="ArgumentException">Thrown when argument test fails</exception>
        [ContractArgumentValidator]
        public static void MatchPattern(string argument, string parameterName, string pattern)
        {
            NotNullOrEmpty(argument, parameterName);

            var regex = new Regex(pattern);
            if (!regex.IsMatch(argument))
            {
                throw new ArgumentException("The parameter doesn't match the pattern.", parameterName);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Validates whether a specified argument is in range
        /// </summary>
        /// <param name="argument">The argument to test</param>
        /// <param name="parameterName">The name of tested argument</param>
        /// <param name="min">Minimal accepted argument value</param>
        /// <param name="max">Maximum accepted argument value</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when argument test fails</exception>
        [ContractArgumentValidator]
        public static void InRange(int argument, string parameterName, int min, int max)
        {
            if(argument < min || argument > max)
            {
                throw new ArgumentOutOfRangeException(parameterName, argument, "The parameter is out of range.");
            }

            Contract.EndContractBlock();
        }
    }
}
