using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing.Strategies
{
    public class DanceFitPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(FrequencePaiement frequencePaiement)
        {
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => 90,
                FrequencePaiement.Trimestriel => 220,
                FrequencePaiement.Annuel => 600,
                _ => throw new ArgumentException("Fréquence de paiement invalide")
            };
        }
    }
}
