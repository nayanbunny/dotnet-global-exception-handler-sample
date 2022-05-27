using System.Globalization;

namespace DotNet.Global.Exceptions.Handler.Sample.Library.Helpers
{
    /// <summary>
    /// Application Exception Handler
    /// </summary>
	public class AppException : Exception
    {
        /// <summary>
        /// Initialize AppException with Empty Message
        /// </summary>
        public AppException() : base() { }

        /// <summary>
        /// Initialize AppException with Error Message
        /// </summary>
        /// <param name="message">The Error Message</param>
        public AppException(string message) : base(message) { }

        /// <summary>
        /// Initialize AppException with Error Message and Arguments
        /// </summary>
        /// <param name="message">The Error Message</param>
        /// <param name="args">Arguemnts</param>
        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}