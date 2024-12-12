using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing
{
    public interface IPricingStrategy
    {
        double CalculatePrice(FrequencePaiement frequencePaiement);
    }
}
