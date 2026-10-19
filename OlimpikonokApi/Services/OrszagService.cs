namespace OlimpikonokApi.Services;
using OlimpikonokApi.Models;
using System.Security.Cryptography.X509Certificates;

public class OrszagService
{
    public List<Orszag> GetAllOrszagok()
    {
        try
        {
            using (var context = new OlimpikonokContext())
            {
                return context.Orszags.ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("hiba történt az országok lekérése során" + ex.Message);
        }

    }
    public Orszag GetOrszagById(int id)
    {
        try
        {
            using (var context = new OlimpikonokContext())
            {
                return context.Orszags.FirstOrDefault(o => o.Id == id);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("hiba történt az ország lekérése során" + ex.Message);
        }
    }
}
