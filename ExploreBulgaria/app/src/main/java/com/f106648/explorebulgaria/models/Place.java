package com.f106648.explorebulgaria.models;

public class Place {
    private String Name;
    private String AreaName;
    private int AreaId;

    public Place(String name, String areaName, int areaId) {
        this.Name = name;
        this.AreaName = areaName;
        this.AreaId = areaId;
    }

    public String getName() {
        return this.Name;
    }

    public void setName(String name) {
        this.Name = name;
    }

    public String getAreaName() {
        return this.AreaName;
    }

    public void setAreaName(String areaName) {
        this.AreaName = areaName;
    }

    public int getAreaId() {
        return this.AreaId;
    }

    public void setAreaId(int areaId) {
        this.AreaId = areaId;
    }
}
