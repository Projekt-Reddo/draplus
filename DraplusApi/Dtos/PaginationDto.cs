using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DraplusApi.Dtos
{
    public class PaginationParameterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        private string _searchName = "";
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                if (value != null)
                {
                    value.Trim();
                    _searchName = value.Replace(" ", ":*|") + ":*";
                }
                else _searchName = "";
            }
        }
    }

    public class PaginationResponse<T>
    {
        public int TotalRecords { get; set; } = 0;
        public T Payload { get; set; }

        public PaginationResponse() { }

        public PaginationResponse(int totalRecords, T payload)
        {
            this.TotalRecords = totalRecords;
            this.Payload = payload;
        }
    }
}