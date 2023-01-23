package com.infm308.explorebulgaria.models;

public class Landmark {
    private String id;
    private String registrationNumber;
    private String name;
    private boolean isNationalLandmark;
    private int placeId;
    private String placeName;
    private String imagePath;
    private String description;

    public Landmark(String id, String registrationNumber, String name, boolean isNationalLandmark, int placeId, String placeName, String imagePath, String description) {
        this.id = id;
        this.registrationNumber = registrationNumber;
        this.name = name;
        this.isNationalLandmark = isNationalLandmark;
        this.placeId = placeId;
        this.placeName = placeName;
        this.imagePath = imagePath;
        this.description = description;
    }

    public String getId() {
        return this.id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getRegistrationNumber() {
        return this.registrationNumber;
    }

    public void setRegistrationNumber(String registrationNumber) {
        this.registrationNumber = registrationNumber;
    }

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean isNationalLandmark() {
        return this.isNationalLandmark;
    }

    public void setNationalLandmark(boolean nationalLandmark) {
        this.isNationalLandmark = nationalLandmark;
    }

    public int getPlaceId() {
        return this.placeId;
    }

    public void setPlaceId(int placeId) {
        this.placeId = placeId;
    }

    public String getPlaceName() {
        return this.placeName;
    }

    public void setPlaceName(String placeName) {
        this.placeName = placeName;
    }

    public String getImagePath() {
        return this.imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
}
