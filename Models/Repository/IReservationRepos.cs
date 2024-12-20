﻿using SportissimoProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportissimoProject.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync(); // Récupérer toutes les réservations
        Task<Reservation> GetByIdAsync(string id);   // Récupérer une réservation par ID
        Task AddAsync(Reservation reservation);       // Ajouter une nouvelle réservation
        Task UpdateAsync(Reservation reservation);    // Mettre à jour une réservation
        Task DeleteAsync(string id);                  // Supprimer une réservation
        Task<List<Reservation>> GetReservationsByTerrainAndDateAsync(string terrainId, DateTime date);
        // Méthodes spécifiques pour les réservations
        // Ajouter une méthode pour vérifier les conflits de réservation
        Task<bool> HasConflictAsync(string terrainId, DateTime dateDebut, DateTime dateFin);

        // Les autres méthodes de l'interface
        
        Task<IEnumerable<Reservation>> GetReservationsByClientAsync(string clientId); // Par client
        Task<IEnumerable<Reservation>> GetReservationsByTerrainAsync(string terrainId); // Par terrain
        Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate); // Par date
        Task<bool> IsTerrainAvailableAsync(string terrainId, DateTime startDate, DateTime endDate); // Vérifier la disponibilité
    }
}
