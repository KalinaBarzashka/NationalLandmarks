package com.f106648.explorebulgaria.servicemodels;

import com.f106648.explorebulgaria.models.Landmark;

import java.util.ArrayList;

public class LandmarksResponse {
    private ArrayList<Landmark> landmarks;
    private int pageNumber;
    private int itemsPerPage;
    private int totalItemsCount;

    public LandmarksResponse(ArrayList<Landmark> landmarks, int pageNumber, int itemsPerPage, int totalItemsCount) {
        this.landmarks = landmarks;
        this.pageNumber = pageNumber;
        this.itemsPerPage = itemsPerPage;
        this.totalItemsCount = totalItemsCount;
    }

    public ArrayList<Landmark> getLandmarks() {
        return landmarks;
    }

    public void setLandmarks(ArrayList<Landmark> landmarks) {
        this.landmarks = landmarks;
    }

    public int getPageNumber() {
        return pageNumber;
    }

    public void setPageNumber(int pageNumber) {
        this.pageNumber = pageNumber;
    }

    public int getItemsPerPage() {
        return itemsPerPage;
    }

    public void setItemsPerPage(int itemsPerPage) {
        this.itemsPerPage = itemsPerPage;
    }

    public int getTotalItemsCount() {
        return totalItemsCount;
    }

    public void setTotalItemsCount(int totalItemsCount) {
        this.totalItemsCount = totalItemsCount;
    }
}
