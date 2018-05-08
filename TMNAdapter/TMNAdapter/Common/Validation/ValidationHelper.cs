using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TMNAdapter.Common.Validation
{
    static class ValidationHelper
    {
        [ContractArgumentValidator]
        public static void NotNull(object argument, string parameterName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(parameterName, "The parameter cannot be null.");
            }

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void NotNullOrEmpty(string argument, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(parameterName, "The parameter cannot be null or empty.");
            }
            
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MatchPattern(string argument, string parameterName, string pattern)
        {
            NotNullOrEmpty(argument, parameterName);

            var regex = new Regex(pattern);
            if (!regex.IsMatch(argument))
            {
                throw new ArgumentException(parameterName, "The parameter cannot doesn't match the pattern.");
            }

            Contract.EndContractBlock();
        }
    }
}
