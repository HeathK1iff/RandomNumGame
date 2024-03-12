using Microsoft.Extensions.Configuration;
using RandomNumGame.Core.Abstractions;

namespace RandomNumGame.Core.Strategies
{
    public class StrategyCreator : IStrategyCreator
    {
        public static string RangeTypeKey = "RangeType";
        public static string AutoTypeKey = "auto";
        public static string ManualTypeKey = "manual";
        private enum StrategyType { Auto, Manual }
        
        private readonly IConfiguration _confuguration;

        public StrategyCreator(IConfiguration confuguration)
        {
            _confuguration = confuguration;
        }


        public INumberGameStrategy Create()
        {
            switch (GetRangeTypeKeyOrDefault())
            {
                case StrategyType.Auto:
                    return new RandomNumberGameStrategy();
                default:
                    return new PredefinedNumberGameStrategy(_confuguration);
            }
        }

        private StrategyType GetRangeTypeKeyOrDefault()
        {
            string? value = _confuguration[RangeTypeKey];

            if (string.IsNullOrWhiteSpace(value))
            {
                return StrategyType.Manual;
            }

            if (value.Equals(AutoTypeKey, StringComparison.InvariantCultureIgnoreCase))
            {
                return StrategyType.Auto;
            }

            return StrategyType.Manual;
        }


    }
}
