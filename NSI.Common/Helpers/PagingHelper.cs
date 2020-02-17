using NSI.Common.Models;
using NSI.Common.Resources;
using System;

namespace NSI.Common.Helpers
{
    public static class PagingHelper
    {
        #region Paging helper methods
        public static void CalculatePagingDetails(Paging paging, int totalRecords, out int skipElements, out int takeElements)
        {
            if (paging == null)
            {
                throw new ArgumentException(ExceptionMessages.PagingParameterNull);
            }

            // Apply paging
            paging.TotalRecords = totalRecords;

            if (paging.RecordsPerPage <= 0)
            {
                paging.RecordsPerPage = totalRecords > 0 ? totalRecords : 1;
            }

            takeElements = paging.RecordsPerPage;

            if (paging.Page >= paging.Pages)
            {
                paging.Page = paging.Pages;
            }
            else if (paging.Page <= 0)
            {
                paging.Page = 1;
            }

            skipElements = paging.Page > 0 ? (paging.Page - 1) * paging.RecordsPerPage : 0;
        }

        #endregion
    }
}
