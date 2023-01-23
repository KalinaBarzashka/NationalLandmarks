package com.infm308.explorebulgaria.models;

import java.util.Date;

public class Visit {

    private int LandmarkId;
    private String Name;
    private boolean IsNationalLandmark;
    private String PlaceName;
    private String ImagePath;
    private Date VisitedOn;
    private String Grade;
    private String Comment;

    public Visit(int landmarkId, String name, boolean isNationalLandmark, String placeName, String imagePath, Date visitedOn, String grade, String comment) {
        LandmarkId = landmarkId;
        Name = name;
        IsNationalLandmark = isNationalLandmark;
        PlaceName = placeName;
        ImagePath = imagePath;
        VisitedOn = visitedOn;
        Grade = grade;
        Comment = comment;
    }

    public int getLandmarkId() {
        return LandmarkId;
    }

    public void setLandmarkId(int landmarkId) {
        LandmarkId = landmarkId;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public boolean isNationalLandmark() {
        return IsNationalLandmark;
    }

    public void setNationalLandmark(boolean nationalLandmark) {
        IsNationalLandmark = nationalLandmark;
    }

    public String getPlaceName() {
        return PlaceName;
    }

    public void setPlaceName(String placeName) {
        PlaceName = placeName;
    }

    public String getImagePath() {
        return ImagePath;
    }

    public void setImagePath(String imagePath) {
        ImagePath = imagePath;
    }

    public Date getVisitedOn() {
        return VisitedOn;
    }

    public void setVisitedOn(Date visitedOn) {
        VisitedOn = visitedOn;
    }

    public String getGrade() {
        return Grade;
    }

    public void setGrade(String grade) {
        Grade = grade;
    }

    public String getComment() {
        return Comment;
    }

    public void setComment(String comment) {
        Comment = comment;
    }
}
