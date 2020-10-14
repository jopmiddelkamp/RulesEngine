using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RulesEngine.Actions;
using RulesEngine.Models;

namespace DemoApp.Actions
{
    public class LowerCreditHistoryAction : ActionBase
    {
        private readonly ILogger _logger;

        public LowerCreditHistoryAction(ILogger logger)
        {
            _logger = logger;
        }
        
        public override ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var personalData = ruleParameters.FirstOrDefault(r => r.Name == "personalData")?.Value as dynamic;
            if (personalData == null)
            {
                _logger.LogError($"The personal data is not available in {nameof(LowerCreditHistoryAction)}.");
                return new ValueTask<object>(null);
            }

            var oldValue = personalData.Value.creditHistory as string;
            var newValue = "bad";
            personalData.creditHistory = newValue;
            _logger.LogInformation($"Changed the creditHistory from {oldValue} to {newValue}.");
            
            return new ValueTask<object>(newValue);
        }
    }
}