using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Helpers
{
    public class DataProviderHelper<T>
    {
        private readonly int currentPage;
        private readonly int itemsOnPage;
        private readonly int initialDataCount;

        public DataProviderHelper(int current, int items, int dataCount)
        {
            if (current <= 0)
                currentPage = 1;
            else
                currentPage = current;
            if (items <= 0)
                itemsOnPage = 10;
            else if (items > 100)
                itemsOnPage = 10;
            else
                itemsOnPage = items;
            initialDataCount = dataCount;
        }

        public PaginatedData<T> GetPaginatedData(List<T> foundData)
        {
            PaginatedData<T> paginatedDataResult = new PaginatedData<T>
            {
                CurrentPage = currentPage,
                RecordPerRage = itemsOnPage,
                TotalRecordsFound = initialDataCount
            };

            var query = foundData.Skip(GetItemsToSkip()).Take(GetItemsToTake()).ToList();

            if (query.Any() && currentPage > 0 && itemsOnPage > 0)
            {
                paginatedDataResult.Data = query;
                paginatedDataResult.RecordsReturned = query.Count();
            }

            return paginatedDataResult;
        }

        private int GetItemsToSkip()
        {
            int itemsToSkip = -1;

            if (initialDataCount > 0 && currentPage > 0 && itemsOnPage > 0)
            {
                itemsToSkip = (currentPage - 1) * itemsOnPage;
            }

            return itemsToSkip;
        }

        private int GetItemsToTake()
        {
            int itemsToTake = -1;

            if (initialDataCount > 0 && currentPage > 0 && itemsOnPage > 0)
            {
                int itemsToSkip = (currentPage - 1) * itemsOnPage;

                if (initialDataCount > itemsToSkip)
                {
                    int remainingDataCount = initialDataCount - itemsToSkip;
                    itemsToTake = (itemsOnPage <= remainingDataCount) ? itemsOnPage : remainingDataCount;
                }
            }

            return itemsToTake;
        }
    }
}
