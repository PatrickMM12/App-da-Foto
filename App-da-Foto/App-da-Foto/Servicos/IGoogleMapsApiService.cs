using App_da_Foto.Models;
using System.Threading.Tasks;

namespace App_da_Foto.Servicos
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}

