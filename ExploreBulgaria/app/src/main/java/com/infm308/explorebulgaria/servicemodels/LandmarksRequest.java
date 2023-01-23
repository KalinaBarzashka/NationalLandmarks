package com.infm308.explorebulgaria.servicemodels;

public class LandmarksRequest {
    private String token;

    public LandmarksRequest(String token) {
        this.token = token;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }
}
