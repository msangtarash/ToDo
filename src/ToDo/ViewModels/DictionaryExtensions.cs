using Prism.Navigation;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static NavigationParameters ToNavParams(this IDictionary<string, object> parameters)
        {
            NavigationParameters result = new NavigationParameters();

            foreach (var param in parameters)
            {
                result.Add(param.Key, param.Value);
            }

            return result;
        }
    }
}
