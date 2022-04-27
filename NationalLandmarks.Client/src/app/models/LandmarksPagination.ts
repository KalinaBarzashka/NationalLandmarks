import { Landmark } from "./Landmark";

export interface LandmarksPagination {
  landmarks: Array<Landmark>;
  pageNumber: number;
  totalItemsCount: number;
  itemsPerPage: number;
  pagesCount: number;
  hasPreviousPage: boolean;
  previousPageNumber: number;
  hasNextPage: boolean;
  nextPageNumber: number;
}