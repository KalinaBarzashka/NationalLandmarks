package com.f106648.explorebulgaria.servicemodels;

public class LoginResponse {

    private String token;

    public LoginResponse(String token) {
        token = token;
    }

    public String getToken() {
        return this.token;
    }

    public void setToken(String token) {
        this.token = token;
    }
}
