using System;

namespace TMNAdapter.Core.Common
{
    internal class SaveAttachmentException : Exception
    {
        public SaveAttachmentException(string message) : base(message)
        {
        }
    }
}
