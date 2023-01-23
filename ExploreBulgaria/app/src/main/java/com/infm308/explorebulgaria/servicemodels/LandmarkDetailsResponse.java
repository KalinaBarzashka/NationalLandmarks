package com.infm308.explorebulgaria.servicemodels;

public class LandmarkDetailsResponse {

    private String registrationNumber;

    private String name;

    private boolean isNationalLandmark;

    private int placeId;

    private String placeName;

    private String imagePath;

    private String description;

    private String address;

    private double latitude;

    private double longitude;

    private String workingTime;

    private String worksOnWeekends;

    private String email;

    private String phone;

    private String website;

    public LandmarkDetailsResponse(String registrationNumber, String name, boolean isNationalLandmark, int placeId, String placeName, String imagePath, String description, String address, double latitude, double longitude, String workingTime, String worksOnWeekends, String email, String phone, String website) {
        this.registrationNumber = registrationNumber;
        this.name = name;
        this.isNationalLandmark = isNationalLandmark;
        this.placeId = placeId;
        this.placeName = placeName;
        this.imagePath = imagePath;
        this.description = description;
        this.address = address;
        this.latitude = latitude;
        this.longitude = longitude;
        this.workingTime = workingTime;
        this.worksOnWeekends = worksOnWeekends;
        this.email = email;
        this.phone = phone;
        this.website = website;
    }

    public String getRegistrationNumber() {
        return registrationNumber;
    }

    public void setRegistrationNumber(String registrationNumber) {
        this.registrationNumber = registrationNumber;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean isNationalLandmark() {
        return isNationalLandmark;
    }

    public void setNationalLandmark(boolean nationalLandmark) {
        isNationalLandmark = nationalLandmark;
    }

    public int getPlaceId() {
        return placeId;
    }

    public void setPlaceId(int placeId) {
        this.placeId = placeId;
    }

    public String getPlaceName() {
        return placeName;
    }

    public void setPlaceName(String placeName) {
        this.placeName = placeName;
    }

    public String getImagePath() {
        return imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    public double getLongitude() {
        return longitude;
    }

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }

    public String getWorkingTime() {
        return workingTime;
    }

    public void setWorkingTime(String workingTime) {
        this.workingTime = workingTime;
    }

    public String getWorksOnWeekends() {
        return worksOnWeekends;
    }

    public void setWorksOnWeekends(String worksOnWeekends) {
        this.worksOnWeekends = worksOnWeekends;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getWebsite() {
        return website;
    }

    public void setWebsite(String website) {
        this.website = website;
    }
}
