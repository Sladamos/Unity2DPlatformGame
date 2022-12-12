using System;

namespace MIIProjekt.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Verifies the object is not null. If the object is null, a NullPointerException is thrown. 
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <returns>Object that the method is executed on.</returns>
        /// <exception cref="NullReferenceException">If the passed argument is null.</exception>
        public static T VerifyNotNull<T>(this T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            return obj;
        }
    }
}
