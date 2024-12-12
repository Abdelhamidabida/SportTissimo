using SportissimoProject.Models;
using SportissimoProject.Services.Pricing.Strategies;
using SportissimoProject.Services.Pricing;

namespace SportissimoProject.Services.Pricing
{
    public class PricingStrategyFactory
    {
        public IPricingStrategy GetStrategy(TypeAbonnement typeGym)
        {
            // Retourner la stratégie appropriée basée sur le type de gym
            return typeGym switch
            {
                TypeAbonnement.SalleDeSport => new SalleDeSportPricingStrategy(),
                TypeAbonnement.CrossFit => new CrossFitPricingStrategy(),
                TypeAbonnement.Yoga => new YogaPricingStrategy(),
                TypeAbonnement.Boxe => new BoxePricingStrategy(),
                TypeAbonnement.Dance => new DanceFitPricingStrategy(),
                TypeAbonnement.Fitness => new FitnessFitPricingStrategy(),
                _ => throw new InvalidOperationException($"Type d'abonnement non supporté : {typeGym}")
            };
        }
    }
}
