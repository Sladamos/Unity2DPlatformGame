using System;

namespace MIIProjekt.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Verifies the object is not null. If the object is null, an ArgumentNullException is thrown. 
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <returns>Object that the method is executed on.</returns>
        /// <exception cref="ArgumentNullException">If the passed argument is null.</exception>
        public static T VerifyNotNull<T>(this T obj)
        {
            return VerifyNotNull(obj, String.Empty);
        }

        /// <summary>
        /// Verifies the object is not null. If the object is null, a ArgumentNullException is thrown. 
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <param name="message">Message passed to the ArgumentNullException ctor that is called if the object is null.</message>
        /// <returns>Object that the method is executed on.</returns>
        /// <exception cref="ArgumentNullException">If the passed argument is null.</exception>
        public static T VerifyNotNull<T>(this T obj, string message)
        {
            if (obj == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    throw new ArgumentNullException(message);
                }
            }

            return obj;
        }
    }
}
