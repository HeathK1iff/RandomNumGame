using Microsoft.Extensions.Configuration;
using RandomNumGame.Core.Types;

namespace RandomNumGame.Core.Strategies
{
    public class PredefinedNumberGameStrategy : NumberGameStrategy
    {
        public static string StartRangeKey = "StartRange";
        public static string EndRangeKey = "EndRange";

        private readonly IConfiguration _configuration;

        public PredefinedNumberGameStrategy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override NumberRange CreateRange()
        {
            return new NumberRange()
            {
                StartRange = GetStartRangeFromConfiguration(),
                EndRange = GetEndRangeFromConfiguration()
            };
        }

        private int GetStartRangeFromConfiguration()
        {
            string? value = _configuration[StartRangeKey];
            
            if (!int.TryParse(value, out int number)) 
            {
               throw new ArgumentNullException($"'{StartRangeKey}' key is not found");
            }

            return number;
        }

        private int GetEndRangeFromConfiguration()
        {
            string? value = _configuration[EndRangeKey];

            if (!int.TryParse(value, out int number))
            {
                throw new ArgumentNullException($"'{EndRangeKey}' key is not found");
            }

            return number;
        }

    }   
}