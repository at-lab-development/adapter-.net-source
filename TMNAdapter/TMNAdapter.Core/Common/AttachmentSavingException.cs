using System;

namespace TMNAdapter.Core.Common
{
    internal class AttachmentSavingException : Exception
    {
        public AttachmentSavingException(string message) : base(message)
        {
        }
    }
}
