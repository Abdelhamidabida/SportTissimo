using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing.Strategies
{
    public class YogaPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(FrequencePaiement frequencePaiement)
        {
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => 50,
                FrequencePaiement.Trimestriel => 140,
                FrequencePaiement.Annuel => 500,
                _ => throw new ArgumentException("Fréquence de paiement invalide")
            };
        }
    }
}
