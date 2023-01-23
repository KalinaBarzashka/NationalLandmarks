package com.infm308.explorebulgaria;

import com.infm308.explorebulgaria.models.Landmark;

import java.util.List;

public class LandmarkPagination {
    private int PageNumber;
    private int ItemsPerPage;
    private int TotalItemsCount;
    private List<Landmark> Landmarks;

    public LandmarkPagination(int pageNumber, int itemsPerPage, int totalItemsCount, List<Landmark> landmarks) {
        PageNumber = pageNumber;
        ItemsPerPage = itemsPerPage;
        TotalItemsCount = totalItemsCount;
        Landmarks = landmarks;
    }

    public int getPageNumber() {
        return PageNumber;
    }

    public void setPageNumber(int pageNumber) {
        PageNumber = pageNumber;
    }

    public int getItemsPerPage() {
        return ItemsPerPage;
    }

    public void setItemsPerPage(int itemsPerPage) {
        ItemsPerPage = itemsPerPage;
    }

    public int getTotalItemsCount() {
        return TotalItemsCount;
    }

    public void setTotalItemsCount(int totalItemsCount) {
        TotalItemsCount = totalItemsCount;
    }

    public List<Landmark> getLandmarks() {
        return Landmarks;
    }

    public void setLandmarks(List<Landmark> landmarks) {
        Landmarks = landmarks;
    }
}
