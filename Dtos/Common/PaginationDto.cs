﻿namespace Terminal.Dtos.Common
{
    public class PaginationDto<T>
    {
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public T Items { get; set; }
    }
}
