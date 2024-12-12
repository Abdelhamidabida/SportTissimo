using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing.Strategies
{
    public class FitnessFitPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(FrequencePaiement frequencePaiement)
        {
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => 100,
                FrequencePaiement.Trimestriel => 250,
                FrequencePaiement.Annuel => 600,
                _ => throw new ArgumentException("Fréquence de paiement invalide")
            };
        }
    }
}
