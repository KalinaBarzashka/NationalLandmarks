package com.f106648.explorebulgaria.models;

public class User {
    private String FirstName;
    private String LastName;
    private String Username;
    private String Email;

    public User(String firstName, String lastName, String username, String email) {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Username = username;
        this.Email = email;
    }

    public String getFitstName() {
        return this.FirstName;
    }

    public void setFitstName(String fitstName) {
        this.FirstName = fitstName;
    }

    public String getLastName() {
        return this.LastName;
    }

    public void setLastName(String lastName) {
        this.LastName = lastName;
    }

    public String getUsername() {
        return this.Username;
    }

    public void setUsername(String username) {
        this.Username = username;
    }

    public String getEmail() {
        return this.Email;
    }

    public void setEmail(String email) {
        this.Email = email;
    }
}
