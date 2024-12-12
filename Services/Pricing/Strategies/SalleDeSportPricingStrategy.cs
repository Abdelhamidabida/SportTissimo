using SportissimoProject.Models;

namespace SportissimoProject.Services.Pricing.Strategies
{
    public class SalleDeSportPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(FrequencePaiement frequencePaiement)
        {
            // Utilisation d'un switch expression pour renvoyer le prix en fonction de la fréquence de paiement
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => 80,
                FrequencePaiement.Trimestriel => 200,
                FrequencePaiement.Annuel => 500,
                _ => throw new ArgumentException("Fréquence de paiement invalide")
            };
        }
    }
}
