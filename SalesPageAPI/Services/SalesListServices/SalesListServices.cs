using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalesPageAPI.Data;
using SalesPageAPI.Interfaces;
using SalesPageAPI.Models;

namespace SalesPageAPI.Services.SalesListServices
{


    public class SalesListServices : ISalesList
    {
        private readonly SalesListDbContext _salesListDbContext;
        public SalesListServices(SalesListDbContext salesListDbContext)
        {
            _salesListDbContext = salesListDbContext;
        }

        public async Task<List<SalesListModel>> AddSalesList(SalesListsModel salesLists)
        {
            if (salesLists.SalesLists == null)
            {
                salesLists.SalesLists = new List<SalesListModel>();
            }

            var result = new List<SalesListModel>();

            foreach (var salesList in salesLists.SalesLists)
            {
                var incorrectResult = _salesListDbContext.DbSalesList
                    .Where(sl => sl.Seller == salesList.Seller)
                    .Where(sl => sl.Product == salesList.Product)
                    .Where(sl => sl.Type != salesList.Type)
                    .ToList();

                if (incorrectResult.Any())
                {
                    throw new ApplicationException("Um usuário não pode ser produtor e afiliado do mesmo produto");
                }
                else
                {
                    var serializedObject = JsonConvert.SerializeObject(salesList);
                    var deserializedObject = JsonConvert.DeserializeObject<SalesListModel>(serializedObject);
                    await _salesListDbContext.DbSalesList.AddAsync(deserializedObject);
                    result.Add(deserializedObject);
                }
            }

            await _salesListDbContext.SaveChangesAsync();
            return result;
        }







        public async Task<List<SalesListModel>> SearchSalesLists()
        {
            try
            {
                return await _salesListDbContext.DbSalesList.ToListAsync();
            }
            catch (Exception)
            {
                throw new ApplicationException("Nenhuma lista foi encontrada");
            }

        }
    }
}
