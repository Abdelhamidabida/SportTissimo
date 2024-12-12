using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing.Strategies
{
    public class BoxePricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(FrequencePaiement frequencePaiement)
        {
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => 55,
                FrequencePaiement.Trimestriel => 150,
                FrequencePaiement.Annuel => 580,
                _ => throw new ArgumentException("Fréquence de paiement invalide")
            };
        }
    }
}
