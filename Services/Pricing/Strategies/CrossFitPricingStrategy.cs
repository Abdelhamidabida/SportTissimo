using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing.Strategies
{
    public class CrossFitPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(FrequencePaiement frequencePaiement)
        {
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => 60,
                FrequencePaiement.Trimestriel => 150,
                FrequencePaiement.Annuel => 400,
                _ => throw new ArgumentException("Fréquence de paiement invalide")
            };
        }
    }
}
