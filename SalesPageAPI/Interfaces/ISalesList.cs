using SalesPageAPI.Models;

namespace SalesPageAPI.Interfaces
{
    public interface ISalesList
    {
        Task<List<SalesListModel>> AddSalesList(SalesListsModel salesLists);
        Task<List<SalesListModel>> SearchSalesLists();
    }
}
