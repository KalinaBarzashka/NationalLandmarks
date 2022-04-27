export interface Pagination {
  pageNumber: number;
  totalItemsCount: number;
  itemsPerPage: number;
  pagesCount: number;
  hasPreviousPage: boolean;
  previousPageNumber: number;
  hasNextPage: boolean;
  nextPageNumber: number;
}